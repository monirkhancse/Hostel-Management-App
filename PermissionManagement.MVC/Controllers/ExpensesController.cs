using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using PermissionManagement.MVC.ViewModels;

namespace PermissionManagement.MVC.Controllers
{
    
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(int? selectedMemberId, DateTime? startDate,DateTime? endDate,int? pageNumber)
        {
           int pageSize = 31;
            //startDate ??= DateTime.Now.Date; // Default to today at 00:00 (midnight) if not provided
            endDate ??= DateTime.Now.Date;   // Default to today at 00:00 (midnight) if not provided

            var members=await _context.Members.OrderBy(m=>m.Name).ToListAsync();

            // Start the query with the base set of meals
            var expenses = _context.Expenses.Include(e=>e.Member).AsQueryable();

            // Apply filters by Member if provided
            if (selectedMemberId.HasValue)
            {
                expenses=expenses.Where(e=>e.MemberId == selectedMemberId.Value);
            }
            // Apply filters by Date between if provided
            if (startDate.HasValue && endDate.HasValue)
            {
                expenses=expenses.Where(e=>e.ExpenseDate >= startDate.Value && e.ExpenseDate <= endDate.Value);
            }
            var paginatedExpense=await PaginatedList<Expense>.CreateAsync(expenses,pageNumber?? 1, pageSize);

            // Create the view model with the paginated meals
            var viewModel = new SearchViewModel
            {
                Members=new SelectList(members,"MemberId","Name",selectedMemberId),
                Expenses= paginatedExpense,
                SelectedMemberId= selectedMemberId,
                StartDate=startDate,
                EndDate=endDate
            };
            return View(viewModel);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var expense = await _context.Expenses
                .Include(d => d.Member)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
                return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ExpenseId,ExpenseDate,BazarCost,Remarks,MemberId")] Expense expense)
        {
            // Check if a different Expense exists for the member on the same date
            bool expenseExist = await _context.Expenses.AnyAsync(e => e.MemberId == expense.MemberId && e.ExpenseDate.Date == expense.ExpenseDate.Date);
            if (expenseExist)
            {
                ModelState.AddModelError("", "A Expense for this member has already been made today.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", expense.MemberId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", expense.MemberId);
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,ExpenseDate,BazarCost,Remarks,MemberId")] Expense expense)
        {
            // Check if a different Expense exists for the member on the same date
            bool expenseExist = await _context.Expenses.AnyAsync(e => e.MemberId == expense.MemberId && e.ExpenseDate.Date == expense.ExpenseDate.Date && e.ExpenseId != expense.ExpenseId);
            if (expenseExist)
            {
                ModelState.AddModelError("", "A Expense for this member has already been made today.");
            }
            if (ModelState.IsValid)
            { 
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", expense.MemberId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var expense = await _context.Expenses
                .Include(d => d.Member)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
