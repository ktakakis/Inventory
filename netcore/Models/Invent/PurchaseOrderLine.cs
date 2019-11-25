using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class PurchaseOrderLine : INetcoreBasic
    {
        public PurchaseOrderLine()
        {
            this.createdAt = DateTime.UtcNow;
            this.discountAmount = 0m;
            this.totalAmount = 0m;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου παραγγελίας Αγοράς")]
        public string purchaseOrderLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Παραγγελίας Αγοράς")]
        public string purchaseOrderId { get; set; }

        [Display(Name = "Παραγγελία Αγοράς")]
        public PurchaseOrder purchaseOrder { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string productId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product product { get; set; }

        [Display(Name = "Ποσ")]
        public float qty { get; set; }

        [Display(Name = "Τιμή προϊόντος")]
        public decimal price { get; set; }

        [Display(Name = "Ποσό έκπτωσης")]
        public decimal discountAmount { get; set; }

        [Display(Name = "Συνολικό ποσό")]
        public decimal totalAmount { get; set; }
    }
}
