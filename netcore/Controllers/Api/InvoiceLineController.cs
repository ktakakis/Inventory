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
    [Route("api/InvoiceLine")]
    public class InvoiceLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceLine
        [HttpGet]
        [Authorize]
        public IActionResult GetShipmentLine(string masterid)
        {
            return Json(new { data = _context.InvoiceLine.Include(x => x.Product).Where(x => x.InvoiceId.Equals(masterid)).ToList() });
        }

        // POST: api/InvoiceLine
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostInvoiceLine([FromBody] InvoiceLine invoiceLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvoiceLine.Add(invoiceLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceLine", new { id = invoiceLine.InvoiceLineId }, invoiceLine);
        }

        // DELETE: api/InvoiceLine/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInvoiceLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceLine = await _context.InvoiceLine.SingleOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            _context.InvoiceLine.Remove(invoiceLine);
            await _context.SaveChangesAsync();

            return Ok(invoiceLine);
        }

        private bool InvoiceLineExists(string id)
        {
            return _context.InvoiceLine.Any(e => e.InvoiceLineId == id);
        }
    }
}