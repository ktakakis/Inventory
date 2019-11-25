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
    [Route("api/SalesOrderLine")]
    public class SalesOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesOrderLine
        [HttpGet]
        [Authorize]
        public IActionResult GetSalesOrderLine(string masterid)
        {
            return Json(new { data = _context.SalesOrderLine.Include(x => x.Product).Where(x => x.SalesOrderId.Equals(masterid)).ToList() });
        }

        // POST: api/SalesOrderLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostSalesOrderLine([FromBody] SalesOrderLine salesOrderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SalesOrder salesOrder = await _context.SalesOrder.Where(x => x.salesOrderId.Equals(salesOrderLine.SalesOrderId)).FirstOrDefaultAsync();
            Customer customer = await _context.Customer.Where(c => c.customerId.Equals(salesOrder.customerId)).FirstOrDefaultAsync();
            Product product = await _context.Product.Where(p => p.productId.Equals(salesOrderLine.ProductId)).FirstOrDefaultAsync();
            
            if (salesOrder.salesOrderStatus == SalesOrderStatus.Completed)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν μπορεί να γίνει Επεξεργασία Παραγγελίας που είναι [Ολοκληρωμένη]." });
            }
            
            salesOrderLine.TotalAmount = (decimal)salesOrderLine.Qty * salesOrderLine.Price;

            if (string.IsNullOrEmpty(salesOrderLine.SalesOrderLineId))
            {
                salesOrderLine.SalesOrderLineId = Guid.NewGuid().ToString();
                _context.SalesOrderLine.Add(salesOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη στοιχείου Παραγγελίας, έγινε με επιτυχία." });
            }
            else
            {                
                _context.Update(salesOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία στοιχείου Παραγγελίας, έγινε με επιτυχία." });
            }

        }

        // DELETE: api/SalesOrderLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeleteSalesOrderLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrderLine = await _context.SalesOrderLine
                .Include(x => x.SalesOrder)
                .SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            if (salesOrderLine.SalesOrder.salesOrderStatus == SalesOrderStatus.Completed)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν μπορεί να γίνει Διαγραφή Παραγγελίας που είναι [Ολοκληρωμένη]." });
            }

            _context.SalesOrderLine.Remove(salesOrderLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου παραγγελίας έγινε με επιτυχία." });
        }

        private bool SalesOrderLineExists(string id)
        {
            return _context.SalesOrderLine.Any(e => e.SalesOrderLineId == id);
        }

    }

}
