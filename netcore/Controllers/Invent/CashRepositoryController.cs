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
    [Authorize(Roles = "CashRepository")]
    public class CashRepositoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashRepositoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CashRepository
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CashRepository.Include(x=>x.EmployeePayment).Include(c => c.Employee).ThenInclude(x=>x.PaymentReceive);
            
            foreach (var cashRepository in applicationDbContext)
            {
                var mto = _context.MoneyTransferOrder.Where(x => x.CashRepositoryIdFrom == cashRepository.CashRepositoryId && x.MoneyTransferOrderStatus == MoneyTransferOrderStatus.Completed);
                var mti = _context.MoneyTransferOrder.Where(x => x.CashRepositoryIdTo == cashRepository.CashRepositoryId && x.MoneyTransferOrderStatus == MoneyTransferOrderStatus.Completed);
                var vp = _context.VendorPayment.Where(x=>x.CashRepositoryId==cashRepository.CashRepositoryId);
                var ep = _context.EmployeePayment.Where(x => x.CashRepositoryId == cashRepository.CashRepositoryId);
                cashRepository.TotalReceipts = cashRepository.Employee.PaymentReceive.Sum(x => x.PaymentAmount);
                cashRepository.TotalCashflowOut = mto.Sum(x => x.PaymentAmount);
                cashRepository.TotalCashflowIn = mti.Sum(x => x.PaymentAmount);
                cashRepository.TotalPayments = vp.Sum(x => x.PaymentAmount);
                cashRepository.TotalEmployeePayments = ep.Sum(x => x.PaymentAmount);
                cashRepository.Balance = cashRepository.TotalReceipts + cashRepository.TotalCashflowIn - cashRepository.TotalCashflowOut- cashRepository.TotalPayments-cashRepository.TotalEmployeePayments;
                _context.Update(cashRepository);
                await _context.SaveChangesAsync();
            }

            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                applicationDbContext = _context.CashRepository.Where(x => x.Employee.UserName == username).Include(c => c.Employee).ThenInclude(x => x.PaymentReceive);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CashRepository/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashRepository = await _context.CashRepository
                .Include(x => x.EmployeePayment)
                .Include(c => c.Employee).ThenInclude(x=>x.PaymentReceive)
                .SingleOrDefaultAsync(m => m.CashRepositoryId == id);
            if (cashRepository == null)
            {
                return NotFound();
            }
            var employee = (from Employee in _context.Employee
                            select new
                            {
                                Employee.EmployeeId,
                                Employee.DisplayName
                            }).ToList();
            ViewData["EmployeeId"] = new SelectList(employee, "EmployeeId", "DisplayName", cashRepository.EmployeeId);
            ViewData["IsCashflowIn"] = true;
            await _context.SaveChangesAsync();
            return View(cashRepository);
        }

        // GET: CashRepository/Create
        public IActionResult Create()
        {
            var employee = (from Employee in _context.Employee
                            select new
                            {
                                Employee.EmployeeId,
                                Employee.DisplayName
                            }).ToList();
            employee.Insert(0,
                new
                {
                    EmployeeId = "0000",
                    DisplayName = "Επιλέξτε"
                });

            ViewData["EmployeeId"] = new SelectList(employee, "EmployeeId", "DisplayName");
            return View();
        }

        // POST: CashRepository/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashRepositoryId,Balance,CashRepositoryName,Description,EmployeeId,TotalPayments,TotalReceipts,createdAt,TotalCashflowIn,TotalCashflowOut,MainRepository,TotalEmployeePayments")] CashRepository cashRepository)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashRepository);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", cashRepository.EmployeeId);
            return View(cashRepository);
        }

        // GET: CashRepository/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashRepository = await _context.CashRepository.SingleOrDefaultAsync(m => m.CashRepositoryId == id);
            if (cashRepository == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", cashRepository.EmployeeId);
            return View(cashRepository);
        }

        // POST: CashRepository/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CashRepositoryId,Balance,CashRepositoryName,Description,EmployeeId,TotalPayments,TotalReceipts,createdAt,TotalCashflowIn,TotalCashflowOut,MainRepository,TotalEmployeePayments")] CashRepository cashRepository)
        {
            if (id != cashRepository.CashRepositoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashRepository);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashRepositoryExists(cashRepository.CashRepositoryId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName", cashRepository.EmployeeId);
            return View(cashRepository);
        }

        // GET: CashRepository/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashRepository = await _context.CashRepository
                .Include(x => x.EmployeePayment)
                .Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.CashRepositoryId == id);
            if (cashRepository == null)
            {
                return NotFound();
            }

            return View(cashRepository);
        }

        // POST: CashRepository/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashRepository = await _context.CashRepository.SingleOrDefaultAsync(m => m.CashRepositoryId == id);
            _context.CashRepository.Remove(cashRepository);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashRepositoryExists(string id)
        {
            return _context.CashRepository.Any(e => e.CashRepositoryId == id);
        }
    }
}


namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class CashRepository
        {
            public const string Controller = "CashRepository";
            public const string Action = "Index";
            public const string Role = "CashRepository";
            public const string Url = "/CashRepository/Index";
            public const string Name = "CashRepository";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Ταμείο")]
        public bool CashRepositoryRole { get; set; } = false;
    }
}
