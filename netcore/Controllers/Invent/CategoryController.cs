using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models.Invent;
using netcore.Services;

namespace netcore.Controllers.Invent
{
    [Authorize(Roles = "Category")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName", category.ParentCategoryID);
            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            List<Category> categoryList = _context.Category.ToList();
            categoryList.Insert(0, new Category { CategoryID = 0, CategoryName = "Επιλέξτε" });
            ViewData["ParentCategoryID"] = new SelectList(categoryList, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName,ParentCategoryID")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Category> categoryList = _context.Category.ToList();
            categoryList.Insert(0, new Category { CategoryID = 0, CategoryName = "Επιλέξτε" });

            ViewData["ParentCategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName",category.ParentCategoryID);
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName,ParentCategoryID")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName", category.ParentCategoryID);
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Category, "CategoryID", "CategoryName", category.ParentCategoryID);
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.SingleOrDefaultAsync(m => m.CategoryID == id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryID == id);
        }

        public async Task<IActionResult> FillDropdown()
        {
            var categories = await _context.Category.ToListAsync();
            var trees = categories.BuildTrees();
            return new JsonResult(trees);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Category
        {
            public const string Controller = "Category";
            public const string Action = "Index";
            public const string Role = "Category";
            public const string Url = "/Category/Index";
            public const string Name = "Category";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Κατηγορίες")]
        public bool CategoryRole { get; set; } = false;
    }
}
