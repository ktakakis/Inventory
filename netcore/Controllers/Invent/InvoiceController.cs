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
    [Authorize(Roles = "Invoice")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public InvoiceController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoice.Include(i => i.Shipment);
            var username = HttpContext.User.Identity.Name;

            if (!(HttpContext.User.IsInRole("ApplicationUser") || HttpContext.User.IsInRole("Secretary")))
            {
                applicationDbContext = _context.Invoice.Where(x=>x.Shipment.Employee.UserName==username && x.Paid!=true).Include(i => i.Shipment);
            }
                return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Shipment)
                .Include(p=>p.PaymentReceive)
                .SingleOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            var shipment =(
                        from Shipment in _context.Shipment
                        join Customer in _context.Customer on Shipment.customerId equals Customer.customerId
                        select new
                        {
                            Shipment.shipmentId,
                            ShipmentName = (Shipment.shipmentNumber + " ( " + Customer.customerName + ")")
                        }).ToList();
            shipment.Insert(0,
             new
             {
                 shipmentId ="0000",
                 ShipmentName = "Επιλέξτε"
             });

            ViewData["shipmentId"] = new SelectList(shipment, "shipmentId", "ShipmentName", invoice.shipmentId);
            invoice.totalPaymentReceive = invoice.PaymentReceive.Sum(x => x.PaymentAmount);
            invoice.InvoiceBalance = invoice.totalOrderAmount - invoice.totalPaymentReceive;
            _context.Update(invoice);
            await _context.SaveChangesAsync();
            return View(invoice);
        }

        // GET: Invoice/Create
        public IActionResult Create(string id)
        {

            var shipment = (
                    from Shipment in _context.Shipment
                    join Customer in _context.Customer on Shipment.customerId equals Customer.customerId
                    select new
                    {
                        Shipment.shipmentId,
                        ShipmentName = (Shipment.shipmentNumber + " ( " + Customer.customerName + ")")
                    }).ToList();

            shipment.Insert(0,
             new
             {
                 shipmentId = "0000",
                 ShipmentName = "Επιλέξτε"
             });

            if (id != null)
            {
                ViewData["shipmentId"] = new SelectList(shipment, "shipmentId", "ShipmentName", id);
            }
            else
            {
                ViewData["shipmentId"] = new SelectList(shipment, "shipmentId", "ShipmentName");
            }
            Invoice inv = new Invoice();
            inv.shipmentId = id;
            return View(inv);
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,HasChild,InvoiceDate,InvoiceNumber,createdAt,shipmentId,Finalized,CustomerCity,CustomerCountry,CustomerPostCode,CustomerStreet,CustomerTaxOffice,CustomerVATRegNumber,EmployeeName,Fax,OfficePhone,PostalCode,TaxOffice,VATNumber,branchName,city,customerName,description,email,street1,Comments,TotalBeforeDiscount,TotalProductVAT,totalDiscountAmount,totalOrderAmount,CustomerCompanyActivity,CustomerFax,CustomerMobilePhone,CustomerOfficePhone,CustomerWorkEmail,Paid,totalPaymentReceive,InvoiceBalance")] Invoice invoice)
        {

            if (ModelState.IsValid)
            {

                //check Invoice
                Invoice check = await _context.Invoice.Include(x => x.Shipment)
                    .SingleOrDefaultAsync(x => x.shipmentId.Equals(invoice.shipmentId));
                if (check != null)
                {
                    ViewData["StatusMessage"] = "Σφάλμα. Το Τιμολόγιο έχει ήδη δημιουργηθεί. " + check.InvoiceNumber;
                    ViewData["shipmentId"] = new SelectList(_context.Shipment, "shipmentId", "shipmentNumber");

                    return View(invoice);
                }

                _context.Add(invoice);
                invoice.InvoiceNumber = _numberSequence.GetNumberSequence("ΔΑΠ");

                Shipment ship = await _context.Shipment.Where(x => x.shipmentId == invoice.shipmentId).FirstOrDefaultAsync();
                ship.invoiceNumber = invoice.InvoiceNumber;
                _context.Update(ship);

                var query =
                    from Invoice in _context.Invoice
                    join Shipment in _context.Shipment on Invoice.shipmentId equals Shipment.shipmentId
                    join SalesOrder in _context.SalesOrder on Shipment.salesOrderId equals SalesOrder.salesOrderId
                    join Customer in _context.Customer
                          on new { Shipment.customerId, Column1 = SalesOrder.customerId }
                      equals new { Customer.customerId, Column1 = Customer.customerId }
                    join Employee in _context.Employee on Customer.EmployeeId equals Employee.EmployeeId
                    join Branch in _context.Branch on Shipment.branchId equals Branch.branchId
                    join CustomerLine in _context.CustomerLine on SalesOrder.customerLineId equals CustomerLine.customerLineId
                    select new
                    {
                        Branch.branchName,
                        Branch.description,
                        Branch.street1,
                        Branch.PostalCode,
                        Branch.city,
                        Branch.OfficePhone,
                        Branch.Fax,
                        Branch.email,
                        Branch.VATNumber,
                        Branch.TaxOffice,
                        EmployeeName = Employee.DisplayName,
                        SalesOrder.deliveryDate,
                        Customer.customerName,
                        Customer.CompanyActivity,
                        customerStreet1 = CustomerLine.street1,
                        customerPostCode = CustomerLine.PostCode,
                        customerCity = CustomerLine.city,
                        customerCountry = CustomerLine.country,
                        customerVATRegNumber = Customer.VATRegNumber,
                        customerTaxOffice = Customer.TaxOffice,
                        CustomerEmail = Customer.workEmail,
                        CustomerOfficePhone = Customer.officePhone,
                        CustomerMobilePhone = Customer.mobilePhone,
                        CustomerFax = Customer.fax,
                        Invoice.InvoiceId,
                        Comments = SalesOrder.description,
                        SalesOrder.salesOrderNumber,
                        SalesOrder.totalDiscountAmount,
                        SalesOrder.totalOrderAmount,
                        SalesOrder.TotalProductVAT,
                        SalesOrder.TotalWithSpecialTax,
                        SalesOrder.TotalBeforeDiscount,
                        SalesOrder.Invoicing
                    };

                var inv = query.Where(x => x.InvoiceId == invoice.InvoiceId).FirstOrDefault();
                
                if (inv.Invoicing)
                {
                    invoice.InvoiceNumber = _numberSequence.GetNumberSequence("ΤΔΑ");
                }

                invoice.branchName = inv.branchName;
                invoice.description = inv.description;
                invoice.VATNumber = inv.VATNumber;
                invoice.city = inv.city;
                invoice.CustomerCity = inv.customerCity;
                invoice.CustomerCountry = inv.customerCountry;
                invoice.customerName = inv.customerName;
                invoice.CustomerCompanyActivity = inv.CompanyActivity;
                invoice.CustomerPostCode = inv.customerPostCode;
                invoice.CustomerStreet = inv.customerStreet1;
                invoice.CustomerTaxOffice = inv.customerTaxOffice;
                invoice.CustomerVATRegNumber = inv.customerVATRegNumber;
                invoice.CustomerFax = inv.CustomerFax;
                invoice.CustomerMobilePhone = inv.CustomerMobilePhone;
                invoice.CustomerWorkEmail = inv.CustomerEmail;
                invoice.CustomerOfficePhone = inv.CustomerOfficePhone;
                invoice.email = inv.email;
                invoice.EmployeeName = inv.EmployeeName;
                invoice.Fax = inv.Fax;
                invoice.InvoiceDate = inv.deliveryDate;
                invoice.OfficePhone = inv.OfficePhone;
                invoice.PostalCode = inv.PostalCode;
                invoice.street1 = inv.street1;
                invoice.TaxOffice = inv.TaxOffice;
                invoice.TotalBeforeDiscount = inv.TotalBeforeDiscount;
                invoice.totalDiscountAmount = inv.totalDiscountAmount;
                invoice.totalOrderAmount = inv.totalOrderAmount;
                invoice.TotalProductVAT = inv.TotalProductVAT;


                await _context.SaveChangesAsync();


                //auto create shipment line, full shipment               
                List<SalesOrderLine> solines = new List<SalesOrderLine>();
                var salesOrderId = _context.Shipment.Where(x => x.shipmentId == invoice.shipmentId).FirstOrDefault().salesOrderId;
                solines = _context.SalesOrderLine.Include(x => x.Product).Where(x => x.SalesOrderId.Equals(salesOrderId)).ToList();
                foreach (var item in solines)
                {
                    InvoiceLine line = new InvoiceLine();

                    line.Discount = item.Discount;
                    line.DiscountAmount = item.DiscountAmount;
                    line.InvoiceId = invoice.InvoiceId;
                    line.InvoiceLineId = item.SalesOrderLineId;
                    line.Price = item.Price;
                    line.Product = item.Product;
                    line.ProductId = item.ProductId;
                    line.ProductVAT = item.ProductVAT;
                    line.ProductVATAmount = item.ProductVATAmount;
                    line.Qty = item.Qty;
                    line.SpecialTaxAmount = item.SpecialTaxAmount;
                    line.SpecialTaxDiscount = item.SpecialTaxDiscount;
                    line.TotalAfterDiscount = item.TotalAfterDiscount;
                    line.TotalAmount = item.TotalAmount;
                    line.TotalBeforeDiscount = item.TotalBeforeDiscount;
                    line.TotalSpecialTaxAmount = item.TotalSpecialTaxAmount;
                    line.TotalWithSpecialTax = item.TotalWithSpecialTax;
                    line.UnitCost = item.UnitCost;

                    _context.InvoiceLine.Add(line);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Details), new { id = invoice.InvoiceId });
            }
            var shipment =
            from Shipment in _context.Shipment
            join Customer in _context.Customer on Shipment.customerId equals Customer.customerId
            select new
            {
                Shipment.shipmentId,
                ShipmentName = (Shipment.shipmentNumber + " ( " + Customer.customerName + ")")
            };
            ViewData["shipmentId"] = new SelectList(shipment, "shipmentId", "ShipmentName");

            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.SingleOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }
            var shipment =
                        from Shipment in _context.Shipment
                        join Customer in _context.Customer on Shipment.customerId equals Customer.customerId
                        select new
                        {
                            Shipment.shipmentId,
                            ShipmentName = (Shipment.shipmentNumber + " ( " + Customer.customerName + ")")
                        };
            ViewData["shipmentId"] = new SelectList(shipment, "shipmentId", "ShipmentName", invoice.shipmentId);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("InvoiceId,HasChild,InvoiceDate,InvoiceNumber,createdAt,shipmentId,Finalized,CustomerCity,CustomerCountry,CustomerPostCode,CustomerStreet,CustomerTaxOffice,CustomerVATRegNumber,EmployeeName,Fax,OfficePhone,PostalCode,TaxOffice,VATNumber,branchName,city,customerName,description,email,street1,Comments,TotalBeforeDiscount,TotalProductVAT,totalDiscountAmount,totalOrderAmount,CustomerCompanyActivity,CustomerFax,CustomerMobilePhone,CustomerOfficePhone,CustomerWorkEmail,Paid,totalPaymentReceive,InvoiceBalance")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            var shipment =
                        from Shipment in _context.Shipment
                        join Customer in _context.Customer on Shipment.customerId equals Customer.customerId
                        select new
                        {
                            Shipment.shipmentId,
                            ShipmentName = (Shipment.shipmentNumber + " ( " + Customer.customerName + ")")
                        };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            ViewData["shipmentId"] = new SelectList(_context.Shipment, "shipmentId", "ShipmentName", invoice.shipmentId);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(i => i.Shipment)
                .SingleOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }
            var shipment =
                        from Shipment in _context.Shipment
                        join Customer in _context.Customer on Shipment.customerId equals Customer.customerId
                        select new
                        {
                            Shipment.shipmentId,
                            ShipmentName = (Shipment.shipmentNumber + " ( " + Customer.customerName + ")")
                        };
            ViewData["shipmentId"] = new SelectList(shipment, "shipmentId", "ShipmentName", invoice.shipmentId);
            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var invoice = await _context.Invoice.SingleOrDefaultAsync(m => m.InvoiceId == id);
            var shipment = _context.Shipment.Where(x => x.shipmentId == invoice.shipmentId).FirstOrDefault();
            shipment.invoiceNumber = null;

            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ShowInvoice(string id)
        {
            Invoice obj = await _context.Invoice
                .Include(x => x.InvoiceLine).ThenInclude(x => x.Product)
                .Include(x => x.Shipment)
                .ThenInclude(x => x.salesOrder)
                .ThenInclude(x => x.branch)
                .Include(x => x.Shipment).ThenInclude(x => x.Employee)
                .Include(x => x.Shipment).ThenInclude(x => x.customer)
                .SingleOrDefaultAsync(x => x.InvoiceId.Equals(id));

            _context.Update(obj);
            if (obj.Shipment.salesOrder.Invoicing)
            {
                ViewBag.InvoiceTitle = "Τιμολόγιο Δελτίο Αποστολής";
            }
            else
            {
                ViewBag.InvoiceTitle = "Δελτίο Αποστολής Προϊόντων";
            }
            return View(obj);
        }

        public async Task<IActionResult> PrintInvoice(string id)
        {
            Invoice obj = await _context.Invoice
                .Include(x => x.InvoiceLine).ThenInclude(x => x.Product)
                .Include(x => x.Shipment)
                .ThenInclude(x => x.salesOrder)
                .ThenInclude(x => x.branch)
                .Include(x => x.Shipment).ThenInclude(x => x.Employee)
                .Include(x => x.Shipment).ThenInclude(x => x.customer)
                .SingleOrDefaultAsync(x => x.InvoiceId.Equals(id));
            if (obj.Shipment.salesOrder.Invoicing)
            {
                ViewBag.InvoiceTitle = "Τιμολόγιο Δελτίο Αποστολής";
            }
            else
            {
                ViewBag.InvoiceTitle = "Δελτίο Αποστολής Προϊόντων";
            }
            return View(obj);
        }


        private bool InvoiceExists(string id)
        {
            return _context.Invoice.Any(e => e.InvoiceId == id);
        }
    }
}

namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Invoice
        {
            public const string Controller = "Invoice";
            public const string Action = "Index";
            public const string Role = "Invoice";
            public const string Url = "/Invoice/Index";
            public const string Name = "Invoice";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Invoice")]
        public bool InvoiceRole { get; set; } = false;
    }
}
