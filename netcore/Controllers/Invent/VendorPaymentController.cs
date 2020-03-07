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
    [Authorize(Roles = "VendorPayment")]
    public class VendorPaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public VendorPaymentController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: VendorPayment
        public async Task<IActionResult> Index(string id)
        {
            var applicationDbContext = _context.VendorPayment
                .Include(v => v.CashRepository)
                .Include(v => v.Employee)
                .Include(v => v.paymentType)
                .Include(v => v.purchaseOrder);
            if (id != null)
            {
                applicationDbContext = _context.VendorPayment.Where(x => x.CashRepositoryId == id)
                .Include(v => v.CashRepository)
                .Include(v => v.Employee)
                .Include(v => v.paymentType)
                .Include(v => v.purchaseOrder);
            }
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
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", vendorPayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", vendorPayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", vendorPayment.PaymentTypeId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrder, "purchaseOrderId", "purchaseOrderNumber", vendorPayment.purchaseOrderId);

            return View(vendorPayment);
        }

        // GET: VendorPayment/Create
        public IActionResult Create(string id)
        {
            VendorPayment vp = new VendorPayment();
            var username = HttpContext.User.Identity.Name;

            var purchaseorder = (
            from PurchaseOrder in _context.PurchaseOrder
            select new
            {
                PurchaseOrder.purchaseOrderId,
                description = (PurchaseOrder.purchaseOrderNumber + " (" + PurchaseOrder.vendor.vendorName + ")"),
                PurchaseOrder.Paid,
                PurchaseOrder.InvoiceBalance,
            }).ToList();
            purchaseorder.Insert(0,
             new
             {
                 purchaseOrderId = "0000",
                 description = "Επιλέξτε",
                 Paid = false,
                 InvoiceBalance = 0.0m
             });
            var employee = (from Employee in _context.Employee
                            select new
                            {
                                Employee.EmployeeId,
                                Employee.DisplayName,
                                Employee.UserName,
                                Employee.PaymentReceiver
                            }).ToList();
            employee.Insert(0,
                new
                {
                    EmployeeId = "0000",
                    DisplayName = "Επιλέξτε",
                    UserName = "",
                    PaymentReceiver = true
                });
            if (id != null)
            {
                ViewData["purchaseOrderId"] = new SelectList(purchaseorder, "purchaseOrderId", "description", id);
                vp.PaymentDate = DateTime.Today;
                vp.purchaseOrderId = id;
                vp.EmployeeId = _context.Invoice.Where(x => x.InvoiceId == id).Include(x => x.Shipment).FirstOrDefault().Shipment.EmployeeId;
                vp.PaymentAmount = _context.PurchaseOrder.Where(x => x.purchaseOrderId == id).FirstOrDefault().InvoiceBalance;

            }
            else
            {
                ViewData["purchaseOrderId"] = new SelectList(purchaseorder, "purchaseOrderId", "description");
            }

            var cashrepository = _context.CashRepository.Include(x=>x.Employee).Where(x => x.MainRepository == true).FirstOrDefault();
            List<PurchaseOrder> poList = _context.PurchaseOrder.Where(x => x.purchaseOrderStatus == PurchaseOrderStatus.Completed && x.Paid==false).ToList();
            poList.Insert(0, new PurchaseOrder { purchaseOrderId = "0", purchaseOrderNumber = "Επιλέξτε" });
            
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName",cashrepository.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName",cashrepository.Employee.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName");
            ViewData["purchaseOrderId"] = new SelectList(poList, "purchaseOrderId", "purchaseOrderNumber");
            vp.PaymentDate = DateTime.Today;
            return View(vp);
        }

        // POST: VendorPayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorPaymentId,CashRepositoryId,EmployeeId,PaymentAmount,PaymentDate,PaymentNumber,PaymentTypeId,createdAt,purchaseOrderId")] VendorPayment vendorPayment)
        {
            var username = HttpContext.User.Identity.Name;

            if (ModelState.IsValid)
            {
                vendorPayment.PaymentNumber = _numberSequence.GetNumberSequence("ΠΛΠ");
                _context.Add(vendorPayment);
                await _context.SaveChangesAsync();
                var purchaseorder = await _context.PurchaseOrder
                    .Include(p => p.vendorPayment)
                    .SingleOrDefaultAsync(m => m.purchaseOrderId == vendorPayment.purchaseOrderId);

                var cashRepository = await _context.CashRepository.Where(x => x.CashRepositoryId == vendorPayment.CashRepositoryId).FirstOrDefaultAsync();
                cashRepository.TotalReceipts += vendorPayment.PaymentAmount;
                cashRepository.Balance = cashRepository.TotalReceipts - cashRepository.TotalPayments;
                _context.Update(cashRepository);
                await _context.SaveChangesAsync();
                purchaseorder.totalVendorPayment = purchaseorder.vendorPayment.Sum(x => x.PaymentAmount);
                purchaseorder.InvoiceBalance = purchaseorder.totalOrderAmount - purchaseorder.totalVendorPayment;

                if (purchaseorder.InvoiceBalance == 0)
                {
                    purchaseorder.Paid = true;
                }
                _context.Update(purchaseorder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "purchaseOrder", new { id = vendorPayment.purchaseOrderId });
            }
            var query =
                from PurchaseOrder in _context.PurchaseOrder
                select new
                {
                    PurchaseOrder.purchaseOrderId,
                    description = (PurchaseOrder.purchaseOrderNumber + " (" + PurchaseOrder.vendor.vendorName + ")"),
                    PurchaseOrder.Paid
                };
            ViewData["purchaseOrderId"] = new SelectList(query.Where(x => x.Paid == false), "purchaseOrderId", "description", vendorPayment.purchaseOrderId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", vendorPayment.PaymentTypeId);
            ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");

            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(x => x.PaymentReceiver == true && x.UserName == username), "EmployeeId", "DisplayName");
                ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository.Where(x => x.Employee.UserName == username), "CashRepositoryId", "CashRepositoryName");

            }
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
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrder.Where(x => x.purchaseOrderStatus == PurchaseOrderStatus.Completed && x.Paid == false).ToList(), "purchaseOrderId", "purchaseOrderNumber", vendorPayment.purchaseOrderId);
            return View(vendorPayment);
        }

        // POST: VendorPayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VendorPaymentId,CashRepositoryId,EmployeeId,PaymentAmount,PaymentDate,PaymentNumber,PaymentTypeId,createdAt,purchaseOrderId")] VendorPayment vendorPayment)
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
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", vendorPayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", vendorPayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", vendorPayment.PaymentTypeId);
            ViewData["purchaseOrderId"] = new SelectList(_context.PurchaseOrder, "purchaseOrderId", "purchaseOrderNumber", vendorPayment.purchaseOrderId);
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

        [HttpGet]
        public JsonResult GetEmployeeRepositories(string employeeId)
        {
            var EmployeeRepositorylist = new SelectList(_context.CashRepository.Where(c => c.EmployeeId == employeeId), "CashRepositoryId", "CashRepositoryName");
            return Json(EmployeeRepositorylist);

        }

        [HttpGet]
        public JsonResult GetPurchaseOrderBalance(string purchaseOrderId)
        {
            var poBalance = _context.PurchaseOrder.Where(c => c.purchaseOrderId == purchaseOrderId).FirstOrDefault();
            var result = new
            {
                paymentAmount = poBalance.totalOrderAmount
            };
            return Json(result);

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
