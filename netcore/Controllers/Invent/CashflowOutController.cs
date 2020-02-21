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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CashflowOut.Include(c => c.MoneyTransferOrder);
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
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowOutId == id);
            if (cashflowOut == null)
            {
                return NotFound();
            }

            return View(cashflowOut);
        }

        // GET: CashflowOut/Create
        public IActionResult Create()
        {
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId");
            return View();
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
                _context.Add(cashflowOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowOut.MoneyTransferOrderId);
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
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowOut.MoneyTransferOrderId);
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
            ViewData["MoneyTransferOrderId"] = new SelectList(_context.MoneyTransferOrder, "MoneyTransferOrderId", "MoneyTransferOrderId", cashflowOut.MoneyTransferOrderId);
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
                .Include(c => c.MoneyTransferOrder)
                .SingleOrDefaultAsync(m => m.CashflowOutId == id);
            if (cashflowOut == null)
            {
                return NotFound();
            }

            return View(cashflowOut);
        }

        // POST: CashflowOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashflowOut = await _context.CashflowOut.SingleOrDefaultAsync(m => m.CashflowOutId == id);
            _context.CashflowOut.Remove(cashflowOut);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
