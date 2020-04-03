using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class PurchaseOrder : INetcoreMasterChild
    {
        public PurchaseOrder()
        {
            this.createdAt = DateTime.UtcNow;
            this.purchaseOrderNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#PO";
            this.poDate = DateTime.Today;
            this.deliveryDate = this.poDate.AddDays(5);
            this.purchaseOrderStatus = PurchaseOrderStatus.Draft;
            this.totalDiscountAmount = 0m;
            this.totalOrderAmount = 0m;
        }

        [StringLength(38)]
        [Display(Name = "Id Παραγγελίας Αγοράς")]
        public string purchaseOrderId { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός")]
        public string purchaseOrderNumber { get; set; }
        
        [Display(Name = "Τρόπος πληρωμής")]
        public TOP top { get; set; }

        [Display(Name = "Ημ. Παραγγελίας")]
        public DateTime poDate { get; set; }

        [Display(Name = "Ημ. Παράδοσης")]
        public DateTime deliveryDate { get; set; }

        [StringLength(30)]
        [Display(Name = "Αριθμός Αναφοράς")]
        public string referenceNumberExternal { get; set; }

        [StringLength(100)]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Υποκαταστήματος")]
        public string branchId { get; set; }

        [Display(Name = "Υποκατάστημα")]
        public Branch branch { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Προμηθευτή")]
        public string vendorId { get; set; }

        [Display(Name = "Προμηθευτής")]
        public Vendor vendor { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "PIC Εσωτερικό")]
        public string picInternal { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "PIC Προμηθευτή")]
        public string picVendor { get; set; }

        [Display(Name = "Κατάσταση")]
        public PurchaseOrderStatus purchaseOrderStatus { get; set; }

        [Display(Name = "Συνολική έκπτωση")]
        public decimal totalDiscountAmount { get; set; }

        [Display(Name = "Σύνολο Παραγγελίας")]
        public decimal totalOrderAmount { get; set; }

        [Display(Name = "Σύνολο Πληρωμών")]
        public decimal totalVendorPayment { get; set; }

        [Display(Name = "Αριθμός παραλαβής")]
        public string purchaseReceiveNumber { get; set; }

        [Display(Name = "Υπόλοιπο Τιμολογίου")]
        public decimal InvoiceBalance { get; set; }

        [Display(Name = "Εξοφλημένο")]
        public Boolean Paid { get; set; }

        [Display(Name = "Συνημμένο pdf")]
        public byte[] File { get; set; }

        [Display(Name = "Στοιχεία Παραγγελίας")]
        public List<PurchaseOrderLine> purchaseOrderLine { get; set; } = new List<PurchaseOrderLine>();

        [Display(Name = "Πληρωμές")]
        public List<VendorPayment> vendorPayment { get; set; } = new List<VendorPayment>();

    }
}
