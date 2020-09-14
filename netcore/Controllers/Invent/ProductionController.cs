using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore.Data;
using netcore.Models.Invent;
using netcore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Controllers.Invent
{
    [Authorize(Roles = "Production")]

    public class ProductionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetcoreService _netcoreService;
        private readonly INumberSequence _numberSequence;

        public ProductionController(ApplicationDbContext context, INetcoreService netcoreService,
                        INumberSequence numberSequence)
        {
            _context = context;
            _netcoreService = netcoreService;
            _numberSequence = numberSequence;
        }

        // GET: Production
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Production.Include(p => p.ProductionOrder).Include(p => p.warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Production/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .Include(p => p.ProductionOrder)
                .Include(p => p.warehouse)
                .SingleOrDefaultAsync(m => m.ProductionId == id);
            if (production == null)
            {
                return NotFound();
            }
            ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", production.ProductionOrderId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName", production.warehouseId);
            return View(production);
        }

        // GET: Production/Create
        public IActionResult Create(string id)
        {
            Production pr = new Production();
           
            ViewData["StatusMessage"] = TempData["StatusMessage"];
            List<ProductionOrder> po = _context.ProductionOrder.ToList();
            po.Insert(0, new ProductionOrder { ProductionOrderId = "0", ProductionOrderNumber = "Επιλέξτε",ProductionOrderStatus=ProductionOrderStatus.Open });
            ViewData["ProductionOrderId"] = new SelectList(po.Where(x => x.ProductionOrderStatus == ProductionOrderStatus.Open), "ProductionOrderId", "ProductionOrderNumber");
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName");
            if (id != null)
            {
                ProductionOrder pro = _context.ProductionOrder
                .Where(x => x.ProductionOrderId.Equals(id)).FirstOrDefault();

                pr.ProductionOrderId = id;
                List<Warehouse> warehouseList = _context.Warehouse.Where(x => x.branchId.Equals(pro.branchId)).ToList();
                warehouseList.Insert(0, new Warehouse { warehouseId = "0", warehouseName = "Επιλέξτε" });
                ViewData["warehouseId"] = new SelectList(warehouseList, "warehouseId", "warehouseName");
            }
            else
            {
                List<Warehouse> warehouseList = _context.Warehouse.ToList();
                warehouseList.Insert(0, new Warehouse { warehouseId = "0", warehouseName = "Επιλέξτε" });
                ViewData["warehouseId"] = new SelectList(warehouseList, "warehouseId", "warehouseName");
            }
            return View(pr);
        }

        // POST: Production/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductionId,HasChild,ProductionDate,ProductionNumber,ProductionOrderId,createdAt,warehouseId,ProductionStatus,Description,Notes")] Production production)
        {
            if (production.ProductionOrderId == "0" || production.warehouseId == "0")
            {
                TempData["StatusMessage"] = "Σφάλμα. Η εντολή παραγωγής ή η αποθήκη δεν είναι έγκυρη. Επιλέξτε έγκυρη παραγγελία και αποθήκη παραγωγής";
                return RedirectToAction(nameof(Create));
            }
            if (ModelState.IsValid)
            {
                var polines =
                    from ProductionOrderLine in _context.ProductionOrderLine
                    join Product in _context.Product on ProductionOrderLine.ProductId equals Product.productId
                    join ProductLine in _context.ProductLine on Product.productId equals ProductLine.ProductId
                    join Product_1 in _context.Product on new { ComponentId = ProductLine.ComponentId } equals new { ComponentId = Product_1.productId }
                    group new { ProductLine, ProductionOrderLine, Product_1 } by new
                    {
                        ProductLine.ComponentId,
                        ProductionOrderLine.ProductionOrderId,
                        Product_1.productCode
                    } into g
                    select new
                    {
                        g.Key.ProductionOrderId,
                        g.Key.ComponentId,
                        g.Key.productCode,
                        ComponentQty = (decimal?)g.Sum(p => p.ProductionOrderLine.Qty * System.Convert.ToSingle(p.ProductLine.Percentage))
                    };

                polines = polines.Where(x => x.ProductionOrderId == production.ProductionOrderId); 

                //check production order
                Production check = await _context.Production
                     .Include(x => x.ProductionOrder)
                     .Include(x => x.warehouse)
                     .SingleOrDefaultAsync(x => x.ProductionOrderId.Equals(production.ProductionOrderId));
                if (check != null)
                {
                    ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder.Where(x => x.ProductionOrderStatus == ProductionOrderStatus.Open), "ProductionOrderId", "ProductionNumber");
                    ViewData["StatusMessage"] = "Σφάλμα. Η εντολή παραγωγής έχει ήδη αποσταλεί. " + check.ProductionNumber;
                    ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName");
                    return View(production);
                }
                //check stock
                bool isStockOK = true;
                string productList = "";
                foreach (var item in polines)
                {
                    VMStock stock = _netcoreService.GetStockByProductAndWarehouse(item.ComponentId, production.warehouseId);
                    if (stock != null)
                    {
                        if (stock.QtyOnhand < System.Convert.ToSingle(item.ComponentQty))
                        {
                            isStockOK = false;
                            productList = productList + " [" + item.productCode + "] ";
                        }
                    }
                    else
                    {
                        isStockOK = false;
                    }
                }

                if (!isStockOK)
                {
                    TempData["StatusMessage"] = "Σφάλμα. Υπάρχει πρόβλημα στην ποσότητα αποθεμάτων, ελέγξτε το απόθεμά σας. " + productList;
                    return RedirectToAction(nameof(Create));
                }

                //change status of salesorder
                var pro = _context.ProductionOrder.Where(x => x.ProductionOrderId == production.ProductionOrderId).FirstOrDefault();
                pro.ProductionOrderStatus = ProductionOrderStatus.Completed;
                _context.Update(pro);
                production.ProductionNumber = _numberSequence.GetNumberSequence("ΠΑΡ");
                _context.Add(production);
                await _context.SaveChangesAsync();

                //*auto create production line, full production
                foreach (var item in polines)
                {
                    Product prod = _context.Product.Where(x => x.productId == item.ComponentId).FirstOrDefault();
                    ProductionLine line = new ProductionLine();
                    line.Production = production;
                    line.product = prod;
                    line.warehouse = production.warehouse;
                    line.branch = production.ProductionOrder.branch;
                    line.qty = System.Convert.ToInt32(item.ComponentQty); 
                    line.branchId = production.ProductionOrder.branchId;
                    line.warehouseId = production.warehouseId;

                    _context.ProductionLine.Add(line);
                    await _context.SaveChangesAsync();
                }
             TempData["TransMessage"] = "Η Δημιουργία της Παραγωγής " + production.ProductionNumber + " έγινε με Επιτυχία";
             ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", production.ProductionOrderId);
             return RedirectToAction(nameof(Details), new { id = production.ProductionId });
            }
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName", production.warehouseId);
            return View(production);
        }

        // GET: Production/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .Include(x=>x.ProductionOrder)
                .SingleOrDefaultAsync(m => m.ProductionId == id);
            if (production == null)
            {
                return NotFound();
            }
            ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", production.ProductionOrderId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName", production.warehouseId);
            return View(production);
        }

        // POST: Production/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductionId,HasChild,ProductionDate,ProductionNumber,ProductionOrderId,createdAt,warehouseId,ProductionStatus,Description,Notes")] Production production)
        {
            if (id != production.ProductionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.ProductionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["TransMessage"] = "Η επεξεργασία της παραγωγής " + production.ProductionNumber + " έγινε με επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductionOrderId"] = new SelectList(_context.ProductionOrder, "ProductionOrderId", "ProductionOrderNumber", production.ProductionOrderId);
            ViewData["warehouseId"] = new SelectList(_context.Warehouse, "warehouseId", "warehouseName", production.warehouseId);
            return View(production);
        }

        // GET: Production/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Production
                .Include(p => p.ProductionOrder)
                .Include(p => p.warehouse)
                .SingleOrDefaultAsync(m => m.ProductionId == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Production/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var production = await _context.Production
                .Include(x=>x.ProductionOrder)
                .Include(x => x.ProductionLine)
                .SingleOrDefaultAsync(m => m.ProductionId == id);
            try
            {
                //rollback status to open
                production.ProductionOrder.ProductionOrderStatus = ProductionOrderStatus.Open;

                _context.ProductionLine.RemoveRange(production.ProductionLine);
                _context.Production.Remove(production);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Διαγραφή της Παραγωγής (ΠΠΡ) " + production.ProductionNumber + " έγινε με επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(production);
            }
        }

        private bool ProductionExists(string id)
        {
            return _context.Production.Any(e => e.ProductionId == id);
        }

        public IActionResult GetWarehouseByOrder(string productionOrderId)
        {
            ProductionOrder po = _context.ProductionOrder
                .Include(x => x.branch)
                .Where(x => x.ProductionOrderId.Equals(productionOrderId)).FirstOrDefault();

            List<Warehouse> warehouseList = _context.Warehouse.Where(x => x.branchId.Equals(po.branchId)).ToList();
            warehouseList.Insert(0, new Warehouse { warehouseId = "0", warehouseName = "Επιλέξτε" });

            return Json(new SelectList(warehouseList, "warehouseId", "warehouseName"));
        }

        public async Task<IActionResult> ShowProduction(string id)
        {
            Production obj = await _context.Production
                .Include(x => x.branch)
                .Include(x => x.ProductionOrder)
                .Include(x => x.warehouse)
                .Include(x => x.branch)
                .Include(x => x.ProductionLine).ThenInclude(x => x.product)
                .SingleOrDefaultAsync(x => x.ProductionId.Equals(id));
            return View(obj);
        }

        public async Task<IActionResult> PrintProduction(string id)
        {
            Production obj = await _context.Production
                .Include(x => x.branch)
                .Include(x => x.ProductionOrder)
                .Include(x => x.warehouse)
                .Include(x => x.branch)
                .Include(x => x.ProductionLine).ThenInclude(x => x.product)
                .SingleOrDefaultAsync(x => x.ProductionId.Equals(id));
            return View(obj);
        }

    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Production
        {
            public const string Controller = "Production";
            public const string Action = "Index";
            public const string Role = "Production";
            public const string Url = "/Production/Index";
            public const string Name = "Production";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Production")]
        public bool ProductionRole { get; set; } = false;
    }
}