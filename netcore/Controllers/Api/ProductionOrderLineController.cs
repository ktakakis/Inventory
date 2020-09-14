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
    [Route("api/ProductionOrderLine")]
    public class ProductionOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductionOrderLine
        [HttpGet]
        [Authorize]
        public IActionResult GetProductionOrderLine(string masterid)
        {
            return Json(new { data = _context.ProductionOrderLine.Include(x => x.Product).Where(x => x.ProductionOrderId.Equals(masterid)).ToList() });
        }

        // POST: api/ProductionOrderLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProductionOrderLine([FromBody] ProductionOrderLine productionOrderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(productionOrderLine.ProductionOrderLineId))
            {
                productionOrderLine.ProductionOrderLineId = Guid.NewGuid().ToString();
                _context.ProductionOrderLine.Add(productionOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη στοιχείου Παραγγελίας, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(productionOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία στοιχείου Παραγγελίας, έγινε με επιτυχία." });
            }
        }

        // DELETE: api/ProductionOrderLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductionOrderLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productionOrderLine = await _context.ProductionOrderLine
                .Include(x => x.ProductionOrder)
                .SingleOrDefaultAsync(m => m.ProductionOrderLineId == id);
            if (productionOrderLine == null)
            {
                return NotFound();
            }

            _context.ProductionOrderLine.Remove(productionOrderLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου παραγωγής έγινε με επιτυχία." });

        }

    }
}