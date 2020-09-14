using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models.Invent;

namespace netcore.Controllers.Invent
{
    public class ProductionLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductionLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductionLine.Include(p => p.Production).Include(p => p.branch).Include(p => p.product).Include(p => p.warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductionLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionLine = await _context.ProductionLine
                .Include(p => p.Production)
                .Include(p => p.branch)
                .Include(p => p.product)
                .Include(p => p.warehouse)
                .SingleOrDefaultAsync(m => m.ProductionLineId == id);
            if (productionLine == null)
            {
                return NotFound();
            }

            return View(productionLine);
        }

        // GET: ProductionLine/Create
        public IActionResult Create()
        {
            ViewData["ProductionId"] = new SelectList(_context.Production, "ProductionId", "ProductionId");
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchId");
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productId");
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseId");
            return View();
        }

        // POST: ProductionLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductionLineId,ProductionId,branchId,warehouseId,productId,qty,createdAt")] ProductionLine productionLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productionLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductionId"] = new SelectList(_context.Production, "ProductionId", "ProductionId", productionLine.ProductionId);
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchId", productionLine.branchId);
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productId", productionLine.productId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseId", productionLine.warehouseId);
            return View(productionLine);
        }

        // GET: ProductionLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionLine = await _context.ProductionLine.SingleOrDefaultAsync(m => m.ProductionLineId == id);
            if (productionLine == null)
            {
                return NotFound();
            }
            ViewData["ProductionId"] = new SelectList(_context.Production, "ProductionId", "ProductionId", productionLine.ProductionId);
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchId", productionLine.branchId);
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productId", productionLine.productId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseId", productionLine.warehouseId);
            return View(productionLine);
        }

        // POST: ProductionLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductionLineId,ProductionId,branchId,warehouseId,productId,qty,createdAt")] ProductionLine productionLine)
        {
            if (id != productionLine.ProductionLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionLineExists(productionLine.ProductionLineId))
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
            ViewData["ProductionId"] = new SelectList(_context.Production, "ProductionId", "ProductionId", productionLine.ProductionId);
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchId", productionLine.branchId);
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productId", productionLine.productId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseId", productionLine.warehouseId);
            return View(productionLine);
        }

        // GET: ProductionLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionLine = await _context.ProductionLine
                .Include(p => p.Production)
                .Include(p => p.branch)
                .Include(p => p.product)
                .Include(p => p.warehouse)
                .SingleOrDefaultAsync(m => m.ProductionLineId == id);
            if (productionLine == null)
            {
                return NotFound();
            }

            return View(productionLine);
        }

        // POST: ProductionLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productionLine = await _context.ProductionLine.SingleOrDefaultAsync(m => m.ProductionLineId == id);
            _context.ProductionLine.Remove(productionLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionLineExists(string id)
        {
            return _context.ProductionLine.Any(e => e.ProductionLineId == id);
        }
    }
}
