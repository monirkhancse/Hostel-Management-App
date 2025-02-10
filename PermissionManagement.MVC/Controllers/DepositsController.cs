using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PermissionManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.ViewModels;
using PermissionManagement.MVC.Data;
using Microsoft.AspNetCore.Authorization;

namespace PermissionManagement.MVC.Controllers
{
   
    public class DepositsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepositsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? selectedMemberId, DateTime? startDate, DateTime? endDate, int? pageNumber)
        {
            int pageSize = 31;
            //startDate ??= DateTime.Now.Date; // Default to today at 00:00 (midnight) if not provided
            endDate ??= DateTime.Now.Date;   // Default to today at 00:00 (midnight) if not provided

            var members = await _context.Members.OrderBy(m => m.Name).ToListAsync();

            // Start the query with the base set of meals
            var deposits = _context.Deposits.Include(d => d.Member).AsQueryable();

            // Apply filters by Member if provided
            if (selectedMemberId.HasValue)
            {
                deposits = deposits.Where(d => d.MemberId == selectedMemberId.Value);
            }

            // Apply filters by Date between if provided
            if (startDate.HasValue && endDate.HasValue)
            {
                deposits = deposits.Where(d => d.DepositDate >= startDate.Value && d.DepositDate <= endDate.Value);
            }

            // Paginate the filtered results
            var paginatedDeposit = await PaginatedList<Deposit>.CreateAsync(deposits, pageNumber ?? 1, pageSize);

            var viewModel = new SearchViewModel
            {
                Members = new SelectList(members, "MemberId", "Name",selectedMemberId),
                Deposits = paginatedDeposit,
                SelectedMemberId = selectedMemberId,
                StartDate = startDate,
                EndDate = endDate
            };

            return View(viewModel);
        }

        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var deposit = await _context.Deposits
                .Include(d => d.Member)
                .FirstOrDefaultAsync(m => m.DepositId == id);
            return View(deposit);
        }

        // GET: Deposits/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("DepositId,DepositDate,Amount,Remarks,MemberId")] Deposit deposit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(deposit);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MemberId"] = new SelectList(_context.Members , "MemberId", "Name", deposit.MemberId);
        //    return View(deposit);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([Bind("DepositId,DepositDate,Amount,Remarks,MemberId")] Deposit deposit)
        {
            // Check if a deposit already exists for the member on the same date
            bool depositExists = await _context.Deposits.AnyAsync(d =>
                d.MemberId == deposit.MemberId && d.DepositDate.Date == deposit.DepositDate.Date);

            if (depositExists)
            {
                ModelState.AddModelError("", "A deposit for this member has already been made today.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(deposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", deposit.MemberId);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", deposit.MemberId);
            return View(deposit);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, [Bind("DepositId,DepositDate,Amount,Remarks,MemberId")] Deposit deposit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //            _context.Update(deposit);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", deposit.MemberId);
        //    return View(deposit);
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("DepositId,DepositDate,Amount,Remarks,MemberId")] Deposit deposit)
        {
            // Check if a different deposit exists for the member on the same date
            bool depositExists = await _context.Deposits.AnyAsync(d =>
                d.MemberId == deposit.MemberId && d.DepositDate.Date == deposit.DepositDate.Date && d.DepositId != deposit.DepositId);

            if (depositExists)
            {
                ModelState.AddModelError("", "A deposit for this member has already been made today.");
            }

            if (ModelState.IsValid)
            {
                _context.Update(deposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", deposit.MemberId);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var deposit = await _context.Deposits
                .Include(d => d.Member)
                .FirstOrDefaultAsync(m => m.DepositId == id);
                return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
