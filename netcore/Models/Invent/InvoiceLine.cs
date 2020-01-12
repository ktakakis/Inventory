using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class InvoiceLine : INetcoreBasic
    {
        public InvoiceLine() 
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Τιμολογίου")]
        public string InvoiceLineId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Τιμολογίου")]
        public string InvoiceId { get; set; }

        [Display(Name = "Τιμολόγιο")]
        public Invoice Invoice { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string ProductId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product Product { get; set; }

        [Display(Name = "Ποσ")]
        public float Qty { get; set; }

        [Display(Name = "Τιμή Μονάδος")]
        public decimal Price { get; set; }

        [Display(Name = "Αξία προ Έκπτωσης")]
        public decimal? TotalBeforeDiscount { get; set; }

        [Display(Name = "Τιμή Αγοράς")]
        public decimal? UnitCost { get; set; }

        [Display(Name = "Ποσοστό Έκπτωσης.")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal? Discount { get; set; }

        [Display(Name = "Ποσό έκπτωσης")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Αξία μετά Έκπτωσης")]
        public decimal? TotalAfterDiscount { get; set; }

        [Display(Name = "Ποσοστό Φ.Π.Α.")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal ProductVAT { get; set; }

        [Display(Name = "Φ.Π.Α.")]
        public decimal ProductVATAmount { get; set; }

        [Display(Name = "Έκπτωση ΚΣ")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal SpecialTaxDiscount { get; set; }


        [Display(Name = "Ειδικός Φόρος")]
        public decimal SpecialTaxAmount { get; set; }

        [Display(Name = "Σύνολο Ειδικού Φόρου")]
        public decimal TotalSpecialTaxAmount { get; set; }

        [Display(Name = "Αξία+Φόρος")]
        public decimal TotalWithSpecialTax { get; set; }

        [Display(Name = "Σύνολο")]
        public decimal TotalAmount { get; set; }
    }
}

