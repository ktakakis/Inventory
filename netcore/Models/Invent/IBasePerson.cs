using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public interface IBasePerson
    {
        [Display(Name = "Όνομα")]
        [Required]
        [StringLength(20)]
        string firstName { get; set; }

        [Display(Name = "Επίθετο")]
        [Required]
        [StringLength(20)]
        string lastName { get; set; }

        [Display(Name = "Μεσαίο")]
        [StringLength(20)]
        string middleName { get; set; }

        [Display(Name = "Υποκοριστικό")]
        [StringLength(20)]
        string nickName { get; set; }

        [Display(Name = "Γένος")]
        Gender gender { get; set; }

        [Display(Name = "Χαιρετισμός")]
        Salutation salutation { get; set; }
    }


}
