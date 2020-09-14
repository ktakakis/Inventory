using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using netcore.Data;
using netcore.Models.Invent;

namespace netcore.Controllers
{
    [Authorize(Roles = "RoastingLog")]
    public class RoastingLogController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RoastingLogController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: RoastingLog
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoastingLog.Include(r => r.Product).Include(r => r.RoastingLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoastingLog/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roastingLog = await _context.RoastingLog
                .Include(r => r.Product)
                .Include(r => r.RoastingLevel)
                .SingleOrDefaultAsync(m => m.RoastingLogId == id);
            if (roastingLog == null)
            {
                return NotFound();
            }

            return View(roastingLog);
        }

        // GET: RoastingLog/Create
        public IActionResult Create()
        {
            RoastingLog roastLog = new RoastingLog();
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId");
            ViewData["RoastingLevelId"] = new SelectList(_context.RoastingLevel, "RoastingLevelId", "RoastingLevelId");
            return View(roastLog);
        }

        // POST: RoastingLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoastingLogId,RoasterName,RoastingLevelId,RoastingDate,StartTime,EndTime,StartWeight,FinishWeight,LossPercent,ProductId,StartTemp,AmbientTemp,AfterFillingTemp,FirstCrackTime,SecondCrackTime,HasChild,createdAt")] RoastingLog roastingLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roastingLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId", roastingLog.ProductId);
            ViewData["RoastingLevelId"] = new SelectList(_context.RoastingLevel, "RoastingLevelId", "RoastingLevelId", roastingLog.RoastingLevelId);
            return View(roastingLog);
        }

        // GET: RoastingLog/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roastingLog = await _context.RoastingLog.SingleOrDefaultAsync(m => m.RoastingLogId == id);
            if (roastingLog == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId", roastingLog.ProductId);
            ViewData["RoastingLevelId"] = new SelectList(_context.RoastingLevel, "RoastingLevelId", "RoastingLevelId", roastingLog.RoastingLevelId);
            return View(roastingLog);
        }

        // POST: RoastingLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RoastingLogId,RoasterName,RoastingLevelId,RoastingDate,StartTime,EndTime,StartWeight,FinishWeight,LossPercent,ProductId,StartTemp,AmbientTemp,AfterFillingTemp,FirstCrackTime,SecondCrackTime,HasChild,createdAt")] RoastingLog roastingLog)
        {
            if (id != roastingLog.RoastingLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roastingLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoastingLogExists(roastingLog.RoastingLogId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "productId", "productId", roastingLog.ProductId);
            ViewData["RoastingLevelId"] = new SelectList(_context.RoastingLevel, "RoastingLevelId", "RoastingLevelId", roastingLog.RoastingLevelId);
            return View(roastingLog);
        }

        // GET: RoastingLog/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roastingLog = await _context.RoastingLog
                .Include(r => r.Product)
                .Include(r => r.RoastingLevel)
                .SingleOrDefaultAsync(m => m.RoastingLogId == id);
            if (roastingLog == null)
            {
                return NotFound();
            }

            return View(roastingLog);
        }

        // POST: RoastingLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roastingLog = await _context.RoastingLog.SingleOrDefaultAsync(m => m.RoastingLogId == id);
            _context.RoastingLog.Remove(roastingLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoastingLogExists(string id)
        {
            return _context.RoastingLog.Any(e => e.RoastingLogId == id);
        }
    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class RoastingLog
        {
            public const string Controller = "RoastingLog";
            public const string Action = "Index";
            public const string Role = "RoastingLog";
            public const string Url = "/RoastingLog/Index";
            public const string Name = "RoastingLog";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "RoastingLog")]
        public bool RoastingLogRole { get; set; } = false;
    }
}

