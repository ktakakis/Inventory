using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using netcore.Data;
using netcore.Models;
using netcore.Models.Invent;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace netcore.Services
{
    public class NetcoreService : INetcoreService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRoles _roles;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;

        public NetcoreService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IRoles roles,
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _roles = roles;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
        }

        public async Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromFullName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email, email));
            await client.SendEmailAsync(msg);

        }

        public async Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL)
        {
            var body = messageBody;
            var message = new MailMessage();
            message.To.Add(new MailAddress(toEmail, toFullName));
            message.From = new MailAddress(fromEmail, fromFullName);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpUser,
                    Password = smtpPassword
                };
                smtp.Credentials = credential;
                smtp.Host = smtpHost;
                smtp.Port = smtpPort;
                smtp.EnableSsl = smtpSSL;
                await smtp.SendMailAsync(message);

            }

        }

        public async Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager)
        {
            bool result = false;
            try
            {
                var user = await userManager.FindByNameAsync(email);
                if (user != null)
                {
                    //Add this to check if the email was confirmed.
                    if (await userManager.IsEmailConfirmedAsync(user))
                    {
                        result = true;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        public async Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env)
        {
            var result = "";

            var webRoot = env.WebRootPath;
            var uploads = System.IO.Path.Combine(webRoot, "uploads");
            var extension = "";
            var filePath = "";
            var fileName = "";


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(formFile.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    result = fileName;

                }
            }

            return result;
        }

        public async Task UpdateRoles(ApplicationUser appUser,
            ApplicationUser currentLoginUser)
        {
            try
            {
                await _roles.UpdateRoles(appUser, currentLoginUser);

                //so no need to manually re-signIn to make roles changes effective
                if (currentLoginUser.Id == appUser.Id)
                {
                    await _signInManager.SignInAsync(appUser, false);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateDefaultSuperAdmin()
        {
            try
            {
                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;
                superAdmin.isSuperAdmin = true;

                Type t = superAdmin.GetType();
                foreach (System.Reflection.PropertyInfo item in t.GetProperties())
                {
                    if (item.Name.Contains("Role"))
                    {
                        item.SetValue(superAdmin, true);
                    }
                }

                await _userManager.CreateAsync(superAdmin, _superAdminDefaultOptions.Password);
                await _userManager.AddToRoleAsync(superAdmin, "Secretary");
                //loop all the roles and then fill to SuperAdmin so he become powerfull
                foreach (var item in typeof(netcore.MVC.Pages).GetNestedTypes())
                {
                    var roleName = item.Name;
                    if (!await _roleManager.RoleExistsAsync(roleName)) { await _roleManager.CreateAsync(new IdentityRole(roleName)); }

                    await _userManager.AddToRoleAsync(superAdmin, roleName);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task CreateDefaultRoles()
        {
            try
            {
                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin.Email = _superAdminDefaultOptions.Email;
                superAdmin.UserName = superAdmin.Email;
                superAdmin.EmailConfirmed = true;
                superAdmin.isSuperAdmin = true;

                
                    var roleName = "Secretary";
                    if (!await _roleManager.RoleExistsAsync(roleName)) { await _roleManager.CreateAsync(new IdentityRole(roleName)); }

                    await _userManager.AddToRoleAsync(superAdmin, roleName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public VMStock GetStockByProductAndWarehouse(string productId, string warehouseId)
        {
            VMStock result = new VMStock();

            try
            {
                Product product = _context.Product.Where(x => x.productId.Equals(productId)).FirstOrDefault();
                Warehouse warehouse = _context.Warehouse.Where(x => x.warehouseId.Equals(warehouseId)).FirstOrDefault();

                if (product != null && warehouse != null)
                {
                    VMStock stock = new VMStock
                    {
                        Product = product.productCode,
                        Warehouse = warehouse.warehouseName,
                        QtyReceiving = _context.ReceivingLine.Where(x => x.productId.Equals(product.productId) && x.warehouseId.Equals(warehouse.warehouseId)).Sum(x => x.qtyReceive),
                        QtyShipment = _context.ShipmentLine.Where(x => x.productId.Equals(product.productId) && x.warehouseId.Equals(warehouse.warehouseId)).Sum(x => x.qtyShipment),
                        QtyTransferIn = _context.TransferInLine.Where(x => x.productId.Equals(product.productId) && x.transferIn.warehouseIdTo.Equals(warehouse.warehouseId)).Sum(x => x.qty),
                        QtyTransferOut = _context.TransferOutLine.Where(x => x.productId.Equals(product.productId) && x.transferOut.warehouseIdFrom.Equals(warehouse.warehouseId)).Sum(x => x.qty)
                    };
                    stock.QtyOnhand = stock.QtyReceiving + stock.QtyTransferIn - stock.QtyShipment - stock.QtyTransferOut;

                    result = stock;
                }

                
            }
            catch (Exception)
            {

                throw;
            }

            return result;

        }

        public List<VMStock> GetStockPerWarehouse()
        {
            List<VMStock> result = new List<VMStock>();

            try
            {
                List<VMStock> stocks = new List<VMStock>();
                List<Product> products = new List<Product>();
                List<Warehouse> warehouses = new List<Warehouse>();
                warehouses = _context.Warehouse.ToList();
                products = _context.Product.ToList();
                foreach (var item in products)
                {
                    foreach (var wh in warehouses)
                    {
                        VMStock stock = stock = GetStockByProductAndWarehouse(item.productId, wh.warehouseId);
                        
                        if (stock != null) stocks.Add(stock);

                    }


                }

                result = stocks;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<TimelineViewModel> GetTimelinesByProductId(string productId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<PurchaseOrderLine> polist = _context.PurchaseOrderLine.Include(x => x.purchaseOrder).Where(x => x.productId.Equals(productId)).OrderByDescending(x => x.purchaseOrder.poDate).Take(3).ToList();
                List<SalesOrderLine> solist = _context.SalesOrderLine.Include(x => x.SalesOrder).Where(x => x.ProductId.Equals(productId)).OrderByDescending(x => x.SalesOrder.soDate).Take(3).ToList();

                foreach (var item in polist)
                {
                    times.Add(new TimelineViewModel { Header = item.purchaseOrder.poDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα παραγγελία Αγοράς: " + item.purchaseOrder.purchaseOrderNumber + " ", Icon = "fa-file-text", CreatedDate = item.purchaseOrder.poDate, ItemId = item.purchaseOrderId, Controler = "PurchaseOrder" });
                }

                foreach (var item in solist)
                {
                    times.Add(new TimelineViewModel { Header = item.SalesOrder.soDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα παραγγελία Πώλησης: " + item.SalesOrder.salesOrderNumber + " ", Icon = "fa-cart-plus", CreatedDate = item.SalesOrder.soDate, ItemId = item.SalesOrderId, Controler = "SalesOrder" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public List<TimelineViewModel> GetTimelinesByBranchId(string branchId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<PurchaseOrder> polist = _context.PurchaseOrder.Where(x => x.branchId.Equals(branchId)).OrderByDescending(x => x.poDate).Take(3).ToList();
                List<SalesOrder> solist = _context.SalesOrder.Where(x => x.branchId.Equals(branchId)).OrderByDescending(x => x.soDate).Take(3).ToList();

                foreach (var item in polist)
                {
                    times.Add(new TimelineViewModel { Header = item.poDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα παραγγελία Αγοράς: " + item.purchaseOrderNumber + " ", Icon = "fa-file-text", CreatedDate = item.poDate, ItemId = item.purchaseOrderId, Controler = "PurchaseOrder" });
                }

                foreach (var item in solist)
                {
                    times.Add(new TimelineViewModel { Header = item.soDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα παραγγελία Πώλησης: " + item.salesOrderNumber + " ", Icon = "fa-cart-plus", CreatedDate = item.soDate, ItemId = item.salesOrderId, Controler = "SalesOrder" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public List<TimelineViewModel> GetTimelinesByVendorId(string vendorId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<PurchaseOrder> polist = _context.PurchaseOrder.Where(x => x.vendorId.Equals(vendorId)).OrderByDescending(x => x.poDate).Take(6).ToList();

                foreach (var item in polist)
                {
                    times.Add(new TimelineViewModel { Header = item.poDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα παραγγελία Αγοράς: " + item.purchaseOrderNumber + " ", Icon = "fa-file-text", CreatedDate = item.poDate, ItemId = item.purchaseOrderId, Controler = "PurchaseOrder" });
                }
                

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public List<TimelineViewModel> GetTimelinesByCustomerId(string customerId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<SalesOrder> solist = _context.SalesOrder.Where(x => x.customerId.Equals(customerId)).OrderByDescending(x => x.soDate).Take(3).ToList();
                
                foreach (var item in solist)
                {
                    times.Add(new TimelineViewModel { Header = item.soDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα παραγγελία Πώλησης: " + item.salesOrderNumber + " ", Icon = "fa-cart-plus", CreatedDate = item.soDate, ItemId = item.salesOrderId, Controler = "SalesOrder" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public List<TimelineViewModel> GetTimelinesByTransferId(string transferId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<TransferOut> outlist = _context.TransferOut.Where(x => x.transferOrderId.Equals(transferId)).OrderByDescending(x => x.transferOutDate).Take(3).ToList();
                List<TransferIn> inlist = _context.TransferIn.Where(x => x.transferOrderId.Equals(transferId)).OrderByDescending(x => x.transferInDate).Take(3).ToList();

                foreach (var item in outlist)
                {
                    times.Add(new TimelineViewModel { Header = item.transferOutDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργία νέας Αποστολής Προϊόντων: " + item.transferOutNumber + " ", Icon = "fa-upload", CreatedDate = item.transferOutDate, ItemId = item.transferOutId, Controler = "TransferOut" });
                }

                foreach (var item in inlist)
                {
                    times.Add(new TimelineViewModel { Header = item.transferInDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργία νέας παραλαβής Προϊόντων: " + item.transferInNumber + " ", Icon = "fa-download", CreatedDate = item.transferInDate, ItemId = item.transferInId, Controler = "TransferIn" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }
        public List<TimelineViewModel> GetTimelinesByPurchaseId(string purchaseId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<Receiving> list = _context.Receiving.Where(x => x.purchaseOrderId.Equals(purchaseId)).ToList();

                foreach (var item in list)
                {
                    times.Add(new TimelineViewModel { Header = item.receivingDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα Παραλαβή: " + item.receivingNumber + " ", Icon = "fa-truck", CreatedDate = item.receivingDate, ItemId = item.receivingId, Controler = "Receiving" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public List<TimelineViewModel> GetTimelinesBySalesId(string salesId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<Shipment> list = _context.Shipment.Where(x => x.salesOrderId.Equals(salesId)).ToList();

                foreach (var item in list)
                {
                    times.Add(new TimelineViewModel { Header = item.shipmentDate.ToString("dd-MMM-yyyy"), Body = "Δημιουργήθηκε νέα αποστολή: " + item.shipmentNumber + " ", Icon = "fa-plane", CreatedDate = item.shipmentDate, ItemId = item.shipmentId, Controler = "Shipment" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public List<TimelineViewModel> GetTimelinesByShipmentId(string shipmentId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<Invoice> invoiceslist = _context.Invoice.Where(x => x.shipmentId.Equals(shipmentId)).OrderByDescending(x => x.InvoiceDate).Take(3).ToList();

                foreach (var item in invoiceslist)
                {
                    times.Add(new TimelineViewModel { Header = item.InvoiceDate.ToString("dd-MMM-yyyy"), Body = "Αρ. ΤΔΑ-ΔΑΠ: " + item.InvoiceNumber + " Ποσόν: " + item.totalOrderAmount +" €", Icon = "fa-money", CreatedDate = item.createdAt, ItemId = item.InvoiceId, Controler= "Invoice" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }
        public List<TimelineViewModel> GetTimelinesByInvoiceId(string invoiceId)
        {
            List<TimelineViewModel> results = new List<TimelineViewModel>();

            try
            {
                List<TimelineViewModel> times = new List<TimelineViewModel>();
                List<PaymentReceive> paymentslist = _context.PaymentReceive.Where(x => x.InvoiceId.Equals(invoiceId)).OrderByDescending(x => x.PaymentDate).Take(3).ToList();

                foreach (var item in paymentslist)
                {
                    times.Add(new TimelineViewModel { Header = item.PaymentDate.ToString("dd-MMM-yyyy"), Body = "Αρ. Είσπραξης: " + item.PaymentReceiveName + " Ποσόν: " + item.PaymentAmount +" €", Icon = "fa-money", CreatedDate = item.createdAt, ItemId = item.PaymentReceiveId, Controler= "PaymentReceive" });
                }

                results = times.OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }
    }
}
