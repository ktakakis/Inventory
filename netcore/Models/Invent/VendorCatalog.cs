using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class VendorCatalog : INetcoreMasterChild
    {
        public VendorCatalog()
        {
            this.createdAt = DateTime.UtcNow;
        }
        [StringLength(38)]
        [Display(Name = "Id Καταλόγου")]
        public string VendorCatalogId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα Καταλόγου")]
        [Required]
        public string VendorCatalogName { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Προμηθευτής")]
        public string VendorId { get; set; }

        [Display(Name = "Προμηθευτής")]
        public Vendor Vendor { get; set; }

        [Display(Name = "Προϊόντα Καταλόγου")]
        public List<VendorCatalogLine> VendorCatalogLine { get; set; } = new List<VendorCatalogLine>();

    }
}
