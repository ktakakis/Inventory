using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class SalesOrderLine : INetcoreBasic
    {
        public SalesOrderLine()
        {
            this.createdAt = DateTime.UtcNow;
            this.DiscountAmount = 0;
            this.TotalAmount = 0;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Παραγγελίας Πώλησης")]
        public string SalesOrderLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Παραγγελίας Πώλησης")]
        public string SalesOrderId { get; set; }

        [Display(Name = "Παραγγελία Πώλησης")]
        public SalesOrder SalesOrder { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string ProductId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product Product { get; set; }

        [Display(Name = "Ποσ")]
        public float Qty { get; set; }

        [Display(Name = "Τιμή Πώλησης")]
        public decimal Price { get; set; }
        
        [Display(Name = "Τιμή Αγοράς")]
        public decimal? UnitCost { get; set; }

        //Product Discount
        [Display(Name = "Ποσοστό Έκπτωσης.")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal? Discount { get; set; }

        //Product amount after Discount
        [Display(Name = "Ποσό έκπτωσης")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Ποσοστό Φ.Π.Α.")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal ProductVAT { get; set; }
        
        [Display(Name = "Ποσό Φ.Π.Α.")]
        public decimal ProductVATAmount { get; set; }

        [Display(Name = "Έκπτωση Ειδικού Φόρου")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal SpecialTaxDiscount { get; set; }
        

        [Display(Name = "Ποσό Ειδικού Φόρου")]
        public decimal SpecialTaxAmount { get; set; }

        [Display(Name = "Συνολικό ποσό")]
        public decimal TotalAmount { get; set; }
    }
}
