﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using netcore.Models;
using netcore.Models.Invent;

namespace netcore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<netcore.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<netcore.Models.Invent.Branch> Branch { get; set; }

        public DbSet<netcore.Models.Invent.Warehouse> Warehouse { get; set; }

        public DbSet<netcore.Models.Invent.Product> Product { get; set; }

        public DbSet<netcore.Models.Invent.Product> ProductCategory { get; set; }

        public DbSet<netcore.Models.Invent.Vendor> Vendor { get; set; }

        public DbSet<netcore.Models.Invent.VendorLine> VendorLine { get; set; }

        public DbSet<netcore.Models.Invent.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<netcore.Models.Invent.PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<netcore.Models.Invent.Customer> Customer { get; set; }

        public DbSet<netcore.Models.Invent.CustomerLine> CustomerLine { get; set; }

        public DbSet<netcore.Models.Invent.SalesOrder> SalesOrder { get; set; }

        public DbSet<netcore.Models.Invent.SalesOrderLine> SalesOrderLine { get; set; }

        public DbSet<netcore.Models.Invent.Shipment> Shipment { get; set; }

        public DbSet<netcore.Models.Invent.ShipmentLine> ShipmentLine { get; set; }

        public DbSet<netcore.Models.Invent.Receiving> Receiving { get; set; }

        public DbSet<netcore.Models.Invent.ReceivingLine> ReceivingLine { get; set; }

        public DbSet<netcore.Models.Invent.TransferOrder> TransferOrder { get; set; }

        public DbSet<netcore.Models.Invent.TransferOrderLine> TransferOrderLine { get; set; }

        public DbSet<netcore.Models.Invent.TransferOut> TransferOut { get; set; }

        public DbSet<netcore.Models.Invent.TransferOutLine> TransferOutLine { get; set; }

        public DbSet<netcore.Models.Invent.TransferIn> TransferIn { get; set; }

        public DbSet<netcore.Models.Invent.TransferInLine> TransferInLine { get; set; }

        public DbSet<netcore.Models.Invent.City> City { get; set; }
        public DbSet<netcore.Models.Invent.Catalog> Catalog { get; set; }

        public DbSet<netcore.Models.Invent.CatalogLine> CatalogLine { get; set; }

        public DbSet<netcore.Models.Invent.Employee> Employee { get; set; }

        public DbSet<netcore.Models.Invent.NumberSequence> NumberSequence { get; set; } 
        public DbSet<netcore.Models.Invent.Invoice> Invoice { get; set; }
        public DbSet<netcore.Models.Invent.InvoiceLine> InvoiceLine { get; set; }
        public DbSet<netcore.Models.Invent.PaymentType> PaymentType { get; set; }

        public DbSet<netcore.Models.Invent.PaymentReceive> PaymentReceive { get; set; }

        public DbSet<netcore.Models.Invent.CashRepository> CashRepository { get; set; } 

        public DbSet<netcore.Models.Invent.MoneyTransferOrder> MoneyTransferOrder { get; set; }

        public DbSet<netcore.Models.Invent.CashflowIn> CashflowIn { get; set; }

        public DbSet<netcore.Models.Invent.CashflowOut> CashflowOut { get; set; }

        public DbSet<netcore.Models.Invent.Category> Category { get; set; }

        public DbSet<netcore.Models.Invent.VendorPayment> VendorPayment { get; set; }

        public DbSet<netcore.Models.Invent.VendorCatalog> VendorCatalog { get; set; }

        public DbSet<netcore.Models.Invent.VendorCatalogLine> VendorCatalogLine { get; set; }

        public DbSet<netcore.Models.Invent.EmployeePayment> EmployeePayment { get; set; }

        public DbSet<netcore.Models.Invent.ProductLine> ProductLine { get; set; }

        public DbSet<netcore.Models.Invent.ProductionOrder> ProductionOrder { get; set; }

        public DbSet<netcore.Models.Invent.ProductionOrderLine> ProductionOrderLine { get; set; } 

        public DbSet<netcore.Models.Invent.Production> Production { get; set; }

        public DbSet<netcore.Models.Invent.ProductionLine> ProductionLine { get; set; } 

        public DbSet<netcore.Models.Invent.RoastingLevel> RoastingLevel { get; set; }

        public DbSet<netcore.Models.Invent.RoastingLog> RoastingLog { get; set; }  

    }
}
