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
            var applicationDbContext = _context.MoneyTransferOrder
                                            .Include(s => s.CashRepositoryFrom)
                                            .Include(x => x.CashRepositoryTo);
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                applicationDbContext = _context.MoneyTransferOrder
                                            .Include(s => s.CashRepositoryFrom)
                                            .Include(x => x.CashRepositoryTo);
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MoneyTransferOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneyTransferOrder = await _context.MoneyTransferOrder
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
            ViewData["CashRepositoryIdFrom"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["CashRepositoryIdTo"] = new SelectList(_context.CashRepository, "CashRepositoryId", "CashRepositoryName");
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            MoneyTransferOrder obj = new MoneyTransferOrder();
            return View(obj);
        }

        // POST: MoneyTransferOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoneyTransferOrderId,MoneyTransferOrderNumber,MoneyTransferOrderDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,PaymentAmount,MoneyTransferOrderStatus,HasChild,createdAt")] MoneyTransferOrder moneyTransferOrder)
        {
            if (ModelState.IsValid)
            {
                moneyTransferOrder.MoneyTransferOrderNumber = _numberSequence.GetNumberSequence("ΕΜΧ");
                _context.Add(moneyTransferOrder);
                await _context.SaveChangesAsync();
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

            return View(moneyTransferOrder);
        }

        // POST: MoneyTransferOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MoneyTransferOrderId,MoneyTransferOrderNumber,MoneyTransferOrderDate,Description,CashRepositoryIdFrom,CashRepositoryIdTo,PaymentAmount,MoneyTransferOrderStatus,HasChild,createdAt")] MoneyTransferOrder moneyTransferOrder)
        {
            if (id != moneyTransferOrder.MoneyTransferOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            var moneyTransferOrder = await _context.MoneyTransferOrder.SingleOrDefaultAsync(m => m.MoneyTransferOrderId == id);
            _context.MoneyTransferOrder.Remove(moneyTransferOrder);
            await _context.SaveChangesAsync();
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
