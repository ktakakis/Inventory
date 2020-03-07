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
    [Route("api/VendorCatalogLine")]
    public class VendorCatalogLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorCatalogLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VendorCatalogLine
        [HttpGet]
        [Authorize]
        public IActionResult GetVendorCatalogLine(string masterid)
        {
            return Json(new { data = _context.VendorCatalogLine.Include(x => x.Product).Where(x => x.VendorCatalogId.Equals(masterid)).ToList() });
        }

        // POST: api/VendorCatalogLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostVendorCatalogLine([FromBody] VendorCatalogLine vendorCatalogLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(vendorCatalogLine.VendorCatalogLineId))
            {
                vendorCatalogLine.VendorCatalogLineId = Guid.NewGuid().ToString();
                _context.VendorCatalogLine.Add(vendorCatalogLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη στοιχείου Έκπτωσης, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(vendorCatalogLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία στοιχείου Έκπτωσης, έγινε με επιτυχία." });
            }

        }

        // DELETE: api/VendorCatalogLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVendorCatalogLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCatalogLine = await _context.VendorCatalogLine
                .Include(x => x.VendorCatalog)
                .SingleOrDefaultAsync(m => m.VendorCatalogLineId == id);
            if (vendorCatalogLine == null)
            {
                return NotFound();
            }

            _context.VendorCatalogLine.Remove(vendorCatalogLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου Έκπτωσης έγινε με επιτυχία." });
        }
    }
}
