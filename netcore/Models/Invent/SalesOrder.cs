using netcore.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class SalesOrder : INetcoreMasterChild
    {

        public SalesOrder()
        {
            this.createdAt = DateTime.UtcNow;
            //this.salesOrderNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#SO";
            this.soDate = DateTime.UtcNow.Date;
            this.deliveryDate = this.soDate.AddDays(5);
            this.salesOrderStatus = SalesOrderStatus.Draft;
            this.totalDiscountAmount = 0m;
            this.totalOrderAmount = 0m;
        }

        [StringLength(38)]
        [Display(Name = "Id Παραγγελίας")]
        public string salesOrderId { get; set; }

        [StringLength(20)]
        
        [Display(Name = "Αριθμός Παραγγελίας")]
        public string salesOrderNumber { get; set; }

        [Display(Name = "Οροι πληρωμής (TOP)")]
        public TOP top { get; set; }

        [Display(Name = "Ημερ. Παραγγελίας")]
        public DateTime soDate { get; set; }

        [Display(Name = "Ημερ.Παράδοσης")]
        public DateTime deliveryDate { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Διεύθυνσης")]
        public string customerLineId { get; set; }

        [Display(Name = "Διεύθυνση παράδοσης")]
        public CustomerLine customerLine { get; set; }

        [StringLength(30)]
        [Display(Name = "Αριθμός αναφοράς (Εσωτερικός)")]
        public string referenceNumberInternal { get; set; }

        [StringLength(30)]
        [Display(Name = "Αριθμός αναφοράς (Εξωτερικός)")]
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
        [Display(Name = "Id Πελάτη")]
        public string customerId { get; set; }

        [Display(Name = "Πελάτης")]
        public Customer customer { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "PIC Εσωτερικό")]
        public string picInternal { get; set; }

        [StringLength(30)]
        [Required]
        [Display(Name = "PIC Πελάτη")]
        public string picCustomer { get; set; }

        [Display(Name = "Κατάσταση")]
        public SalesOrderStatus salesOrderStatus { get; set; }

        [Display(Name = "Συνολική έκπτωση")]
        public decimal totalDiscountAmount { get; set; }

        [Display(Name = "Σύνολο Παραγγελίας")]
        public decimal totalOrderAmount { get; set; }

        [Display(Name = "Αριθμός Αποστολής")]
        public string salesShipmentNumber { get; set; }

        [Display(Name = "Στοιχεία Παραγγελίας")]
        public List<SalesOrderLine> salesOrderLine { get; set; } = new List<SalesOrderLine>();
    }
}
