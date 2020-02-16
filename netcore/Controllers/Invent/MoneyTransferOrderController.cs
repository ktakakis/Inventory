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
    [Authorize(Roles = "MoneyTransferOrder")]
    public class MoneyTransferOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoneyTransferOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MoneyTransferOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoneyTransferOrder.ToListAsync());
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

            return View(moneyTransferOrder);
        }

        // GET: MoneyTransferOrder/Create
        public IActionResult Create()
        {
            return View();
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
