using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Invent;
using System.Globalization;
using System.Threading;

namespace netcore.Controllers.Invent
{
    [Authorize(Roles = "Catalog")]
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Catalog.Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                    .Include(x => x.CatalogLine)
                    .Include(s => s.Customer)
                        .SingleOrDefaultAsync(m => m.CatalogId == id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "customerId", "customerName", catalog.CustomerId);
            return View(catalog);
        }

        // GET: Catalog/Create
        public IActionResult Create()
        {
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName");
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode");
            Catalog catalog = new Catalog();
            return View(catalog);
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogId,CatalogName,CustomerId,HasChild,createdAt")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Create Customer " + catalog.CatalogName + " Success";
                return RedirectToAction(nameof(Details), new { id = catalog.CatalogId });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "customerId", "customerName", catalog.CustomerId);
            return View(catalog);
        }

        // GET: Catalog/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog.Include(x => x.CatalogLine).SingleOrDefaultAsync(m => m.CatalogId == id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", catalog.CustomerId);
            return View(catalog);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CatalogId,CatalogName,CustomerId,HasChild,createdAt")] Catalog catalog)
        {
            if (id != catalog.CatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.CatalogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               TempData["TransMessage"] = "Η Επεξεργασία του Καταλόγου " + catalog.CatalogName + " έγινε με Επιτυχία";
               return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "customerId", "customerName", catalog.CustomerId);
            return View(catalog);
        }

        // GET: Catalog/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .Include(x => x.CatalogLine)
                .Include(c => c.Customer)
                .SingleOrDefaultAsync(m => m.CatalogId == id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "customerId", "customerName", catalog.CustomerId);
            return View(catalog);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var catalog = await _context.Catalog.SingleOrDefaultAsync(m => m.CatalogId == id);
            _context.Catalog.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(string id)
        {
            return _context.Catalog.Any(e => e.CatalogId == id);
        }
    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Catalog
        {
            public const string Controller = "Catalog";
            public const string Action = "Index";
            public const string Role = "Catalog";
            public const string Url = "/Catalog/Index";
            public const string Name = "Catalog";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Κατάλογος Εκπτώσεων")]
        public bool CatalogRole { get; set; } = false;
    }
}
