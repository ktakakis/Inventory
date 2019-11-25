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
    [Route("api/TransferOrderLine")]
    public class TransferOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransferOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TransferOrderLine
        [HttpGet]
        [Authorize]
        public IActionResult GetTransferOrderLine(string masterid)
        {
            return Json(new { data = _context.TransferOrderLine.Include(x => x.product).Where(x => x.transferOrderId.Equals(masterid)).ToList() });
        }

        // POST: api/TransferOrderLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTransferOrderLine([FromBody] TransferOrderLine transferOrderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TransferOrder transferOrder = await _context.TransferOrder.Where(x => x.transferOrderId.Equals(transferOrderLine.transferOrderId)).FirstOrDefaultAsync();

            if (transferOrder.transferOrderStatus == TransferOrderStatus.Completed)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν μπορεί να γίνει Επεξεργασία, Παραγγελίας που είναι [Ολοκληρωμένη]." });
            }

            if (transferOrder.isIssued == true)
            {
                return Json(new { success = false, message = "Σφάλμα. Can not edit [Open] order that already process the goods issue" });
            }

            if (transferOrder.isReceived == true)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία παραγγελίας που είναι [Ανοιχτή], και ήδη επεξεργάζεται την παραλαβή των προϊόντων" });
            }

            if (transferOrderLine.transferOrderLineId == string.Empty)
            {
                transferOrderLine.transferOrderLineId = Guid.NewGuid().ToString();
                _context.TransferOrderLine.Add(transferOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Προσθήκη νέου στοιχείου μεταφοράς, έγινε με επιτυχία." });
            }
            else
            {
                _context.Update(transferOrderLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Η Επεξεργασία του στοιχείου μεταφοράς, έγινε με επιτυχία." });
            }

        }

        // DELETE: api/TransferOrderLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeleteTransferOrderLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferOrderLine = await _context.TransferOrderLine
                .Include(x => x.transferOrder)
                .SingleOrDefaultAsync(m => m.transferOrderLineId == id);
            if (transferOrderLine == null)
            {
                return NotFound();
            }

            if (transferOrderLine.transferOrder.transferOrderStatus == TransferOrderStatus.Completed
                || transferOrderLine.transferOrder.isIssued == true
                || transferOrderLine.transferOrder.isReceived == true)
            {
                return Json(new { success = false, message = "Σφάλμα. Δεν μπορεί να γίνει Διαγραφή Παραγγελίας που είναι [Ολοκληρωμένη] ή [Ανοιχτή], που ήδη επεξεργάζεται την Αποστολή ή την παραλαβή προϊόντων" });
            }

            _context.TransferOrderLine.Remove(transferOrderLine);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Η Διαγραφή του στοιχείου μεταφοράς, έγινε με επιτυχία." });
        }


        private bool TransferOrderLineExists(string id)
        {
            return _context.TransferOrderLine.Any(e => e.transferOrderLineId == id);
        }


    }

}
