using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum ExpeditionMode
    {
        [Display(Name = "Χερσαία")]
        Land = 1,
        [Display(Name = "Θαλάσσια")]
        Sea = 2,
        [Display(Name = "Εναέρια")]
        Air = 3
    }
}
