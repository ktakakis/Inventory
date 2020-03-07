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
    [Authorize(Roles = "VendorCatalog")]

    public class VendorCatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorCatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorCatalog
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorCatalog.Include(v => v.Vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorCatalog/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCatalog = await _context.VendorCatalog
                .Include(v => v.Vendor)
                .SingleOrDefaultAsync(m => m.VendorCatalogId == id);
            if (vendorCatalog == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName");
            return View(vendorCatalog);
        }

        // GET: VendorCatalog/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName");
            return View();
        }

        // POST: VendorCatalog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorCatalogId,VendorCatalogName,VendorId,HasChild,createdAt")] VendorCatalog vendorCatalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorCatalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = vendorCatalog.VendorCatalogId });
            }
            ViewData["VendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorCatalog.VendorId);
            return View(vendorCatalog);
        }

        // GET: VendorCatalog/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCatalog = await _context.VendorCatalog.SingleOrDefaultAsync(m => m.VendorCatalogId == id);
            if (vendorCatalog == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorCatalog.VendorId);
            return View(vendorCatalog);
        }

        // POST: VendorCatalog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VendorCatalogId,VendorCatalogName,VendorId,HasChild,createdAt")] VendorCatalog vendorCatalog)
        {
            if (id != vendorCatalog.VendorCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorCatalogExists(vendorCatalog.VendorCatalogId))
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
            ViewData["VendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorCatalog.VendorId);
            return View(vendorCatalog);
        }

        // GET: VendorCatalog/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCatalog = await _context.VendorCatalog
                .Include(v => v.Vendor)
                .SingleOrDefaultAsync(m => m.VendorCatalogId == id);
            if (vendorCatalog == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendor, "vendorId", "vendorName", vendorCatalog.VendorId);
            return View(vendorCatalog);
        }

        // POST: VendorCatalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vendorCatalog = await _context.VendorCatalog.SingleOrDefaultAsync(m => m.VendorCatalogId == id);
            _context.VendorCatalog.Remove(vendorCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorCatalogExists(string id)
        {
            return _context.VendorCatalog.Any(e => e.VendorCatalogId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class VendorCatalog
        {
            public const string Controller = "VendorCatalog";
            public const string Action = "Index";
            public const string Role = "VendorCatalog";
            public const string Url = "/VendorCatalog/Index";
            public const string Name = "VendorCatalog";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Κατάλογος Εκπτώσεων")]
        public bool VendorCatalogRole { get; set; } = false;
    }
}
