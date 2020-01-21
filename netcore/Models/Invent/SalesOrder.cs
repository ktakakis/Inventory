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
            this.deliveryDate = this.soDate.AddDays(1);
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

        [Display(Name ="Αναγνωριστικό")]
        public string SalesOrderName { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Διεύθυνσης")]
        public string customerLineId { get; set; }

        [Display(Name = "Διεύθυνση παράδοσης")]
        public CustomerLine customerLine { get; set; }

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
        [Display(Name = "Id Employee")]
        public string EmployeeId { get; set; }

        [Display(Name = "Συνεργάτης")] 
        public Employee Employee { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Πελάτη")]
        public string customerId { get; set; }

        [Display(Name = "Πελάτης")]
        public Customer customer { get; set; }

        [Display(Name = "Κατάσταση")]
        public SalesOrderStatus salesOrderStatus { get; set; }

        [Display(Name = "Έκδοση Τιμολογίου")]
        public Boolean Invoicing { get; set; } 

        [Display(Name = "Αξία με Ε. Φόρο")] 
        public decimal TotalWithSpecialTax { get; set; }  

        [Display(Name = "Σύνολο Προ Έκπτωσης")] 
        public decimal TotalBeforeDiscount { get; set; }  


        [Display(Name = "Συνολική έκπτωση")]
        public decimal totalDiscountAmount { get; set; }

        [Display(Name = "Τελική Αξία")]
        public decimal totalOrderAmount { get; set; }

        [Display(Name = "Σύνολο Φ.Π.Α.")]
        public decimal TotalProductVAT { get; set; } 


        [Display(Name = "Αριθμός Αποστολής")]
        public string salesShipmentNumber { get; set; }

        [Display(Name = "Στοιχεία Παραγγελίας")]
        public List<SalesOrderLine> salesOrderLine { get; set; } = new List<SalesOrderLine>();
    }
}
