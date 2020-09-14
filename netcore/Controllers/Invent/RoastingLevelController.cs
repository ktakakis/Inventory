using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Invent;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace netcore.Controllers.Invent
{
    [Authorize(Roles = "RoastingLevel")]

    public class RoastingLevelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoastingLevelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoastingLevel
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoastingLevel.ToListAsync());
        }

        // GET: RoastingLevel/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roastingLevel = await _context.RoastingLevel
                .SingleOrDefaultAsync(m => m.RoastingLevelId == id);
            if (roastingLevel == null)
            {
                return NotFound();
            }

            return View(roastingLevel);
        }
        public ActionResult ByteArrayToFile(string id)
        {
            var rostlevel = _context.RoastingLevel.Where(x => x.RoastingLevelId == id).FirstOrDefault();
            string fileName = rostlevel.RoastingLevelName + ".jpg";

            if (rostlevel == null)
            {
                return NotFound();
            }

            Response.Headers.Append("content-disposition", "inline; filename=" + fileName);
            return new FileStreamResult(new MemoryStream(rostlevel.File.ToArray()), "image/jpeg");
        }

        // GET: RoastingLevel/Create
        public IActionResult Create()
        {
            RoastingLevel roastLevel = new RoastingLevel();
            return View(roastLevel);
        }

        // POST: RoastingLevel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoastingLevelId,RoastingLevelName,Description,File,createdAt")] RoastingLevel roastingLevel, IFormFile File)
        {
            if (File != null)
            {
                using (var stream = new MemoryStream())
                {
                    await File.CopyToAsync(stream);
                    roastingLevel.File = stream.ToArray();
                }
            }


            if (ModelState.IsValid)
            {
                _context.Add(roastingLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roastingLevel);
        }

        // GET: RoastingLevel/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roastingLevel = await _context.RoastingLevel.SingleOrDefaultAsync(m => m.RoastingLevelId == id);
            if (roastingLevel == null)
            {
                return NotFound();
            }
            return View(roastingLevel);
        }

        // POST: RoastingLevel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RoastingLevelId,RoastingLevelName,Description,File,createdAt")] RoastingLevel roastingLevel, IFormFile File)
        {
            if (id != roastingLevel.RoastingLevelId)
            {
                return NotFound();
            }

            if (File != null)
            {
                using (var stream = new MemoryStream())
                {
                    await File.CopyToAsync(stream);
                    roastingLevel.File = stream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roastingLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoastingLevelExists(roastingLevel.RoastingLevelId))
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
            return View(roastingLevel);
        }

        // GET: RoastingLevel/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roastingLevel = await _context.RoastingLevel
                .SingleOrDefaultAsync(m => m.RoastingLevelId == id);
            if (roastingLevel == null)
            {
                return NotFound();
            }

            return View(roastingLevel);
        }

        // POST: RoastingLevel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roastingLevel = await _context.RoastingLevel.SingleOrDefaultAsync(m => m.RoastingLevelId == id);
            _context.RoastingLevel.Remove(roastingLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        private bool RoastingLevelExists(string id)
        {
            return _context.RoastingLevel.Any(e => e.RoastingLevelId == id);
        }
    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class RoastingLevel
        {
            public const string Controller = "RoastingLevel";
            public const string Action = "Index";
            public const string Role = "RoastingLevel";
            public const string Url = "/RoastingLevel/Index";
            public const string Name = "RoastingLevel";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "RoastingLevel")]
        public bool RoastingLevelRole { get; set; } = false;
    }
}

