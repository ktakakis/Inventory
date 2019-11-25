using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public enum ProductType
    {
        [Display(Name = "ESPRESSO")]
        ESPRESSO = 1,
        [Display(Name = "ΚΑΦΕΔΕΣ")]
        COFFEES = 2,
        [Display(Name = "ΤΑΜΠΛΕΤΕΣ")]
         ESPRESSOTABLETS= 3,
        [Display(Name = "ΣΟΚΟΛΑΤΑ")]
        CHOCOLATE = 4,
        [Display(Name = "ΣΙΡΟΠΙΑ")]
        SIROPS = 5,
        [Display(Name = "ΖΑΧΑΡΗ")]
        SOUGAR = 6,
        [Display(Name = "ΤΣΑΪ")]
        TEA = 7,
        [Display(Name = "ΓΡΑΝΙΤΕΣ")]
        GRANITES = 8,
        [Display(Name = "ΠΟΤΗΡΙΑ-ΚΟΥΠΕΣ")]
        GLASSESCUPS = 9,
        [Display(Name = "ΧΑΡΤΟΠΕΤΣΕΤΕΣ")]
        NAPKINS = 10,
        [Display(Name = "ΜΗΧΑΝΗΜΑΤΑ ΚΑΦΕ")]
        COFFEEMACHINES = 12,
        [Display(Name = "ΔΙΑΦΗΜΗΣΤΙΚΑ")]
        ADVERTISING = 13,
        [Display(Name = "ΠΑΡΕΛΚΟΜΕΝΑ")]
        ACCESSORIES = 14,
        [Display(Name = "ΜΥΛΟΙ ΑΛΕΣΗΣ")]
        COFFEEGRINDER = 15,
        [Display(Name = "ΠΡΟΙΟΝΤΑ ΠΑΡΑΓΓΕΛΙΩΝ")]
        ORDERPRODUCTS = 16,
        [Display(Name = "ΔΩΡΑ")]
        GIFTS = 17,
        [Display(Name = "ΑΝΤΑΛΛΑΚΤΙΚΑ")]
        SPAREPARTS = 18

    }
}
