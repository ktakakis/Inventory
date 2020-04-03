using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Product: INetcoreMasterChild
    {
        public Product()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string productId { get; set; }

        [StringLength(50)]
        [Display(Name = "Κωδικός")]
        [Required]
        public string productCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Ονομασία")]
        [Required]
        public string productName { get; set; }

        [StringLength(50)]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }

        [StringLength(50)]
        [Display(Name = "Barcode")]
        public string barcode { get; set; }

        [StringLength(50)]
        [Display(Name = "Σειριακός αριθμός")]
        public string serialNumber { get; set; }
        
        [Display(Name = "Κατηγορία")]
        public ProductType productType { get; set; }
       
        [Display(Name = "Μονάδα μέτρησης (UOM)")]
        public UOM uom { get; set; }

        [Display(Name ="Τιμή Πώλησης")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Κόστος Αγοράς")]
        public decimal? UnitCost { get; set; }

        [Display(Name ="Φ.Π.Α.")]
        public decimal ProductVAT { get; set; }

        [Display(Name = "Ειδικός Φόρος")]
        public decimal SpecialTaxValue { get; set; }

        [Display(Name = "Βάρος")]
        public decimal? ProductWeight { get; set; }

        [Display(Name = "Όγκος")]
        public decimal? ProductVolume { get; set; }

        [Display(Name = "Επίπεδο Παραγγελίας")]
        public int? ReorderLevel { get; set; }
        
        [Display(Name = "Ενεργό Προϊόν")]
        public Boolean  Discontinued { get; set; }


    }
}
