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
    [Authorize(Roles = "InvoiceLine")]
    public class InvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvoiceLine.Include(i => i.Invoice).Include(i => i.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLine
                .Include(i => i.Invoice)
                .Include(i => i.Product)
                .SingleOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLine/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId");
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId");
            return View();
        }

        // POST: InvoiceLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceLineId,InvoiceId,ProductId,Qty,Price,TotalBeforeDiscount,UnitCost,Discount,DiscountAmount,TotalAfterDiscount,ProductVAT,ProductVATAmount,SpecialTaxDiscount,SpecialTaxAmount,TotalSpecialTaxAmount,TotalWithSpecialTax,TotalAmount,createdAt")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId", invoiceLine.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId", invoiceLine.ProductId);
            return View(invoiceLine);
        }

        // GET: InvoiceLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLine.SingleOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId", invoiceLine.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId", invoiceLine.ProductId);
            return View(invoiceLine);
        }

        // POST: InvoiceLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("InvoiceLineId,InvoiceId,ProductId,Qty,Price,TotalBeforeDiscount,UnitCost,Discount,DiscountAmount,TotalAfterDiscount,ProductVAT,ProductVATAmount,SpecialTaxDiscount,SpecialTaxAmount,TotalSpecialTaxAmount,TotalWithSpecialTax,TotalAmount,createdAt")] InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.InvoiceLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
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
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId", invoiceLine.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId", invoiceLine.ProductId);
            return View(invoiceLine);
        }

        // GET: InvoiceLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLine
                .Include(i => i.Invoice)
                .Include(i => i.Product)
                .SingleOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // POST: InvoiceLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var invoiceLine = await _context.InvoiceLine.SingleOrDefaultAsync(m => m.InvoiceLineId == id);
            _context.InvoiceLine.Remove(invoiceLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceLineExists(string id)
        {
            return _context.InvoiceLine.Any(e => e.InvoiceLineId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class InvoiceLine
        {
            public const string Controller = "InvoiceLine";
            public const string Action = "Index";
            public const string Role = "InvoiceLine";
            public const string Url = "/InvoiceLine/Index";
            public const string Name = "InvoiceLine";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "InvoiceLine")]
        public bool InvoiceLineRole { get; set; } = false;
    }
}

