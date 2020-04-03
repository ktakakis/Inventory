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
    [Route("api/ProductLine")]
    public class ProductLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductLine
        [HttpGet]
        [Authorize]
        public IActionResult GetProductLine(string masterid)
        {
            return Json(new { data = _context.ProductLine
                .Include(x => x.Product)
                .Include(x => x.Component)
                .Where(x => x.ProductId.Equals(masterid)).ToList() });
        }

        // POST: api/ProductLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProductLine([FromBody] ProductLine productLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(productLine.ProductLineId))
            {
                productLine.ProductLineId = Guid.NewGuid().ToString();
                _context.ProductLine.Add(productLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη του στοιχείου, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(productLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία στοιχείου, έγινε με επιτυχία." });
            }

        }

        // DELETE: api/ProductLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productLine = await _context.ProductLine
                .Include(x => x.Product)
                .Include(x => x.Component)
                .SingleOrDefaultAsync(m => m.ProductLineId == id);
            if (productLine == null)
            {
                return NotFound();
            }

            _context.ProductLine.Remove(productLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου, έγινε με επιτυχία." });
        }
    }
}
