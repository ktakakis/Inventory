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
            ViewData["productId"] = new SelectList(_context.Product, "productId", "productCode");
            ViewData["salesOrderId"] = new SelectList(_context.SalesOrder, "salesOrderId", "salesOrderNumber");
            if (check == null)
            {
                SalesOrderLine objline = new SalesOrderLine
                {
                    SalesOrder = selected,
                    SalesOrderId = masterid,
                    Qty = 1,
                };
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: SalesOrderLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderLineId,createdAt,DiscountAmount,Price,ProductId,Qty,SalesOrderId,TotalAmount,ProductVAT,Discount,ProductVATAmount,SpecialTaxAmount,UnitCost,SpecialTaxDiscount")] SalesOrderLine salesOrderLine)
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
        public async Task<IActionResult> Edit(string id, [Bind("SalesOrderLineId,createdAt,DiscountAmount,Price,ProductId,Qty,SalesOrderId,TotalAmount,ProductVAT,Discount,ProductVATAmount,SpecialTaxAmount,UnitCost,SpecialTaxDiscount")] SalesOrderLine salesOrderLine)
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
        public JsonResult GetOrderLineNumbers(string productId, string salesorderId,decimal qty)
        {
            var sa = new JsonSerializerSettings(); 
            var custId = _context.SalesOrder.Where(s => s.salesOrderId == salesorderId).FirstOrDefault().customerId;
            var productPrice = _context.Product.Where(p => p.productId == productId).FirstOrDefault().UnitPrice;
            var specialtaxDiscount = _context.Customer.Where(c => c.customerId == custId.ToString()).FirstOrDefault().TaxDiscount;
            var specialtaxamount = _context.Product.Where(p => p.productId == productId).FirstOrDefault().SpecialTaxValue;
            var productVAT = _context.Product.Where(s => s.productId == productId).FirstOrDefault().ProductVAT;
            var productCost = _context.Product.Where(p => p.productId == productId).FirstOrDefault().UnitCost;
            var discount = _context.CatalogLine.Where(d => d.Catalog.CustomerId == custId ).FirstOrDefault().Discount.GetValueOrDefault(0);
            var productvatamount = (productPrice + specialtaxamount *(1- specialtaxDiscount)) * productVAT * qty;
            var result = new { 
                Price = productPrice,
                Discount = discount,
                DiscountAmount = productPrice*discount,
                ProductVATAmount=productvatamount,
                SpecialTaxDiscount = specialtaxDiscount,
                SpecialTaxAmount=specialtaxamount,
                ProductVAT=productVAT,
                ProductCost=productCost,
                Qty=qty
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



