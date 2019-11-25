using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum Gender
    {
        [Display(Name = "Άνδρας")]
        Male = 1,
        [Display(Name = "Γυναίκα")]
        Female = 2
    }
}
