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
using Newtonsoft.Json;

namespace netcore.Controllers.Invent
{

    [Authorize(Roles = "VendorCatalogLine")]
    public class VendorCatalogLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorCatalogLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorCatalogLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorCatalogLine.Include(p => p.Product).Include(c => c.VendorCatalog);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorCatalogLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCatalogLine = await _context.VendorCatalogLine
                    .Include(p => p.Product)
                    .Include(c => c.VendorCatalog)
                        .SingleOrDefaultAsync(m => m.VendorCatalogLineId == id);
            if (vendorCatalogLine == null)
            {
                return NotFound();
            }

            return View(vendorCatalogLine);
        }


        // GET: VendorCatalogLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.VendorCatalogLine.SingleOrDefault(m => m.VendorCatalogLineId == id);
            var selected = _context.VendorCatalog.SingleOrDefault(m => m.VendorCatalogId == masterid);
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode");
            ViewData["VendorCatalogId"] = new SelectList(_context.VendorCatalog, "VendorCatalogId", "VendorCatalogName");
            if (check == null)
            {
                VendorCatalogLine objline = new VendorCatalogLine
                {
                    VendorCatalog = selected,
                    VendorCatalogId = masterid
                };
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }


        // POST: VendorCatalogLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorCatalogLineId,Discount,EndDate,ProductId,VendorCatalogId,createdAt,Price")] VendorCatalogLine vendorCatalogLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorCatalogLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", vendorCatalogLine.ProductId);
            ViewData["vendorCatalogId"] = new SelectList(_context.Catalog, "VendorCatalogId", "VendorCatalogName", vendorCatalogLine.VendorCatalogId);
            return View(vendorCatalogLine);
        }

        // GET: CatalogLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCatalogLine = await _context.VendorCatalogLine.SingleOrDefaultAsync(m => m.VendorCatalogLineId == id);
            if (vendorCatalogLine == null)
            {
                return NotFound();
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", vendorCatalogLine.ProductId);
            ViewData["vendorCatalogId"] = new SelectList(_context.VendorCatalog, "VendorCatalogId", "VendorCatalogName", vendorCatalogLine.VendorCatalogId);
            return View(vendorCatalogLine);
        }

        // POST: VendorCatalogLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VendorCatalogLineId,Discount,EndDate,ProductId,VendorCatalogId,createdAt,Price")] VendorCatalogLine vendorCatalogLine)
        {
            if (id != vendorCatalogLine.VendorCatalogLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorCatalogLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorCatalogLineExists(vendorCatalogLine.VendorCatalogLineId))
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
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", vendorCatalogLine.ProductId);
            ViewData["vendorCatalogId"] = new SelectList(_context.VendorCatalog, "VendorCatalogId", "VendorCatalogName", vendorCatalogLine.VendorCatalogId);
            return View(vendorCatalogLine);
        }

        // GET: CatalogLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorCatalogLine = await _context.VendorCatalogLine
                    .Include(s => s.Product)
                    .Include(c => c.VendorCatalog)
                    .SingleOrDefaultAsync(m => m.VendorCatalogLineId == id);
            if (vendorCatalogLine == null)
            {
                return NotFound();
            }

            return View(vendorCatalogLine);
        }




        // POST: VendorCatalogLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vendorCatalogLine = await _context.VendorCatalogLine.SingleOrDefaultAsync(m => m.VendorCatalogLineId == id);
            _context.VendorCatalogLine.Remove(vendorCatalogLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorCatalogLineExists(string id)
        {
            return _context.VendorCatalogLine.Any(e => e.VendorCatalogLineId == id);
        }

    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class VendorCatalogLine
        {
            public const string Controller = "VendorCatalogLine";
            public const string Action = "Index";
            public const string Role = "VendorCatalogLine";
            public const string Url = "/VendorCatalogLine/Index";
            public const string Name = "VendorCatalogLine";
        }
    }
}

namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "VendorCatalogLine")]
        public bool VendorCatalogLineRole { get; set; } = false;
    }
}

 