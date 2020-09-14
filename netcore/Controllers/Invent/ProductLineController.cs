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

namespace netcore.Controllers.Invent
{
    [Authorize(Roles = "ProductLine")]
    public class ProductLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductLine
                .Include(x=>x.Component)
                .Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLine = await _context.ProductLine
                .Include(x => x.Component)
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.ProductLineId == id);
            if (productLine == null)
            {
                return NotFound();
            }

            return View(productLine);
        }

        // GET: ProductLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.ProductLine.SingleOrDefault(m => m.ProductLineId == id);
            var selected = _context.Product.SingleOrDefault(m => m.productId == masterid);
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productId");
            ViewData["componentId"] = new SelectList(_context.Product.Where(x => x.IsMaterial == true), "productId", "productName");
           if (check == null)
            {
                ProductLine objline = new ProductLine();
                objline.Product = selected;
                objline.ProductId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }

        }

        // POST: ProductLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductLineId,Percentage,ProductId,createdAt,ComponentId")] ProductLine productLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productName", productLine.ProductId);
            ViewData["componentId"] = new SelectList(_context.Product.Where(x => x.IsMaterial == true), "productId", "productName", productLine.ComponentId);
            return View(productLine);
        }

        // GET: ProductLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLine = await _context.ProductLine.SingleOrDefaultAsync(m => m.ProductLineId == id);
            if (productLine == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productName", productLine.ProductId);
            ViewData["componentId"] = new SelectList(_context.Product.Where(x => x.IsMaterial == true), "productId", "productName", productLine.ComponentId);
            return View(productLine);
        }

        // POST: ProductLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductLineId,Percentage,ProductId,createdAt,ComponentId")] ProductLine productLine)
        {
            if (id != productLine.ProductLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductLineExists(productLine.ProductLineId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productName", productLine.ProductId);
            ViewData["componentId"] = new SelectList(_context.Product.Where(x => x.IsMaterial == true), "productId", "productName", productLine.ComponentId);
            return View(productLine);
        }

        // GET: ProductLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productLine = await _context.ProductLine
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.ProductLineId == id);
            if (productLine == null)
            {
                return NotFound();
            }

            return View(productLine);
        }

        // POST: ProductLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productLine = await _context.ProductLine.SingleOrDefaultAsync(m => m.ProductLineId == id);
            _context.ProductLine.Remove(productLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductLineExists(string id)
        {
            return _context.ProductLine.Any(e => e.ProductLineId == id);
        }
    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class ProductLine
        {
            public const string Controller = "ProductLine";
            public const string Action = "Index";
            public const string Role = "ProductLine";
            public const string Url = "/ProductLine/Index";
            public const string Name = "ProductLine";
        }
    }
}

namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "ProductLine")]
        public bool ProductLineRole { get; set; } = false;
    }
}
