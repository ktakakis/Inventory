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
    [Authorize(Roles = "VendorPayment")]
    public class VendorPaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorPaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorPayment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorPayment.Include(v => v.CashRepository).Include(v => v.Employee).Include(v => v.paymentType).Include(v => v.purchaseOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorPayment/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorPayment = await _context.VendorPayment
                .Include(v => v.CashRepository)
                .Include(v => v.Employee)
                .Include(v => v.paymentType)
                .Include(v => v.purchaseOrder)
                .SingleOrDefaultAsync(m => m.VendorPaymentId == id);
            if (vendorPayment == null)
            {
                return NotFound();
            }

            return View(vendorPayment);
        }

        // GET: VendorPayment/Create
        public IActionResult Create()
        {
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName");
            List<PurchaseOrder> poList = _context.PurchaseOrder.Where(x => x.purchaseOrderStatus == PurchaseOrderStatus.Completed).ToList();
            poList.Insert(0, new PurchaseOrder { purchaseOrderId = "0", purchaseOrderNumber = "Select" });
            ViewData["purchaseOrderId"] = new SelectList(poList, "purchaseOrderId", "purchaseOrderNumber");
            VendorPayment vp = new VendorPayment();
            vp.PaymentDate = DateTime.Today;
            return View(vp);
        }

        // POST: VendorPayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorPaymentId,PaymentNumber,PaymentDate,purchaseOrderId,PaymentTypeId,PaymentAmount,EmployeeId,CashRepositoryId,createdAt")] VendorPayment vendorPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", vendorPayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", vendorPayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", vendorPayment.PaymentTypeId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrder, "purchaseOrderId", "purchaseOrderNumber", vendorPayment.purchaseOrderId);
            return View(vendorPayment);
        }

        // GET: VendorPayment/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorPayment = await _context.VendorPayment.SingleOrDefaultAsync(m => m.VendorPaymentId == id);
            if (vendorPayment == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", vendorPayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", vendorPayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", vendorPayment.PaymentTypeId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrder, "purchaseOrderId", "purchaseOrderNumber", vendorPayment.purchaseOrderId);
            return View(vendorPayment);
        }

        // POST: VendorPayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VendorPaymentId,PaymentNumber,PaymentDate,purchaseOrderId,PaymentTypeId,PaymentAmount,EmployeeId,CashRepositoryId,createdAt")] VendorPayment vendorPayment)
        {
            if (id != vendorPayment.VendorPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorPaymentExists(vendorPayment.VendorPaymentId))
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
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", vendorPayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", vendorPayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", vendorPayment.PaymentTypeId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrder, "purchaseOrderId", "purchaseOrderNumber", vendorPayment.purchaseOrderId);
            return View(vendorPayment);
        }

        // GET: VendorPayment/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorPayment = await _context.VendorPayment
                .Include(v => v.CashRepository)
                .Include(v => v.Employee)
                .Include(v => v.paymentType)
                .Include(v => v.purchaseOrder)
                .SingleOrDefaultAsync(m => m.VendorPaymentId == id);
            if (vendorPayment == null)
            {
                return NotFound();
            }

            return View(vendorPayment);
        }

        // POST: VendorPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vendorPayment = await _context.VendorPayment.SingleOrDefaultAsync(m => m.VendorPaymentId == id);
            _context.VendorPayment.Remove(vendorPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorPaymentExists(string id)
        {
            return _context.VendorPayment.Any(e => e.VendorPaymentId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class VendorPayment
        {
            public const string Controller = "VendorPayment";
            public const string Action = "Index";
            public const string Role = "VendorPayment";
            public const string Url = "/VendorPayment/Index";
            public const string Name = "VendorPayment";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Vendor")]
        public bool VendorPaymentRole { get; set; } = false;
    }
}
