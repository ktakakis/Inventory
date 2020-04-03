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
    [Authorize(Roles = "EmployeePayment")]
    public class EmployeePaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public EmployeePaymentController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: EmployeePayment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeePayment.Include(e => e.CashRepository).Include(e => e.Employee).Include(e => e.paymentType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeePayment/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayment = await _context.EmployeePayment
                .Include(e => e.CashRepository)
                .Include(e => e.Employee)
                .Include(e => e.paymentType)
                .SingleOrDefaultAsync(m => m.EmployeePaymentId == id);
            if (employeePayment == null)
            {
                return NotFound();
            }

            return View(employeePayment);
        }

        // GET: EmployeePayment/Create
        public IActionResult Create()
        {
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName");
            EmployeePayment ep = new EmployeePayment();
            ep.PaymentDate = DateTime.UtcNow;
            return View(ep);
        }

        // POST: EmployeePayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeePaymentId,PaymentNumber,PaymentDate,PaymentTypeId,PaymentAmount,EmployeeId,CashRepositoryId,createdAt")] EmployeePayment employeePayment)
        {
            var username = HttpContext.User.Identity.Name;
            var cashrepository = await _context.CashRepository.Where(x => x.CashRepositoryId == employeePayment.CashRepositoryId).FirstOrDefaultAsync();

            if (cashrepository.Balance < employeePayment.PaymentAmount)
            {
                TempData["StatusMessage"] = "Σφάλμα. Υπάρχει πρόβλημα στα ταμειακά διαθέσιμα. Το υπόλοιπο του ταμείου σας (" + cashrepository.Balance + "€), δεν επαρκεί για να καλύψει το ποσόν της πληρωμής(" + employeePayment.PaymentAmount + "€). Αλλάξτε ταμείο ή πληρώστε λιγότερα.";
                return RedirectToAction(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                employeePayment.PaymentNumber = _numberSequence.GetNumberSequence("ΠΛΣ");
                _context.Add(employeePayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", employeePayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", employeePayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", employeePayment.PaymentTypeId);
            return View(employeePayment);
        }

        // GET: EmployeePayment/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayment = await _context.EmployeePayment.SingleOrDefaultAsync(m => m.EmployeePaymentId == id);
            if (employeePayment == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", employeePayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", employeePayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", employeePayment.PaymentTypeId);
            return View(employeePayment);
        }

        // POST: EmployeePayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeePaymentId,PaymentNumber,PaymentDate,PaymentTypeId,PaymentAmount,EmployeeId,CashRepositoryId,createdAt")] EmployeePayment employeePayment)
        {
            if (id != employeePayment.EmployeePaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeePayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeePaymentExists(employeePayment.EmployeePaymentId))
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
            ViewData["CashRepositoryId"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", employeePayment.CashRepositoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", employeePayment.EmployeeId);
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "PaymentTypeName", employeePayment.PaymentTypeId);
            return View(employeePayment);
        }

        // GET: EmployeePayment/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePayment = await _context.EmployeePayment
                .Include(e => e.CashRepository)
                .Include(e => e.Employee)
                .Include(e => e.paymentType)
                .SingleOrDefaultAsync(m => m.EmployeePaymentId == id);
            if (employeePayment == null)
            {
                return NotFound();
            }

            return View(employeePayment);
        }

        // POST: EmployeePayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employeePayment = await _context.EmployeePayment.SingleOrDefaultAsync(m => m.EmployeePaymentId == id);
            _context.EmployeePayment.Remove(employeePayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeePaymentExists(string id)
        {
            return _context.EmployeePayment.Any(e => e.EmployeePaymentId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class EmployeePayment
        {
            public const string Controller = "EmployeePayment";
            public const string Action = "Index";
            public const string Role = "EmployeePayment";
            public const string Url = "/EmployeePayment/Index";
            public const string Name = "EmployeePayment";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Πληρωμές Συνεργατών")]
        public bool EmployeePaymentRole { get; set; } = false;
    }
}
