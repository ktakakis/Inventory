using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Invent;
using netcore.Services;

namespace netcore.Controllers.Invent
{


    [Authorize(Roles = "SalesOrder")]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public SalesOrderController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }
        public async Task<IActionResult> ShowSalesOrder(string id)
        {
            SalesOrder obj = await _context.SalesOrder
                .Include(x => x.Employee)
                .Include(x => x.customer)
                .Include(x => x.salesOrderLine).ThenInclude(x => x.Product)
                .Include(x => x.branch)
                .Include(c=>c.customerLine)
                .SingleOrDefaultAsync(x => x.salesOrderId.Equals(id));

            obj.totalOrderAmount = obj.salesOrderLine.Sum(x => x.TotalAmount);
            obj.totalDiscountAmount = obj.salesOrderLine.Sum(x => x.DiscountAmount);
            obj.TotalProductVAT = obj.salesOrderLine.Sum(x => x.ProductVATAmount);
            obj.TotalWithSpecialTax = obj.salesOrderLine.Sum(x => x.TotalWithSpecialTax);
            obj.TotalBeforeDiscount = obj.salesOrderLine.Sum(x => x.TotalBeforeDiscount);
            _context.Update(obj);

            return View(obj);
        }

        public async Task<IActionResult> PrintSalesOrder(string id)
        {
            SalesOrder obj = await _context.SalesOrder
                .Include(x => x.Employee)
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

            var applicationDbContext = _context.SalesOrder.OrderBy(x => x.salesOrderStatus).ThenByDescending(x=>x.createdAt).Include(s => s.customer);
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                  applicationDbContext = _context.SalesOrder.Where(s => s.Employee.UserName == username).OrderByDescending(x => x.createdAt).Include(s => s.customer);
            }
           
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
                    .Include(x => x.Employee)
                    .Include(x => x.salesOrderLine)
                    .Include(s => s.customer)
                    .Include(x => x.branch)
                    .Include(c=>c.customerLine)
                    .SingleOrDefaultAsync(m => m.salesOrderId == id);
            salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
            salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
            salesOrder.TotalProductVAT = salesOrder.salesOrderLine.Sum(x => x.ProductVATAmount);
            salesOrder.TotalWithSpecialTax = salesOrder.salesOrderLine.Sum(x => x.TotalWithSpecialTax);
            salesOrder.TotalBeforeDiscount = salesOrder.salesOrderLine.Sum(x => x.TotalBeforeDiscount);
            salesOrder.Commission = salesOrder.salesOrderLine.Sum(x => x.TotalAfterDiscount) * salesOrder.Employee.Commission;
            _context.Update(salesOrder);
            await _context.SaveChangesAsync();

           if (salesOrder == null)
            {
                return NotFound();
            }

            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            ViewData["customerLineId"] = new SelectList(_context.CustomerLine, "customerLineId", "street1");
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["employeeId"] = new SelectList(_context.Employee.Where(e => e.UserName == username), "EmployeeId", "DisplayName");
            }
            else
            {
                ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            }
            return View(salesOrder);
        }


        // GET: SalesOrder/Create
        public IActionResult Create()
        {
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["customerId"] = new SelectList(_context.Customer.Where(c=>c.Employee.UserName==username && c.Active==true), "customerId", "customerName");
                ViewData["employeeId"] = new SelectList(_context.Employee.Where(e => e.UserName == username), "EmployeeId", "DisplayName");
          }
            else
            {
                ViewData["customerId"] = new SelectList(_context.Customer.Where(c=>c.Active == true), "customerId", "customerName");
                ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName"); 
            }
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
        public async Task<IActionResult> Create([Bind("salesOrderId,HasChild,branchId,createdAt,customerId,customerLineId,deliveryDate,description,salesOrderNumber,salesOrderStatus,salesShipmentNumber,soDate,top,totalDiscountAmount,totalOrderAmount,Invoicing,TotalProductVAT,TotalWithSpecialTax,EmployeeId,TotalBeforeDiscount,SalesOrderName,Commission")] SalesOrder salesOrder)
        {

            if (ModelState.IsValid)
            {
                string customerName = _context.Customer.Where(x => x.customerId == salesOrder.customerId).FirstOrDefault().customerName;
                
                salesOrder.salesOrderNumber = _numberSequence.GetNumberSequence("ΠΠ");
                salesOrder.SalesOrderName = salesOrder.salesOrderNumber + " ("+ customerName + ")";
                _context.Add(salesOrder);
                await _context.SaveChangesAsync();
                TempData["TransMessage"] = "Η Δημιουργία της Παραγγελίας Πώλησης (ΠΠ) " + salesOrder.salesOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Details), new { id = salesOrder.salesOrderId });
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["employeeId"] = new SelectList(_context.Employee.Where(e => e.UserName == username), "EmployeeId", "DisplayName");
            }
            else
            {
                ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName");
            }
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

            var salesOrder = await _context.SalesOrder
                .Include(x => x.salesOrderLine)
                .Include(x=>x.Employee)
                .SingleOrDefaultAsync(m => m.salesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
            salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
            salesOrder.TotalProductVAT = salesOrder.salesOrderLine.Sum(x => x.ProductVATAmount);
            salesOrder.TotalWithSpecialTax = salesOrder.salesOrderLine.Sum(x => x.TotalWithSpecialTax);
            salesOrder.TotalBeforeDiscount = salesOrder.salesOrderLine.Sum(x => x.TotalBeforeDiscount);
            salesOrder.Commission = salesOrder.salesOrderLine.Sum(x => x.TotalAfterDiscount) * salesOrder.Employee.Commission;

            _context.Update(salesOrder);
            await _context.SaveChangesAsync();
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["customerId"] = new SelectList(_context.Customer.Where(c => c.Employee.UserName == username && c.Active == true), "customerId", "customerName", salesOrder.customerId);
                ViewData["employeeId"] = new SelectList(_context.Employee.Where(e => e.UserName == username), "EmployeeId", "DisplayName",salesOrder.EmployeeId);
            }
            else
            {
                ViewData["customerId"] = new SelectList(_context.Customer.Where(c=>c.Active==true), "customerId", "customerName", salesOrder.customerId);
                ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName",salesOrder.EmployeeId);
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
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
        public async Task<IActionResult> Edit(string id, [Bind("salesOrderId,HasChild,branchId,createdAt,customerId,customerLineId,deliveryDate,description,salesOrderNumber,salesOrderStatus,salesShipmentNumber,soDate,top,totalDiscountAmount,totalOrderAmount,Invoicing,TotalProductVAT,TotalWithSpecialTax,EmployeeId,TotalBeforeDiscount,SalesOrderName,Commission")] SalesOrder salesOrder)
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
                    string customerName = _context.Customer.Where(x => x.customerId == salesOrder.customerId).FirstOrDefault().customerName;
                    salesOrder.SalesOrderName = salesOrder.salesOrderNumber + " (" + customerName + ")";
                    salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
                    salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
                    salesOrder.TotalProductVAT = salesOrder.salesOrderLine.Sum(x => x.ProductVATAmount);
                    salesOrder.TotalWithSpecialTax = salesOrder.salesOrderLine.Sum(x => x.TotalWithSpecialTax);
                    salesOrder.TotalBeforeDiscount = salesOrder.salesOrderLine.Sum(x => x.TotalBeforeDiscount);
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
                TempData["TransMessage"] = "Η Επεξεργασία της Παραγγελίας Πώλησης (ΠΠ) " + salesOrder.salesOrderNumber + " έγινε με Επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["employeeId"] = new SelectList(_context.Employee.Where(e => e.UserName == username), "EmployeeId", "DisplayName",salesOrder.EmployeeId);
            }
            else
            {
                ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName",salesOrder.EmployeeId);
            }

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
                    .Include(x => x.Employee)
                    .Include(x => x.salesOrderLine)
                    .Include(s => s.customer)
                    .SingleOrDefaultAsync(m => m.salesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            ViewData["branchId"] = new SelectList(_context.Branch, "branchId", "branchName", salesOrder.branchId);
            ViewData["customerId"] = new SelectList(_context.Customer, "customerId", "customerName", salesOrder.customerId);
            var username = HttpContext.User.Identity.Name;
            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                ViewData["employeeId"] = new SelectList(_context.Employee.Where(e => e.UserName == username), "EmployeeId", "DisplayName",salesOrder.EmployeeId);
            }
            else
            {
                ViewData["employeeId"] = new SelectList(_context.Employee, "EmployeeId", "DisplayName",salesOrder.EmployeeId);
            }

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
                TempData["TransMessage"] = "Η Διαγραφή της παραγγελίας πώλησης (ΠΠ) " + salesOrder.salesOrderNumber + " έγινε με επιτυχία";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewData["StatusMessage"] = "Σφάλμα. Ηρεμήστε ^ _ ^ και παρακαλούμε επικοινωνήστε διαχειριστή του συστήματός σας με αυτό το μήνυμα: " + ex;
                return View(salesOrder);
            }
            
        }
        public JsonResult UpdateSalesOrderTotals(string id)
        {
            var salesOrder = _context.SalesOrder.Where(x => x.salesOrderId == id)
            .Include(x => x.Employee)
            .Include(x => x.salesOrderLine)
            .Include(s => s.customer)
            .Include(x => x.branch)
            .Include(c => c.customerLine).SingleOrDefault();

            salesOrder.totalOrderAmount = salesOrder.salesOrderLine.Sum(x => x.TotalAmount);
            salesOrder.totalDiscountAmount = salesOrder.salesOrderLine.Sum(x => x.DiscountAmount);
            salesOrder.TotalProductVAT = salesOrder.salesOrderLine.Sum(x => x.ProductVATAmount);
            salesOrder.TotalWithSpecialTax = salesOrder.salesOrderLine.Sum(x => x.TotalWithSpecialTax);
            salesOrder.TotalBeforeDiscount = salesOrder.salesOrderLine.Sum(x => x.TotalBeforeDiscount);
            _context.Update(salesOrder);
            _context.SaveChangesAsync();
            return Json(salesOrder);

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



