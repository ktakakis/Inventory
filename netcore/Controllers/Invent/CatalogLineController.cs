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

    [Authorize(Roles = "CatalogLine")]
    public class CatalogLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CatalogLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CatalogLine.Include(p=>p.Product).Include(c => c.Catalog);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CatalogLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogLine = await _context.CatalogLine
                    .Include(p=>p.Product)
                    .Include(c => c.Catalog)
                        .SingleOrDefaultAsync(m => m.CatalogLineId == id);
            if (catalogLine == null)
            {
                return NotFound();
            }

            return View(catalogLine);
        }


        // GET: CatalogLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.CatalogLine.SingleOrDefault(m => m.CatalogLineId == id);
            var selected = _context.Catalog.SingleOrDefault(m => m.CatalogId == masterid);
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode");
            ViewData["catalogId"] = new SelectList(_context.Catalog, "catalogId", "catalogId");
            if (check == null)
            {
                CatalogLine objline = new CatalogLine
                {
                    Catalog = selected,
                    CatalogId = masterid
                };
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }


        // POST: CatalogLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogLineId,CatalogId,Discount,ProductId,createdAt")] CatalogLine catalogLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", catalogLine.ProductId);
            ViewData["catalogId"] = new SelectList(_context.Catalog, "catalogId", "catalogId", catalogLine.CatalogId);
            return View(catalogLine);
        }

        // GET: CatalogLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogLine = await _context.CatalogLine.SingleOrDefaultAsync(m => m.CatalogLineId == id);
            if (catalogLine == null)
            {
                return NotFound();
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", catalogLine.ProductId);
            ViewData["catalogId"] = new SelectList(_context.Catalog, "catalogId", "catalogId", catalogLine.CatalogId);
            return View(catalogLine);
        }

        // POST: CatalogLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CatalogLineId,CatalogId,Discount,ProductId,createdAt")] CatalogLine catalogLine)
        {
            if (id != catalogLine.CatalogLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogLineExists(catalogLine.CatalogLineId))
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
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", catalogLine.ProductId);
            ViewData["catalogId"] = new SelectList(_context.Catalog, "catalogId", "catalogId", catalogLine.CatalogId);
            return View(catalogLine);
        }

        // GET: CatalogLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogLine = await _context.CatalogLine
                    .Include(s => s.Product)
                    .Include(c => c.Catalog)
                    .SingleOrDefaultAsync(m => m.CatalogLineId == id);
            if (catalogLine == null)
            {
                return NotFound();
            }

            return View(catalogLine);
        }




        // POST: CatalogLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var catalogLine = await _context.CatalogLine.SingleOrDefaultAsync(m => m.CatalogLineId == id);
            _context.CatalogLine.Remove(catalogLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogLineExists(string id)
        {
            return _context.CatalogLine.Any(e => e.CatalogLineId == id);
        }

    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class CatalogLine
        {
            public const string Controller = "CatalogLine";
            public const string Action = "Index";
            public const string Role = "CatalogLine";
            public const string Url = "/CatalogLine/Index";
            public const string Name = "CatalogLine";
        }
    }
} 
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "CatalogLine")]
        public bool CatalogLineRole { get; set; } = false;
    }
}



