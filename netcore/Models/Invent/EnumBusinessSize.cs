using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum BusinessSize
        {
            [Display(Name = "Μικρή")]
            Small = 1,
            [Display(Name = "Μεσαία")]
            Medium = 2,
            [Display(Name = "Μικρομεσαία")]
            SMB = 3,
            [Display(Name = "Μεγάλη")]
            Enterprise = 4,
            [Display(Name = "Δημόσιο")]
            Government = 5,
            [Display(Name = "Μη Κερδοσκοπική")]
            NGO = 6
        }
}

