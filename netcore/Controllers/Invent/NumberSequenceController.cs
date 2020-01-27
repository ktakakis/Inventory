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
    [Authorize(Roles = "NumberSequence")]
    public class NumberSequenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NumberSequenceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NumberSequence
        public async Task<IActionResult> Index()
        {
            return View(await _context.NumberSequence.ToListAsync());
        }

        // GET: NumberSequence/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberSequence = await _context.NumberSequence
                .SingleOrDefaultAsync(m => m.NumberSequenceId == id);
            if (numberSequence == null)
            {
                return NotFound();
            }

            return View(numberSequence);
        }

        // GET: NumberSequence/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NumberSequence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberSequenceId,NumberSequenceName,Module,Prefix,LastNumber,MyProperty,createdAt")] NumberSequence numberSequence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(numberSequence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(numberSequence);
        }

        // GET: NumberSequence/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberSequence = await _context.NumberSequence.SingleOrDefaultAsync(m => m.NumberSequenceId == id);
            if (numberSequence == null)
            {
                return NotFound();
            }
            return View(numberSequence);
        }

        // POST: NumberSequence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumberSequenceId,NumberSequenceName,Module,Prefix,LastNumber,MyProperty,createdAt")] NumberSequence numberSequence)
        {
            if (id != numberSequence.NumberSequenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(numberSequence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NumberSequenceExists(numberSequence.NumberSequenceId))
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
            return View(numberSequence);
        }

        // GET: NumberSequence/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var numberSequence = await _context.NumberSequence
                .SingleOrDefaultAsync(m => m.NumberSequenceId == id);
            if (numberSequence == null)
            {
                return NotFound();
            }

            return View(numberSequence);
        }

        // POST: NumberSequence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var numberSequence = await _context.NumberSequence.SingleOrDefaultAsync(m => m.NumberSequenceId == id);
            _context.NumberSequence.Remove(numberSequence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NumberSequenceExists(int id)
        {
            return _context.NumberSequence.Any(e => e.NumberSequenceId == id);
        }
    }
}
namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class NumberSequence
        {
            public const string Controller = "NumberSequence";
            public const string Action = "Index";
            public const string Role = "NumberSequence";
            public const string Url = "/NumberSequence/Index";
            public const string Name = "NumberSequence";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "NumberSequence")]
        public bool NumberSequenceRole { get; set; } = false;
    }
}