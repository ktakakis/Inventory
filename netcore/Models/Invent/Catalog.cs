using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Catalog : INetcoreMasterChild
    {
        public Catalog()
        {
            this.createdAt = DateTime.UtcNow;
        }
        [StringLength(38)]
        [Display(Name = "Id Καταλόγου")]
        public string CatalogId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα Καταλόγου")]
        [Required]
        public string CatalogName { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Πελάτη")]
        public string CustomerId { get; set; }

        [Display(Name = "Πελάτης")]
        public Customer Customer { get; set; }

        [Display(Name = "Προϊόντα Καταλόγου")]
        public List<CatalogLine> CatalogLine { get; set; } = new List<CatalogLine>();

    }
}
