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
using System.Globalization;
using System.Threading;

namespace netcore.Controllers.Invent
{


    [Authorize(Roles = "SalesOrder")]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowSalesOrder(string id)
        {
            SalesOrder obj = await _context.SalesOrder
                .Include(x => x.customer)
                .Include(x => x.salesOrderLine).ThenInclude(x => x.Product)
                .Include(x => x.branch)
                .Include(c=>c.customerLine)
                .SingleOrDefaultAsync(x => x.salesOrderId.Equals(id));

            obj.totalOrderAmount = obj.salesOrderLine.Sum(x => x.TotalAmount);
            obj.totalDiscountAmount = obj.salesOrderLine.Sum(x => x.DiscountAmount);
            
            _context.Update(obj);

            return View(obj);
        }

        public async Task<IActionResult> PrintSalesOrder(string id)
        {
            SalesOrder obj = await _context.SalesOrder
                .Include(x => x.customer)
                .Include(x => x.salesOrderLine).ThenInclude(x => x.Product)
                .Include(x => x.branch)
                .Include(c=>c.customerLine)
                .SingleOrDefaultAsync(x => x.salesOrderId.Equals(id));
            return View(obj);
        }

        // GET: SalesOrder
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.SalesOrder.OrderByDescending(x => x.createdAt).Include(s => s.customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalesOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrder
                    .Include(x => x.salesOrderLine)
                    .Include(s => s.customer)
                    .Include(x => x.branch)
                    .Include(c=>c.customerLine)
                        .SingleOrDefaultAsync(m => m.salesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            ViewData["customerLineId"] = new SelectList(_context.CustomerLine, "customerLineId", "street1");

            salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
            salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
            _context.Update(salesOrder);
            await _context.SaveChangesAsync();

            return View(salesOrder);
        }


        // GET: SalesOrder/Create
        public IActionResult Create()
        {
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName");
            Branch defaultBranch = _context.Branch.Where(x => x.isDefaultBranch.Equals(true)).FirstOrDefault();
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", defaultBranch != null ? defaultBranch.branchId : null);
            ViewData["customerLineId"]= new SelectList(_context.CustomerLine, "customerLineId", "street1");
            SalesOrder so = new SalesOrder();
            
            return View(so);
        }


        // POST: SalesOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("salesOrderId,HasChild,branchId,createdAt,customerId,deliveryDate,description,picCustomer,picInternal,referenceNumberExternal,referenceNumberInternal,salesOrderNumber,salesOrderStatus,salesShipmentNumber,soDate,top,totalDiscountAmount,totalOrderAmount,customerLineId")] SalesOrder salesOrder)
        {

            if (ModelState.IsValid)
            {
                _context.Add(salesOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Δημιουργία της Παραγγελίας Πώλησης (SO) " + salesOrder.salesOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Details), new { id = salesOrder.salesOrderId });
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            return View(salesOrder);
        }
        [HttpGet]
        public JsonResult GetCustomerLines(string customerId)
        {
            var customerLinelist = new SelectList(_context.CustomerLine.Where(c => c.customer.customerId == customerId), "customerLineId", "street1");
            return Json(customerLinelist);

        }
        // GET: SalesOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrder.Include(x => x.salesOrderLine).SingleOrDefaultAsync(m => m.salesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
            salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
            _context.Update(salesOrder);
            await _context.SaveChangesAsync();
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            TempData["SalesOrderStatus"] = salesOrder.salesOrderStatus;
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            ViewData["customerLineId"] = new SelectList(_context.CustomerLine.Where(c => c.customer.customerId == salesOrder.customerId), "customerLineId", "street1");
            salesOrder.soDate.ToString("dd/mm/yy");
            return View(salesOrder);
        }

        // POST: SalesOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("salesOrderId,HasChild,branchId,createdAt,customerId,deliveryDate,description,picCustomer,picInternal,referenceNumberExternal,referenceNumberInternal,salesOrderNumber,salesOrderStatus,salesShipmentNumber,soDate,top,totalDiscountAmount,totalOrderAmount,customerLineId")] SalesOrder salesOrder)
        {
            if (id != salesOrder.salesOrderId)
            {
                return NotFound();
            }

            if ((SalesOrderStatus)TempData["SalesOrderStatus"] == SalesOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία μιας [Ολοκληρωμένης] Παραγγελίας.";
                return RedirectToAction(nameof(Edit), new { id = salesOrder.salesOrderId});
            }

            if (salesOrder.salesOrderStatus== SalesOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία της κατάστασης μιας [Ολοκληρωμένης] Παραγγελίας.";
                return RedirectToAction(nameof(Edit), new { id = salesOrder.salesOrderId});
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderExists(salesOrder.salesOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["TransMessage"] = "Η Επεξεργασία της Παραγγελίας Πώλησης (PO) " + salesOrder.salesOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            
            return View(salesOrder);
        }

        // GET: SalesOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrder
                    .Include(x => x.salesOrderLine)
                    .Include(s => s.customer)
                    .SingleOrDefaultAsync(m => m.salesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);

            salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
            salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
            _context.Update(salesOrder);
            await _context.SaveChangesAsync();

            return View(salesOrder);
        }




        // POST: SalesOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var salesOrder = await _context.SalesOrder
                .Include(x => x.salesOrderLine)
                .SingleOrDefaultAsync(m => m.salesOrderId == id);
            try
            {
                _context.SalesOrderLine.RemoveRange(salesOrder.salesOrderLine);
                _context.SalesOrder.Remove(salesOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Delete SO " + salesOrder.salesOrderNumber + " Success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(salesOrder);
            }
            
        }

        private bool SalesOrderExists(string id)
        {
            return _context.SalesOrder.Any(e => e.salesOrderId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class SalesOrder
        {
            public const string Controller = "SalesOrder";
            public const string Action = "Index";
            public const string Role = "SalesOrder";
            public const string Url = "/SalesOrder/Index";
            public const string Name = "SalesOrder";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "SalesOrder")]
        public bool SalesOrderRole { get; set; } = false;
    }
}



