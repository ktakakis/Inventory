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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CashflowIn.Include(c => c.MoneyTransferOrder);
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
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId");
            return View();
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
                _context.Add(cashflowIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var cashflowIn = await _context.CashflowIn.SingleOrDefaultAsync(m => m.CashflowInId == id);
            if (cashflowIn == null)
            {
                return NotFound();
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowIn.MoneyTransferOrderId);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowIn.MoneyTransferOrderId);
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
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowInId == id);
            if (cashflowIn == null)
            {
                return NotFound();
            }

            return View(cashflowIn);
        }

        // POST: CashflowIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashflowIn = await _context.CashflowIn.SingleOrDefaultAsync(m => m.CashflowInId == id);
            _context.CashflowIn.Remove(cashflowIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
