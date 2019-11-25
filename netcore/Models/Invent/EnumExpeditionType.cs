using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum ExpeditionType
    {
        [Display(Name = "Εσωτερικό")]
        Internal = 1,
        [Display(Name = "Εξωτερικό")]
        External = 2
    }
}
