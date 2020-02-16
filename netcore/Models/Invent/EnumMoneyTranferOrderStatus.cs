using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum MoneyTransferOrderStatus
    {
        [Display(Name = "Αίτηση")]
        Draft = -1,
        [Display(Name = "Έγκριση")]
        Open = 1,
        [Display(Name = "Ολοκληρωμένο")]
        Completed = 2
    }
}
