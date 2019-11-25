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

namespace netcore.Controllers.Api
{

    [Produces("application/json")]
    [Route("api/PurchaseOrderLine")]
    public class PurchaseOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrderLine
        [HttpGet]
        [Authorize]
        public IActionResult GetPurchaseOrderLine(string masterid)
        {
            return Json(new { data = _context.PurchaseOrderLine.Include(x => x.product).Where(x => x.purchaseOrderId.Equals(masterid)).ToList() });
        }

        // POST: api/PurchaseOrderLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostPurchaseOrderLine([FromBody] PurchaseOrderLine purchaseOrderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PurchaseOrder purchaseOrder = await _context.PurchaseOrder.Where(x => x.purchaseOrderId.Equals(purchaseOrderLine.purchaseOrderId)).FirstOrDefaultAsync();

            if (purchaseOrder.purchaseOrderStatus == PurchaseOrderStatus.Completed)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία παραγγελίας που είναι [Ολοκληρωμένη]" });
            }

            purchaseOrderLine.totalAmount = (decimal)purchaseOrderLine.qty * purchaseOrderLine.price;

            if (purchaseOrderLine.purchaseOrderLineId == string.Empty)
            {
                purchaseOrderLine.purchaseOrderLineId = Guid.NewGuid().ToString();
                _context.PurchaseOrderLine.Add(purchaseOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη στοιχείου στην Παραγγελία Αγοράς, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(purchaseOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία του στοιχείου στην Παραγγελία Αγοράς, έγινε με επιτυχία." });
            }

        }

        // DELETE: api/PurchaseOrderLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeletePurchaseOrderLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrderLine = await _context.PurchaseOrderLine
                .Include(x => x.purchaseOrder)
                .SingleOrDefaultAsync(m => m.purchaseOrderLineId == id);

            if (purchaseOrderLine == null)
            {
                return NotFound();
            }

            if (purchaseOrderLine.purchaseOrder.purchaseOrderStatus == PurchaseOrderStatus.Completed)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν μπορεί να γίνει Διαγραφή Παραγγελίας που είναι [Ολοκληρωμένη]." });
            }

            _context.PurchaseOrderLine.Remove(purchaseOrderLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου της Παραγγελίας Αγοράς, έγινε με επιτυχία." });
        }


        private bool PurchaseOrderLineExists(string id)
        {
            return _context.PurchaseOrderLine.Any(e => e.purchaseOrderLineId == id);
        }


    }

}
