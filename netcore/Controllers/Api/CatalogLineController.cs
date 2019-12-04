using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using netcore.Data;
using netcore.Models.Invent;
using System.Threading;
using System.Globalization;

namespace netcore.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/CatalogLine")]
    public class CatalogLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CatalogLine
        [HttpGet]
        public IActionResult GetCatalogLine(string masterid)
        {
            return Json(new { data = _context.CatalogLine.Include(x => x.Product).Where(x => x.CatalogId.Equals(masterid)).ToList() });
        }

        // POST: api/CatalogLine
        [HttpPost]
        public async Task<IActionResult> PostCatalogLine([FromBody] CatalogLine catalogLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(catalogLine.CatalogLineId))
            {
                catalogLine.CatalogLineId = Guid.NewGuid().ToString();
                _context.CatalogLine.Add(catalogLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη στοιχείου Έκπτωσης, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(catalogLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία στοιχείου Έκπτωσης, έγινε με επιτυχία." });
            }

        }

        // DELETE: api/CatalogLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalogLine = await _context.CatalogLine
                .Include(x => x.Catalog)
                .SingleOrDefaultAsync(m => m.CatalogLineId == id);
            if (catalogLine == null)
            {
                return NotFound();
            }

            _context.CatalogLine.Remove(catalogLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου Έκπτωσης έγινε με επιτυχία." });
        }

        private bool CatalogLineExists(string id)
        {
            return _context.CatalogLine.Any(e => e.CatalogLineId == id);
        }

    }

}
