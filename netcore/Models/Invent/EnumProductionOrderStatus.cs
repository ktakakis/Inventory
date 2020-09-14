using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum ProductionOrderStatus
    {
        [Display(Name = "Πρόχειρο")]
        Draft = -1,
        [Display(Name = "Ανοιχτό")]
        Open = 1,
        [Display(Name = "Ολοκληρωμένο")]
        Completed = 2 
    }
}
