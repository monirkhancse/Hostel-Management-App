using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using PermissionManagement.MVC.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace PermissionManagement.MVC.Controllers
{
   
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Rate()
        {
            var viewModel = new RateViewModel
            {
                Meals = await _context.Meals.ToListAsync(),
                Expenses = await _context.Expenses.ToListAsync(),
                Deposits = await _context.Deposits.ToListAsync(),
                Managers = await _context.Managers.ToListAsync(),
                Members = await _context.Members.ToListAsync()
            };

            return View(viewModel);
        }
       
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int? selectedMemberId, int? pageNumber)
        {
            int pageSize = 31;
            /*startDate ??= DateTime.Now.Date;*/ // Default to today at 00:00 (midnight) if not provided
            endDate ??= DateTime.Now.Date;   // Default to today at 00:00 (midnight) if not provided

            var members = await _context.Members.OrderBy(m => m.Name).ToListAsync();

            // Start the query with the base set of meals
            var meals = _context.Meals.Include(m => m.Member).AsQueryable();

            // Apply filters by Member if provided
            if (selectedMemberId.HasValue)
            {
                meals = meals.Where(m => m.MemberId == selectedMemberId.Value);
            }
            // Apply filters by Date between if provided
            if (startDate.HasValue && endDate.HasValue)
            {
                meals = meals.Where(m => m.EntryDate >= startDate.Value && m.EntryDate <= endDate.Value);
            }

            // Paginate the filtered results
            var paginatedMeals = await PaginatedList<Meal>.CreateAsync(meals, pageNumber ?? 1, pageSize);

            // Create the view model with the paginated meals
            var viewModel = new SearchViewModel
            {
                Members = new SelectList(members, "MemberId", "Name", selectedMemberId),
                Meals = paginatedMeals,
                SelectedMemberId = selectedMemberId,
                StartDate = startDate,
                EndDate = endDate
            };

            return View(viewModel);
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var meal = await _context.Meals
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MealId == id);
                return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("MealId,EntryDate,TodayMeal,Remarks,MemberId")] Meal meal)
        {
            // Check if a Meal already exists for the member on the same date
            bool mealExist = await _context.Meals.AnyAsync(d =>
            d.MemberId == meal.MemberId && d.EntryDate.Date == meal.EntryDate.Date);

            if (mealExist)
            {
                ModelState.AddModelError("", "A Meal for this member has already been made today.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", meal.MemberId);
            return View(meal);
        }

        // GET: Meals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var meal = await _context.Meals.FindAsync(id);
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", meal.MemberId);
            return View(meal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("MealId,EntryDate,TodayMeal,Remarks,MemberId")] Meal meal)
        {
            // Check if a Meal already exists for the member on the same date
            bool mealExist = await _context.Meals.AnyAsync(d =>
            d.MemberId == meal.MemberId && d.EntryDate.Date == meal.EntryDate.Date && d.MealId != meal.MealId);

            if (mealExist)
            {
                ModelState.AddModelError("", "A Meal for this member has already been made today.");
            }
            if (ModelState.IsValid)
            {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Name", meal.MemberId);
            return View(meal);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        { 
            var meal = await _context.Meals
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.MealId == id);
                return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
