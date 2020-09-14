using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models.Invent;

namespace netcore.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ProductionLine")]
    public class ProductionLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductionLine
        [HttpGet]
        [Authorize]
        public IActionResult GetProductionLine(string masterid)
        {
            return Json(new { data = _context.ProductionLine.Include(x => x.product).Where(x => x.ProductionId.Equals(masterid)).ToList() });
        }

        // POST: api/ProductionLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProductionLine([FromBody] ProductionLine productionLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(productionLine.ProductionLineId))
            {
                productionLine.ProductionLineId = Guid.NewGuid().ToString();
                _context.ProductionLine.Add(productionLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη στοιχείου Παραγγελίας, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(productionLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία στοιχείου Παραγγελίας, έγινε με επιτυχία." });
            }
        }

        // DELETE: api/ProductionLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductionLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productionLine = await _context.ProductionLine
                .Include(x => x.Production)
                .SingleOrDefaultAsync(m => m.ProductionLineId == id);
            if (productionLine == null)
            {
                return NotFound();
            }

            _context.ProductionLine.Remove(productionLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου παραγωγής έγινε με επιτυχία." });

        }

    }
}