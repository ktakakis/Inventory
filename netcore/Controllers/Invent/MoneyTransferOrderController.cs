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
    [Authorize(Roles = "MoneyTransferOrder")]
    public class MoneyTransferOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetcoreService _netcoreService;
        private readonly INumberSequence _numberSequence;


        public MoneyTransferOrderController(ApplicationDbContext context, INetcoreService netcoreService,
                        INumberSequence numberSequence)
        {
            _context = context;
            _netcoreService = netcoreService;
            _numberSequence = numberSequence;
        }

        // GET: MoneyTransferOrder
        public async Task<IActionResult> Index()
        {
            List<MoneyTransferOrder> lists = new List<MoneyTransferOrder>();
            lists = await _context.MoneyTransferOrder.OrderByDescending(x => x.createdAt)
                .Include(x => x.CashRepositoryFrom)
                .Include(x => x.CashRepositoryTo)
                .ToListAsync();
            return View(lists);
        }

        // GET: MoneyTransferOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransferOrder = await _context.MoneyTransferOrder
                .Include(x=>x.CashRepositoryFrom)
                .Include(x=>x.CashRepositoryTo)
                .SingleOrDefaultAsync(m => m.MoneyTransferOrderId == id);
            if (moneyTransferOrder == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", id);
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", id);

            return View(moneyTransferOrder);
        }

        // GET: MoneyTransferOrder/Create
        public IActionResult Create()
        {
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            MoneyTransferOrder obj = new MoneyTransferOrder();
            return View(obj);
        }

        // POST: MoneyTransferOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoneyTransferOrderId,CashRepositoryFromCashRepositoryId,CashRepositoryIdFrom,CashRepositoryIdTo,CashRepositoryToCashRepositoryId,Description,HasChild,MoneyTransferOrderDate,MoneyTransferOrderNumber,MoneyTransferOrderStatus,PaymentAmount,createdAt,isIssued,isReceived")] MoneyTransferOrder moneyTransferOrder)
        {
            if (moneyTransferOrder.CashRepositoryIdFrom == moneyTransferOrder.CashRepositoryIdTo)
            {
                TempData["StatusMessage"] = "Σφάλμα. Το Ταμείο από και προς είναι το ίδιο. Η μεταφορά χρημάτων γίνεται μεταξύ διαφορετικών ταμείων";
                return RedirectToAction(nameof(Create));
            }


            if (ModelState.IsValid)
            {
                moneyTransferOrder.CashRepositoryFrom = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(moneyTransferOrder.CashRepositoryIdFrom));
                moneyTransferOrder.CashRepositoryTo = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(moneyTransferOrder.CashRepositoryIdTo));

                moneyTransferOrder.MoneyTransferOrderNumber = _numberSequence.GetNumberSequence("ΕΜΧ");
                _context.Add(moneyTransferOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Εντολή Μεταφοράς χρημάτων " + moneyTransferOrder.MoneyTransferOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }

            return View(moneyTransferOrder);
        }

        // GET: MoneyTransferOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var moneyTransferOrder = await _context.MoneyTransferOrder.SingleOrDefaultAsync(m => m.MoneyTransferOrderId == id);
            if (moneyTransferOrder == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", id);
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName", id);
            TempData["MoneyTransferOrderStatus"] = moneyTransferOrder.MoneyTransferOrderStatus;
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            return View(moneyTransferOrder);
        }

        // POST: MoneyTransferOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MoneyTransferOrderId,MoneyTransferOrderNumber,MoneyTransferOrderDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,PaymentAmount,MoneyTransferOrderStatus,isIssued,isReceived,HasChild,createdAt")] MoneyTransferOrder moneyTransferOrder)
        {
            if (id != moneyTransferOrder.MoneyTransferOrderId)
            {
                return NotFound();
            }
            if ((MoneyTransferOrderStatus)TempData["MoneyTransferOrderStatus"] == MoneyTransferOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία [Ολοκληρωμένης] παραγγελίας.";
                return RedirectToAction(nameof(Edit), new { id = moneyTransferOrder.MoneyTransferOrderId });
            }

            if (moneyTransferOrder.MoneyTransferOrderStatus == MoneyTransferOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία της κατάστασης όταν είναι [Ολοκληρωμένη] η Παραγγελία.";
                return RedirectToAction(nameof(Edit), new { id = moneyTransferOrder.MoneyTransferOrderId});
            }

            if (moneyTransferOrder.isIssued == true
            || moneyTransferOrder.isReceived == true)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία της εντολής με κατάσταση [Ανοιχτή] που ήδη επεξεργάζεται την Αποστολή ή την Παραλαβή χρημάτων.";
                return RedirectToAction(nameof(Edit), new { id = moneyTransferOrder.MoneyTransferOrderId });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    moneyTransferOrder.CashRepositoryFrom = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(moneyTransferOrder.CashRepositoryIdFrom));
                    moneyTransferOrder.CashRepositoryTo = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(moneyTransferOrder.CashRepositoryIdTo));
                  _context.Update(moneyTransferOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyTransferOrderExists(moneyTransferOrder.MoneyTransferOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["TransMessage"] = "Η Επεξεργασία της Μεταφοράς " + moneyTransferOrder.MoneyTransferOrderNumber + " έγινε με Επιτυχία!";
                return RedirectToAction(nameof(Index));
            }
            return View(moneyTransferOrder);
        }


        // GET: MoneyTransferOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransferOrder = await _context.MoneyTransferOrder
                .Include(x=>x.CashRepositoryFrom)
                .Include(x=>x.CashRepositoryTo)
                .SingleOrDefaultAsync(m => m.MoneyTransferOrderId == id);
            if (moneyTransferOrder == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName",id);
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName",id);

            return View(moneyTransferOrder);
        }

        // POST: MoneyTransferOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var moneyTransferOrder = await _context.MoneyTransferOrder
                .SingleOrDefaultAsync(m => m.MoneyTransferOrderId == id);
            _context.MoneyTransferOrder.Remove(moneyTransferOrder);
            await _context.SaveChangesAsync();
            TempData["TransMessage"] = "Η Διαγραφή της Μεταφοράς " + moneyTransferOrder.MoneyTransferOrderNumber + " έγινε με Επιτυχία";
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyTransferOrderExists(string id)
        {
            return _context.MoneyTransferOrder.Any(e => e.MoneyTransferOrderId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class MoneyTransferOrder
        {
            public const string Controller = "MoneyTransferOrder";
            public const string Action = "Index";
            public const string Role = "MoneyTransferOrder";
            public const string Url = "/MoneyTransferOrder/Index";
            public const string Name = "MoneyTransferOrder";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "MoneyTransferOrder")]
        public bool MoneyTransferOrderRole { get; set; } = false;
    }
}
