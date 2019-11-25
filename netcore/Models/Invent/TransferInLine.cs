using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class TransferInLine : INetcoreBasic
    {
        public TransferInLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id στοιχείου εισαγωγής")]
        public string transferInLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Εισαγωγής")]
        public string transferInId { get; set; }

        [Display(Name = "Παραλαβή εμπορευμάτων")]
        public TransferIn transferIn { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string productId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product product { get; set; }

        [Display(Name = "Ποσ")]
        public float qty { get; set; }

        [Display(Name = "Ποσ. αποθέματος")]
        public float qtyInventory { get; set; }
    }
}
