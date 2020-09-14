using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models.Invent;
using netcore.Services;

namespace netcore.Controllers.Invent
{
    [Authorize(Roles = "ProductionOrder")]
    public class ProductionOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public ProductionOrderController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: ProductionOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionOrder.ToListAsync());
        }

        // GET: ProductionOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionOrder = await _context.ProductionOrder
                .Include(x=>x.ProductionOrderLine)
                .SingleOrDefaultAsync(m => m.ProductionOrderId == id);
            if (productionOrder == null)
            {
                return NotFound();
            }

            return View(productionOrder);
        }

        // GET: ProductionOrder/Create
        public IActionResult Create()
        {
            ProductionOrder po = new ProductionOrder();
            Branch defaultBranch = _context.Branch.Where(x => x.isDefaultBranch.Equals(true)).FirstOrDefault();
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", defaultBranch != null ? defaultBranch.branchId : null);

            return View(po);
        }

        // POST: ProductionOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductionOrderId,Description,HasChild,Notes,ProductionOrderDate,ProductionOrderStatus,RequiredDeliveryDate,createdAt,ProductionOrderNumber,branchId")] ProductionOrder productionOrder)
        {
            if (ModelState.IsValid)
            {
                productionOrder.ProductionOrderNumber = _numberSequence.GetNumberSequence("ΠΠΡ");
                _context.Add(productionOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Δημιουργία της Παραγγελίας Παραγωγής (ΠΠΡ) " + productionOrder.ProductionOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Details), new { id = productionOrder.ProductionOrderId });
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", productionOrder.branchId);
            return View(productionOrder);
        }

        // GET: ProductionOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionOrder = await _context.ProductionOrder
                .Include(x=>x.ProductionOrderLine)
                .SingleOrDefaultAsync(m => m.ProductionOrderId == id);
            
            if (productionOrder == null)
            {
                return NotFound();
            }
            productionOrder.ProductionOrderDate.ToString("dd/mm/yy");
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", productionOrder.branchId);
            TempData["ProductionOrderStatus"] = productionOrder.ProductionOrderStatus;
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            return View(productionOrder);
        }

        // POST: ProductionOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductionOrderId,Description,HasChild,Notes,ProductionOrderDate,ProductionOrderStatus,RequiredDeliveryDate,createdAt,ProductionOrderNumber,branchId")] ProductionOrder productionOrder)
        {
            if (id != productionOrder.ProductionOrderId)
            {
                return NotFound();
            }

            if ((ProductionOrderStatus)TempData["ProductionOrderStatus"] == ProductionOrderStatus.Completed)
            {
                TempData["StatusMessage"] = "Σφάλμα. Δεν είναι δυνατή η επεξεργασία μιας [Ολοκληρωμένης] Παραγγελίας.";
                return RedirectToAction(nameof(Edit), new { id = productionOrder.ProductionOrderId });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionOrderExists(productionOrder.ProductionOrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["TransMessage"] = "Η Επεξεργασία της Παραγγελίας Παραγωγής (ΠΠΡ) " + productionOrder.Description + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", productionOrder.branchId);
            TempData["ProductionOrderStatus"] = productionOrder.ProductionOrderStatus;
            return View(productionOrder);
        }

        // GET: ProductionOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionOrder = await _context.ProductionOrder
                .Include(x=>x.ProductionOrderLine)
                .SingleOrDefaultAsync(m => m.ProductionOrderId == id);
            if (productionOrder == null)
            {
                return NotFound();
            }

            return View(productionOrder);
        }

        // POST: ProductionOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productionOrder = await _context.ProductionOrder
                .Include(x => x.ProductionOrderLine)
                .SingleOrDefaultAsync(m => m.ProductionOrderId == id);
            try
            {
                _context.ProductionOrderLine.RemoveRange(productionOrder.ProductionOrderLine);
                _context.ProductionOrder.Remove(productionOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Διαγραφή της παραγγελίας Παραγωγής (ΠΠΡ) " + productionOrder.ProductionOrderNumber + " έγινε με επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(productionOrder);
            }
        }

        private bool ProductionOrderExists(string id)
        {
            return _context.ProductionOrder.Any(e => e.ProductionOrderId == id);
        }
        public async Task<IActionResult> ShowProductionOrder(string id)
        {
            ProductionOrder obj = await _context.ProductionOrder
                .Include(x => x.branch)
                .Include(x => x.ProductionOrderLine).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.ProductionOrderId.Equals(id));
            return View(obj);
        }

        public async Task<IActionResult> PrintProductionOrder(string id)
        {
            ProductionOrder obj = await _context.ProductionOrder
                .Include(x => x.branch)
                .Include(x => x.ProductionOrderLine).ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.ProductionOrderId.Equals(id));
            return View(obj);
        }

    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class ProductionOrder
        {
            public const string Controller = "ProductionOrder";
            public const string Action = "Index";
            public const string Role = "ProductionOrder";
            public const string Url = "/ProductionOrder/Index";
            public const string Name = "ProductionOrder";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "ProductionOrder")]
        public bool ProductionOrderRole { get; set; } = false;
    }
}