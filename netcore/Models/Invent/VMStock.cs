using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class VMStock
    {
        [Display(Name = "Προϊόν")]
        public string Product { get; set; }
        [Display(Name = "Αποθήκη")]
        public string Warehouse { get; set; }
        [Display(Name = "Αγορές")]
        public float QtyPO { get; set; }
        [Display(Name = "Παραλαβές")]
        public float QtyReceiving { get; set; }
        [Display(Name = "Πωλήσεις")]
        public float QtySO { get; set; }
        [Display(Name = "Αποστολές")]
        public float QtyShipment { get; set; }
        [Display(Name = "Εισαγωγές")]
        public float QtyTransferIn { get; set; }
        [Display(Name = "Εξαγωγές")]
        public float QtyTransferOut { get; set; }
        [Display(Name = "Στο χέρι")]
        public float QtyOnhand { get; set; }
    }
}
