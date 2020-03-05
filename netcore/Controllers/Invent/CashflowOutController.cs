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
    [Authorize(Roles = "CashflowOut")]
    public class CashflowOutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashflowOutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CashflowOut
        public async Task<IActionResult> Index(string id)
        {
            var applicationDbContext = _context.CashflowOut
                .Include(x=>x.CashRepositoryFrom)
                .Include(x=>x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder);
            if (id != null)
            {
                applicationDbContext = _context.CashflowOut.Where(x => x.CashRepositoryIdFrom == id)
                .Include(x => x.CashRepositoryFrom)
                .Include(x => x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CashflowOut/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashflowOut = await _context.CashflowOut
                .Include(x=>x.CashRepositoryFrom)
                .Include(x => x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowOutId == id);
            if (cashflowOut == null)
            {
                return NotFound();
            }

            return View(cashflowOut);
        }

        // GET: CashflowOut/Create
        public IActionResult Create(string id)
        {
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            ViewData["moneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder.Where(x => x.MoneyTransferOrderStatus == MoneyTransferOrderStatus.Open && x.isIssued == false).ToList(), "MoneyTransferOrderId", "MoneyTransferOrderNumber");
            ViewData["cashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["cashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            CashflowOut obj = new CashflowOut();
            obj.MoneyTransferOrderId = id;
            return View(obj);
        }

        // POST: CashflowOut/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashflowOutId,MoneyTransferOrderId,CashflowOutNumber,CashflowOutDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,HasChild,createdAt")] CashflowOut cashflowOut)
        {
            if (ModelState.IsValid)
            {
                //check MoneyTransferOrder
                CashflowOut check = await _context.CashflowOut
                    .Include(x => x.MoneyTransferOrder)
                    .SingleOrDefaultAsync(x => x.MoneyTransferOrderId.Equals(cashflowOut.MoneyTransferOrderId));
                if (check != null)
                {
                    ViewData["StatusMessage"] = "Σφάλμα. Η εντολή εκροής έχει ήδη εκδοθεί. " + check.CashflowOutNumber;
                    ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder.ToList(), "MoneyTransferOrderId", "MoneyTransferOrderNumber");
                    ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
                    ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
                    return View(cashflowOut);
                }
                //check Balance
                bool isBalanceOK = true;

                MoneyTransferOrder mto = _context.MoneyTransferOrder.Where(x => x.MoneyTransferOrderId == cashflowOut.MoneyTransferOrderId).FirstOrDefault();
                 MoneyTransferOrder to = await _context.MoneyTransferOrder.Where(x => x.MoneyTransferOrderId.Equals(cashflowOut.MoneyTransferOrderId)).FirstOrDefaultAsync();
                cashflowOut.CashRepositoryIdFrom = to.CashRepositoryIdFrom;
                cashflowOut.CashRepositoryIdTo = to.CashRepositoryIdTo;

                cashflowOut.CashRepositoryFrom = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(cashflowOut.CashRepositoryIdFrom));
                cashflowOut.CashRepositoryTo = await _context.CashRepository.Include(x => x.Employee).SingleOrDefaultAsync(x => x.CashRepositoryId.Equals(cashflowOut.CashRepositoryIdTo));
                to.isIssued = true;

                CashRepository cashrepository =await  _context.CashRepository.Where(x => x.CashRepositoryId == cashflowOut.CashRepositoryIdFrom).FirstAsync();
                if (mto != null && cashrepository != null)
                {
                    if (cashrepository.Balance < mto.PaymentAmount)
                    {
                        isBalanceOK = false;
                    }
                }
                else
                {
                    isBalanceOK = false;
                }


                if (!isBalanceOK)
                {
                    TempData["StatusMessage"] = "Σφάλμα. Υπάρχει πρόβλημα στα ταμειακά διαθέσιμα, το ταμείο σας έχει " + cashrepository.Balance + " €";
                    return RedirectToAction(nameof(Create));
                }
                _context.Add(cashflowOut);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η ταμειακή εκροή " + cashflowOut.CashflowOutNumber + " έγινε με Επιτυχία!";
                return RedirectToAction("Details","CashRepository", new { id = cashflowOut.CashRepositoryIdTo });
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderNumber", cashflowOut.MoneyTransferOrderId);
            return View(cashflowOut);
        }

        // GET: CashflowOut/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashflowOut = await _context.CashflowOut.SingleOrDefaultAsync(m => m.CashflowOutId == id);
            if (cashflowOut == null)
            {
                return NotFound();
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderNumber", cashflowOut.MoneyTransferOrderId);
            return View(cashflowOut);
        }

        // POST: CashflowOut/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CashflowOutId,MoneyTransferOrderId,CashflowOutNumber,CashflowOutDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,HasChild,createdAt")] CashflowOut cashflowOut)
        {
            if (id != cashflowOut.CashflowOutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashflowOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashflowOutExists(cashflowOut.CashflowOutId))
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
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderNumber", cashflowOut.MoneyTransferOrderId);
            return View(cashflowOut);
        }

        // GET: CashflowOut/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashflowOut = await _context.CashflowOut
                .Include(x=>x.CashRepositoryFrom)
                .Include(x=>x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowOutId == id);
            if (cashflowOut == null)
            {
                return NotFound();
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderNumber", cashflowOut.MoneyTransferOrderId);
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");

            return View(cashflowOut);
        }

        // POST: CashflowOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashflowOut = await _context.CashflowOut
                .Include(x=>x.CashRepositoryFrom)
                .Include(x=>x.CashRepositoryTo)
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowOutId == id);

            if (cashflowOut.MoneyTransferOrder.isReceived == true)
            {
                ViewData["StatusMessage"] = "Σφάλμα. Παρακαλώ πρώτα διαγράψτε τις εγγραφές των χρημάτων που έχετε πάρει.";
                return View(cashflowOut);
            }

            try
            {
                _context.CashflowOut.Remove(cashflowOut);
                cashflowOut.MoneyTransferOrder.isIssued = false;
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Διαγραφή της εγγραφής μεταφοράς χρημάτων, " + cashflowOut.CashflowOutNumber + ", έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(cashflowOut);
            }
        }

        private bool CashflowOutExists(string id)
        {
            return _context.CashflowOut.Any(e => e.CashflowOutId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class CashflowOut
        {
            public const string Controller = "CashflowOut";
            public const string Action = "Index";
            public const string Role = "CashflowOut";
            public const string Url = "/CashflowOut/Index";
            public const string Name = "CashflowOut";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "CashflowOut")]
        public bool CashflowOutRole { get; set; } = false;
    }
}
