using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models.Invent;

namespace netcore.Controllers.Invent
{
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
            var applicationDbContext = _context.CashRepository.Include(c => c.Employee);
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
                .Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.CashRepositoryId == id);
            if (cashRepository == null)
            {
                return NotFound();
            }

            return View(cashRepository);
        }

        // GET: CashRepository/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: CashRepository/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CashRepositoryId,CashRepositoryName,Description,TotalReceipts,TotalPayments,Balance,EmployeeId,createdAt")] CashRepository cashRepository)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashRepository);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", cashRepository.EmployeeId);
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", cashRepository.EmployeeId);
            return View(cashRepository);
        }

        // POST: CashRepository/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CashRepositoryId,CashRepositoryName,Description,TotalReceipts,TotalPayments,Balance,EmployeeId,createdAt")] CashRepository cashRepository)
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", cashRepository.EmployeeId);
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
