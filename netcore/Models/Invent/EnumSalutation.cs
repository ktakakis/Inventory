using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum Salutation
    {
        [Display(Name = "κ")]
        Mr = 1,
        [Display(Name = "Κα")]
        Mrs = 2,
        [Display(Name = "Δς")]
        Ms = 3
    }
}
