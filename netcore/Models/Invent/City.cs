using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class City:INetcoreBasic
    {
        public City()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Πόλης")]
        public string CityId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα Πόλης")]
        [Required]
        public string CityName { get; set; }

        [Display(Name = "Ποσοστό Φ.Π.Α.")]
        public decimal ProductVAT { get; set; }

    }
}
