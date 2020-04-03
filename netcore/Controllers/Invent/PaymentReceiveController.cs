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
    [Authorize(Roles = "PaymentReceive")]
    public class PaymentReceiveController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public PaymentReceiveController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: PaymentReceive
        public async Task<IActionResult> Index(string id)
        {
           var username = HttpContext.User.Identity.Name;
           var applicationDbContext = _context.PaymentReceive
                .Include(e=>e.Employee)
                .Include(p => p.invoice)
                .Include(p => p.paymentType);
           if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                applicationDbContext = _context.PaymentReceive
                    .Where(s => s.Employee.UserName == username)
                    .OrderByDescending(x => x.createdAt)
                    .Include(e => e.Employee)
                    .Include(p => p.invoice)
                    .Include(p => p.paymentType);
            }
            if (id != null)
            {
                return View(await applicationDbContext.Where(x=>x.CashRepositoryId==id).ToListAsync());    
            }
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
                .Include(e=>e.Employee)
                .SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            if (paymentReceive == null)
            {
                return NotFound();
            }

            return View(paymentReceive);
        }

        // GET: PaymentReceive/Create
        public IActionResult Create(string id)
        {
            PaymentReceive pr = new PaymentReceive();             
            
            var username = HttpContext.User.Identity.Name;
            var invoice =(
            from Invoice in _context.Invoice
            select new
            {
                Invoice.InvoiceId,
                description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")"),
                Invoice.Paid,
                Invoice.InvoiceBalance,
           }).ToList();
            invoice.Insert(0,
             new
             {
                 InvoiceId= "0000",
                 description= "Επιλέξτε",
                 Paid=false,
                 InvoiceBalance=0.0m
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
                    EmployeeId="0000",
                    DisplayName="Επιλέξτε",
                    UserName="",
                    PaymentReceiver=true
                });

            if (id != null)
            {
                ViewData["InvoiceId"] = new SelectList(invoice, "InvoiceId", "description", id);
                pr.PaymentDate = DateTime.Today;
                pr.InvoiceId = id;
                pr.EmployeeId = _context.Invoice.Where(x => x.InvoiceId == id).Include(x=>x.Shipment).FirstOrDefault().Shipment.EmployeeId;
                pr.PaymentAmount = _context.Invoice.Where(x => x.InvoiceId == id).FirstOrDefault().InvoiceBalance;
            }
            else
            {
                ViewData["InvoiceId"] = new SelectList(invoice, "InvoiceId", "description");
            }

            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName");
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["EmployeeId"] = new SelectList(employee.Where(x => x.PaymentReceiver == true && x.UserName == username), "EmployeeId", "DisplayName");
            }
            else
            {
                ViewData["employeeId"] = new SelectList(employee.Where(x => x.PaymentReceiver == true), "EmployeeId", "DisplayName");
            }
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository.Where(x=>x.EmployeeId==pr.EmployeeId), "CashRepositoryId", "CashRepositoryName");

            return View(pr);
        }

        // POST: PaymentReceive/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentReceiveId,InvoiceId,IsFullPayment,PaymentAmount,PaymentDate,PaymentReceiveName,PaymentTypeId,createdAt,EmployeeId,CashRepositoryId")] PaymentReceive paymentReceive)
        {
            var username = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                paymentReceive.PaymentReceiveName = _numberSequence.GetNumberSequence("ΕΙΣ");
                
                _context.Add(paymentReceive);
                await _context.SaveChangesAsync();

                var invoice = await _context.Invoice
                .Include(i => i.Shipment)
                .Include(p => p.PaymentReceive)
                .SingleOrDefaultAsync(m => m.InvoiceId == paymentReceive.InvoiceId);

                var cashRepository = await _context.CashRepository.Where(x => x.CashRepositoryId == paymentReceive.CashRepositoryId).FirstOrDefaultAsync();
                cashRepository.TotalReceipts += paymentReceive.PaymentAmount;
                cashRepository.Balance = cashRepository.TotalReceipts - cashRepository.TotalPayments;
                _context.Update(cashRepository);
                await _context.SaveChangesAsync();
                invoice.totalPaymentReceive = invoice.PaymentReceive.Sum(x => x.PaymentAmount);
                invoice.InvoiceBalance = invoice.totalOrderAmount - invoice.totalPaymentReceive;

                if (invoice.InvoiceBalance==0)
                {
                    invoice.Paid = true; 
                }
                _context.Update(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Invoice", new { id = paymentReceive.InvoiceId });

                //return RedirectToAction(nameof(Index));
            }
            var query =
                from Invoice in _context.Invoice
                select new
                {
                    Invoice.InvoiceId,
                    description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")"),
                    Invoice.Paid
                };
            ViewData["InvoiceId"] = new SelectList(query.Where(x => x.Paid == false), "InvoiceId", "description", paymentReceive.InvoiceId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", paymentReceive.PaymentTypeId);
            ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");

            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(x => x.PaymentReceiver == true && x.UserName == username), "EmployeeId", "DisplayName");
                ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository.Where(x=>x.Employee.UserName==username), "CashRepositoryId", "CashRepositoryName");

            }
            return View(paymentReceive);
        }

        // GET: PaymentReceive/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var username = HttpContext.User.Identity.Name;
            var query =
                from Invoice in _context.Invoice
                select new
                {
                    Invoice.InvoiceId,
                    description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")"),
                    Invoice.Paid
                };
            if (id == null)
            {
                return NotFound();
            }

            var paymentReceive = await _context.PaymentReceive.SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
             ViewData["InvoiceId"] = new SelectList(query, "InvoiceId", "description");
           if (paymentReceive == null)
            {
                return NotFound();
            }
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(x => x.PaymentReceiver == true && x.UserName == username), "EmployeeId", "DisplayName");
                ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository.Where(x => x.Employee.UserName == username), "CashRepositoryId", "CashRepositoryName");

            }

            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", paymentReceive.PaymentTypeId);
            ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");            
            return View(paymentReceive);
        }

        // POST: PaymentReceive/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PaymentReceiveId,InvoiceId,IsFullPayment,PaymentAmount,PaymentDate,PaymentReceiveName,PaymentTypeId,createdAt,EmployeeId,CashRepositoryId")] PaymentReceive paymentReceive)
        {
            var username = HttpContext.User.Identity.Name;
            var query =
                from Invoice in _context.Invoice
                select new
                {
                    Invoice.InvoiceId,
                    description = (Invoice.InvoiceNumber + " (" + Invoice.customerName + ")"),
                    Invoice.Paid
                };
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

            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["EmployeeId"] = new SelectList(_context.Employee.Where(x => x.PaymentReceiver == true && x.UserName == username), "EmployeeId", "DisplayName");
                ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository.Where(x=>x.Employee.UserName==username), "CashRepositoryId", "CashRepositoryName", paymentReceive.CashRepositoryId);
            }

   
        ViewData["InvoiceId"] = new SelectList(query.Where(x => x.Paid == false), "InvoiceId", "description", paymentReceive.InvoiceId);
        ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", paymentReceive.PaymentTypeId);
        ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
        ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName",paymentReceive.CashRepositoryId);
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
                .Include(e=>e.Employee)
                .SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            if (paymentReceive == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", paymentReceive.CashRepositoryId);
            return View(paymentReceive);
        }

        // POST: PaymentReceive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var paymentReceive = await _context.PaymentReceive.SingleOrDefaultAsync(m => m.PaymentReceiveId == id);
            var invoice = await _context.Invoice.SingleOrDefaultAsync(m => m.InvoiceId == paymentReceive.InvoiceId);
            invoice.Paid = false;
            _context.PaymentReceive.Remove(paymentReceive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentReceiveExists(string id)
        {
            return _context.PaymentReceive.Any(e => e.PaymentReceiveId == id);
        }

        [HttpGet]
        public JsonResult GetEmployeeRepositories(string employeeId)
        {
            var EmployeeRepositorylist = new SelectList(_context.CashRepository.Where(c => c.EmployeeId == employeeId), "CashRepositoryId", "CashRepositoryName");
            return Json(EmployeeRepositorylist);

        }

        [HttpGet]
        public JsonResult GetInvoiceBalance(string invoiceId)
        {
            var invBalance = _context.Invoice.Where(c => c.InvoiceId == invoiceId).FirstOrDefault();
            var result = new
            {
                paymentAmount = invBalance.InvoiceBalance
            };
            return Json(result);

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
