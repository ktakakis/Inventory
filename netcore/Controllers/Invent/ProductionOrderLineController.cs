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
    [Authorize(Roles = "ProductionOrderLine")]
    public class ProductionOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductionOrderLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductionOrderLine.Include(p => p.Product).Include(p => p.ProductionOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductionOrderLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionOrderLine = await _context.ProductionOrderLine
                .Include(p => p.Product)
                .Include(p => p.ProductionOrder)
                .SingleOrDefaultAsync(m => m.ProductionOrderLineId == id);
            if (productionOrderLine == null)
            {
                return NotFound();
            }

            return View(productionOrderLine);
        }

        // GET: ProductionOrderLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.ProductionOrderLine
                            .Include(x => x.ProductionOrder)
                            .SingleOrDefault(m => m.ProductionOrderLineId == id);
            var selected = _context.ProductionOrder
                .SingleOrDefault(m => m.ProductionOrderId == masterid);
            if (check == null)
            {
                ProductionOrderLine objline = new ProductionOrderLine
                {
                    ProductionOrder = selected,
                    ProductionOrderId = masterid,
                    Qty = 1,
                };
                ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productCode");
                ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber");
               return View(objline);
            }
            else
            {
                ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productCode");
                ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber");
                return View(check);
            }

        }

        // POST: ProductionOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductionOrderLineId,ProductionOrderId,ProductId,Qty,createdAt")] ProductionOrderLine productionOrderLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productionOrderLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productCode", productionOrderLine.ProductId);
            ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", productionOrderLine.ProductionOrderId);
            return View(productionOrderLine);
        }

        // GET: ProductionOrderLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionOrderLine = await _context.ProductionOrderLine.SingleOrDefaultAsync(m => m.ProductionOrderLineId == id);
            if (productionOrderLine == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productCode", productionOrderLine.ProductId);
            ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", productionOrderLine.ProductionOrderId);
            return View(productionOrderLine);
        }

        // POST: ProductionOrderLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductionOrderLineId,ProductionOrderId,ProductId,Qty,createdAt")] ProductionOrderLine productionOrderLine)
        {
            if (id != productionOrderLine.ProductionOrderLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionOrderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionOrderLineExists(productionOrderLine.ProductionOrderLineId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productCode", productionOrderLine.ProductId);
            ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", productionOrderLine.ProductionOrderId);
            return View(productionOrderLine);
        }

        // GET: ProductionOrderLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionOrderLine = await _context.ProductionOrderLine
                .Include(p => p.Product)
                .Include(p => p.ProductionOrder)
                .SingleOrDefaultAsync(m => m.ProductionOrderLineId == id);
            if (productionOrderLine == null)
            {
                return NotFound();
            }

            return View(productionOrderLine);
        }

        // POST: ProductionOrderLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productionOrderLine = await _context.ProductionOrderLine.SingleOrDefaultAsync(m => m.ProductionOrderLineId == id);
            _context.ProductionOrderLine.Remove(productionOrderLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionOrderLineExists(string id)
        {
            return _context.ProductionOrderLine.Any(e => e.ProductionOrderLineId == id);
        }
    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class ProductionOrderLine
        {
            public const string Controller = "ProductionOrderLine";
            public const string Action = "Index";
            public const string Role = "ProductionOrderLine";
            public const string Url = "/ProductionOrderLine/Index";
            public const string Name = "ProductionOrderLine";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "ProductionOrderLine")]
        public bool ProductionOrderLineRole { get; set; } = false;
    }
}