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
    [Authorize(Roles = "CashflowIn")]

    public class CashflowInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashflowInController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CashflowIn
        public async Task<IActionResult> Index(string id)
        {
            var applicationDbContext = _context.CashflowIn
                .Include(x => x.CashRepositoryFrom)
                .Include(x => x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder);
            if (id != null)
            {
                applicationDbContext = _context.CashflowIn.Where(x=>x.CashRepositoryIdTo==id)
                .Include(x => x.CashRepositoryFrom)
                .Include(x => x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CashflowIn/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashflowIn = await _context.CashflowIn
                .Include(c => c.MoneyTransferOrder)
                .Include(x=>x.CashRepositoryFrom)
                .Include(x => x.CashRepositoryTo)
                .SingleOrDefaultAsync(m => m.CashflowInId == id);
            if (cashflowIn == null)
            {
                return NotFound();
            }

            return View(cashflowIn);
        }

        // GET: CashflowIn/Create
        public IActionResult Create()
        {
            ViewData["moneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder.Where(x => x.MoneyTransferOrderStatus == MoneyTransferOrderStatus.Open && x.isIssued == true).ToList(), "MoneyTransferOrderId", "MoneyTransferOrderNumber");
            ViewData["cashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["cashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            CashflowIn obj = new CashflowIn();
            return View(obj);
        }

        // POST: CashflowIn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashflowInId,MoneyTransferOrderId,CashflowInNumber,CashflowInDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,HasChild,createdAt")] CashflowIn cashflowIn)
        {
            if (ModelState.IsValid)
            {
                //check MoneyTransferOrder
                CashflowIn check = await _context.CashflowIn.SingleOrDefaultAsync(x => x.MoneyTransferOrderId.Equals(cashflowIn.MoneyTransferOrderId));
                if (check != null)
                {
                    ViewData["StatusMessage"] = "Σφάλμα. Η εντολή μεταφοράς χρημάτων έχει ήδη δημιουργηθεί. " + check.CashflowInNumber;
                    ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder.ToList(), "MoneyTransferOrderId", "MoneyTransferOrderNumber");
                    ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
                    ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
                    return View(cashflowIn);
                }

                MoneyTransferOrder to = await _context.MoneyTransferOrder.Where(x => x.MoneyTransferOrderId.Equals(cashflowIn.MoneyTransferOrderId)).FirstOrDefaultAsync();
                cashflowIn.CashRepositoryIdFrom = to.CashRepositoryIdFrom;
                cashflowIn.CashRepositoryIdTo = to.CashRepositoryIdTo;

                cashflowIn.CashRepositoryFrom = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(cashflowIn.CashRepositoryIdFrom));
                cashflowIn.CashRepositoryTo = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(cashflowIn.CashRepositoryIdTo));

                to.isReceived = true;
                to.MoneyTransferOrderStatus = MoneyTransferOrderStatus.Completed;

                _context.Add(cashflowIn);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η εγγραφή Εισροής " + cashflowIn.CashflowInNumber + " έγινε με Επιτυχία!";
                return RedirectToAction("Details", "CashRepository", new { id = cashflowIn.CashRepositoryIdTo });
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowIn.MoneyTransferOrderId);
            return View(cashflowIn);
        }

        // GET: CashflowIn/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashflowIn = await _context.CashflowIn
                .Include(x=>x.CashRepositoryFrom)
                .Include(x=>x.CashRepositoryTo)
                .SingleOrDefaultAsync(m => m.CashflowInId == id);
            if (cashflowIn == null)
            {
                return NotFound();
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowIn.MoneyTransferOrderId);
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            return View(cashflowIn);
        }

        // POST: CashflowIn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CashflowInId,MoneyTransferOrderId,CashflowInNumber,CashflowInDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,HasChild,createdAt")] CashflowIn cashflowIn)
        {
            if (id != cashflowIn.CashflowInId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashflowIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashflowInExists(cashflowIn.CashflowInId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["TransMessage"] = "Η Επεξεργασία της εγγραφής παραλαβής," + cashflowIn.CashflowInNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowIn.MoneyTransferOrderId);
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            return View(cashflowIn);
        }

        // GET: CashflowIn/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashflowIn = await _context.CashflowIn
                .Include(c=>c.CashRepositoryFrom)
                .Include(c=>c.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowInId == id);
            if (cashflowIn == null)
            {
                return NotFound();
            }
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            return View(cashflowIn);
        }

        // POST: CashflowIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashflowIn = await _context.CashflowIn
                .Include(x=>x.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowInId == id);
            try
            {
                _context.CashflowIn.Remove(cashflowIn);
                cashflowIn.MoneyTransferOrder.MoneyTransferOrderStatus = MoneyTransferOrderStatus.Open;
                cashflowIn.MoneyTransferOrder.isReceived = false;
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Διαγραφή της εγγραφής παραλαβής," + cashflowIn.CashflowInNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(cashflowIn);
            }
        }

        private bool CashflowInExists(string id)
        {
            return _context.CashflowIn.Any(e => e.CashflowInId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class CashflowIn
        {
            public const string Controller = "CashflowIn";
            public const string Action = "Index";
            public const string Role = "CashflowIn";
            public const string Url = "/CashflowIn/Index";
            public const string Name = "CashflowIn";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "CashflowIn")]
        public bool CashflowInRole { get; set; } = false;
    }
}
