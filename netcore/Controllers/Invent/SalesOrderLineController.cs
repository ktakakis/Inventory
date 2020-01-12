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
using Newtonsoft.Json;

namespace netcore.Controllers.Invent
{
    
    [Authorize(Roles = "SalesOrderLine")]
    public class SalesOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesOrderLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SalesOrderLine.Include(s => s.Product).Include(s => s.SalesOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SalesOrderLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLine
                    .Include(s => s.Product)
                    .Include(s => s.SalesOrder)
                        .SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return View(salesOrderLine);
        }


        // GET: SalesOrderLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.SalesOrderLine.SingleOrDefault(m => m.SalesOrderLineId == id);
            var selected = _context.SalesOrder.SingleOrDefault(m => m.salesOrderId == masterid);
            var catalogProducts = from Catalog in _context.Catalog
                                  join CatalogLine in _context.CatalogLine on Catalog.CatalogId equals CatalogLine.CatalogId
                                  join Product in _context.Product on CatalogLine.ProductId equals Product.productId
                                  select new
                                  {
                                      CatalogLine.ProductId,
                                      Product.productCode,
                                      CatalogLine.CatalogId,
                                      CatalogLine.Discount
                                  };
                ViewData["salesOrderId"] = new SelectList(_context.SalesOrder, "salesOrderId", "salesOrderNumber");
            if (check == null)
            {
                SalesOrderLine objline = new SalesOrderLine
                {
                    SalesOrder = selected,
                    SalesOrderId = masterid,
                    Qty = 1,
                };
                var custId = _context.SalesOrder.Where(s => s.salesOrderId == selected.salesOrderId).FirstOrDefault().customerId;
                var catalogId = _context.Catalog.Where(c => c.CustomerId == custId).FirstOrDefault().CatalogId;
                ViewData["productId"] = new SelectList(catalogProducts.Where(p => p.CatalogId == catalogId), "ProductId", "productCode");
                return View(objline);
            }
            else
            {
               //var catalogId = _context.Catalog.Where(c => c.CustomerId == check.SalesOrder.customerId).FirstOrDefault().CatalogId; 
               //ViewData["productId"] = new SelectList(catalogProducts.Where(p => p.CatalogId == catalogId), "ProductId", "productCode");
               ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode"); 
                return View(check);
            }
        }




        // POST: SalesOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderLineId,Discount,DiscountAmount,Price,TotalBeforeDiscount,ProductId,ProductVAT,ProductVATAmount,Qty,SalesOrderId,SpecialTaxAmount,SpecialTaxDiscount,TotalAmount,UnitCost,createdAt,TotalAfterDiscount,TotalSpecialTaxAmount,TotalWithSpecialTax")] SalesOrderLine salesOrderLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", salesOrderLine.ProductId);
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrder, "salesOrderId", "salesOrderNumber", salesOrderLine.SalesOrderId);
            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLine.SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", salesOrderLine.ProductId);
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrder, "salesOrderId", "salesOrderNumber", salesOrderLine.SalesOrderId);
            return View(salesOrderLine);
        }

        // POST: SalesOrderLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SalesOrderLineId,Discount,DiscountAmount,Price,TotalBeforeDiscount,ProductId,ProductVAT,ProductVATAmount,Qty,SalesOrderId,SpecialTaxAmount,SpecialTaxDiscount,TotalAmount,UnitCost,createdAt,TotalAfterDiscount,TotalSpecialTaxAmount,TotalWithSpecialTax")] SalesOrderLine salesOrderLine)
        {
            if (id != salesOrderLine.SalesOrderLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderLineExists(salesOrderLine.SalesOrderLineId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode", salesOrderLine.ProductId);
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrder, "salesOrderId", "salesOrderNumber", salesOrderLine.SalesOrderId);
            
            return View(salesOrderLine);
        }

        // GET: SalesOrderLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderLine = await _context.SalesOrderLine
                    .Include(s => s.Product)
                    .Include(s => s.SalesOrder)
                    .SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            if (salesOrderLine == null)
            {
                return NotFound();
            }

            return View(salesOrderLine);
        }


        // POST: SalesOrderLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var salesOrderLine = await _context.SalesOrderLine.SingleOrDefaultAsync(m => m.SalesOrderLineId == id);
            _context.SalesOrderLine.Remove(salesOrderLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool SalesOrderLineExists(string id)
        {
            return _context.SalesOrderLine.Any(e => e.SalesOrderLineId == id);
        }
        [HttpGet]
        public JsonResult GetOrderLineNumbers(string productId, string salesorderId, decimal qty)
        {
            var sa = new JsonSerializerSettings();
            var custId = _context.SalesOrder.Where(s => s.salesOrderId == salesorderId).FirstOrDefault().customerId;
            var catalogId = _context.Catalog.Where(c => c.CustomerId == custId).FirstOrDefault().CatalogId;
            var productPrice = _context.Product.Where(p => p.productId == productId).FirstOrDefault().UnitPrice;
            var discount = _context.CatalogLine.Where(d => d.CatalogId == catalogId && d.ProductId == productId).FirstOrDefault().Discount.GetValueOrDefault(0);
            decimal specialtaxDiscount = _context.Customer.Where(c => c.customerId == custId.ToString()).FirstOrDefault().TaxDiscount;
            var specialtaxamount = _context.Product.Where(p => p.productId == productId).FirstOrDefault().SpecialTaxValue;
            var productVAT = _context.Product.Where(s => s.productId == productId).FirstOrDefault().ProductVAT;
            var productCost = _context.Product.Where(p => p.productId == productId).FirstOrDefault().UnitCost;
            var invoicing = _context.SalesOrder.Where(s => s.salesOrderId == salesorderId).FirstOrDefault().Invoicing;
            var productvatamount = ((productPrice * (1 - discount)) + specialtaxamount * (1 - (specialtaxDiscount * Convert.ToInt32(!invoicing)))) * productVAT * qty;
            var totalwithspecialtax = ((qty * specialtaxamount * (1 - (specialtaxDiscount * Convert.ToInt32(!invoicing)))) + productPrice) * (1 + productVAT);

            var result = new {
                Qty = qty,
                Price = productPrice,
                BeforeDiscountAmount = productPrice * qty,
                Discount = discount,
                DiscountAmount = productPrice * discount * qty,
                PriceWithDiscount = productPrice * (1 - discount) * qty,
                SpecialTaxAmount = specialtaxamount,
                SpecialTaxDiscount = specialtaxDiscount,
                ProductVATAmount = productvatamount,
                TotalPriceWithTaxes  = (qty * specialtaxamount * (1 - (specialtaxDiscount * Convert.ToInt32(!invoicing)))) + productPrice ,
                ProductVAT = productVAT,
                ProductCost = productCost,
                TotalAfterDiscount = qty * productPrice * (1 - discount),
                TotalSpecialTaxAmount = qty * specialtaxamount* (1 - (specialtaxDiscount * Convert.ToInt32(!invoicing))),
                TotalAmount = ((qty * productPrice * (1 - discount)) + (qty * specialtaxamount * (1 - (specialtaxDiscount * Convert.ToInt32(!invoicing))))) * (1 + productVAT),
                TotalBeforeDiscount = qty * productPrice,
                TotalWithSpecialTax = totalwithspecialtax
            };
            return Json(result, sa);
        }

    }
}


namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class SalesOrderLine
        {
            public const string Controller = "SalesOrderLine";
            public const string Action = "Index";
            public const string Role = "SalesOrderLine";
            public const string Url = "/SalesOrderLine/Index";
            public const string Name = "SalesOrderLine";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Στοιχεία Παραγγελίας")]
        public bool SalesOrderLineRole { get; set; } = false;
    }
}



