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
    [Authorize(Roles = "PaymentReceive")]
    public class PaymentReceiveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentReceiveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentReceive
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PaymentReceive.Include(p => p.invoice).Include(p => p.paymentType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentReceive/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentReceive = await _context.PaymentReceive
                .Include(p => p.invoice)
                .Include(p => p.paymentType)
                .SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            if (paymentReceive == null)
            {
                return NotFound();
            }

            return View(paymentReceive);
        }

        // GET: PaymentReceive/Create
        public IActionResult Create()
        {
            var query =
            from Invoice in _context.Invoice
            select new
            {
                Invoice.InvoiceId,
                description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")")
            };

            ViewData["InvoiceId"] = new SelectList(query, "InvoiceId", "description");
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName");
            return View();
        }

        // POST: PaymentReceive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentReceiveId,PaymentReceiveName,InvoiceId,PaymentDate,PaymentTypeId,PaymentAmount,IsFullPayment,createdAt")] PaymentReceive paymentReceive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentReceive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var query =
                from Invoice in _context.Invoice
                select new
                {
                    Invoice.InvoiceId,
                    description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")")
                };

            ViewData["InvoiceId"] = new SelectList(query, "InvoiceId", "description", paymentReceive.InvoiceId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeId", paymentReceive.PaymentTypeId);
            return View(paymentReceive);
        }

        // GET: PaymentReceive/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentReceive = await _context.PaymentReceive.SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            if (paymentReceive == null)
            {
                return NotFound();
            }
            var query =
                from Invoice in _context.Invoice
                select new
                {
                    Invoice.InvoiceId,
                    description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")")
                };

            ViewData["InvoiceId"] = new SelectList(query, "InvoiceId", "description", paymentReceive.InvoiceId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeId", paymentReceive.PaymentTypeId);
            return View(paymentReceive);
        }

        // POST: PaymentReceive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PaymentReceiveId,PaymentReceiveName,InvoiceId,PaymentDate,PaymentTypeId,PaymentAmount,IsFullPayment,createdAt")] PaymentReceive paymentReceive)
        {
            if (id != paymentReceive.PaymentReceiveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentReceive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentReceiveExists(paymentReceive.PaymentReceiveId))
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
            var query =
                from Invoice in _context.Invoice
                select new
                {
                    Invoice.InvoiceId,
                    description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")")
                };

            ViewData["InvoiceId"] = new SelectList(query, "InvoiceId", "description", paymentReceive.InvoiceId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeId", paymentReceive.PaymentTypeId);
            return View(paymentReceive);
        }

        // GET: PaymentReceive/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentReceive = await _context.PaymentReceive
                .Include(p => p.invoice)
                .Include(p => p.paymentType)
                .SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            if (paymentReceive == null)
            {
                return NotFound();
            }

            return View(paymentReceive);
        }

        // POST: PaymentReceive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var paymentReceive = await _context.PaymentReceive.SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            _context.PaymentReceive.Remove(paymentReceive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentReceiveExists(string id)
        {
            return _context.PaymentReceive.Any(e => e.PaymentReceiveId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class PaymentReceive
        {
            public const string Controller = "PaymentReceive";
            public const string Action = "Index";
            public const string Role = "PaymentReceive";
            public const string Url = "/PaymentReceive/Index";
            public const string Name = "PaymentReceive";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "PaymentReceive")]
        public bool PaymentReceiveRole { get; set; } = false;
    }
}
