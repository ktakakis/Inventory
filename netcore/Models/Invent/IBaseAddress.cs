using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public interface IBaseAddress
    {
        [Display(Name = "Διεύθυνση")]
        [Required]
        [StringLength(50)]
        string street1 { get; set; }

        [Display(Name = "Πόλη")]
        [StringLength(30)]
        string city { get; set; }

        [Display(Name = "Νομός")]
        [StringLength(30)]
        string province { get; set; }

        [Display(Name = "Χώρα")]
        [StringLength(30)]
        string country { get; set; }
    }
}
