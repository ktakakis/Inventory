using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class CatalogLine : INetcoreBasic
    {
        public CatalogLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Καταλόγου")]
        public string CatalogLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Καταλόγου")]
        public string CatalogId { get; set; }

        [Display(Name = "Κατάλογος")]
        public Catalog Catalog { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string ProductId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product Product { get; set; }

        [Display(Name = "Εκπτωση")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal? Discount { get; set; }

        [Display(Name = "Ημερ. Λήξης")] 
        public DateTime EndDate { get; set; }

    }
}
