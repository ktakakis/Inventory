using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum TOP
    {
        [Display(Name = "Μετρητά")]
        D00 = 0,
        [Display(Name = "5 μέρες")]
        D05 = 5,
        [Display(Name = "10 μέρες")]
        D10 = 10,
        [Display(Name = "15 μέρες")]
        D15 = 15
    }
}
