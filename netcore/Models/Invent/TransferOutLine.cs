using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class TransferOutLine : INetcoreBasic
    {
        public TransferOutLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id")]
        public string transferOutLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Εξαγωγής")]
        public string transferOutId { get; set; }

        [Display(Name = "Εξαγωγή εμπορευμάτων")]
        public TransferOut transferOut { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string productId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product product { get; set; }

        [Display(Name = "Ποσ")]
        public float qty { get; set; }

        [Display(Name = "Ποσότητα αποθέματος")]
        public float qtyInventory { get; set; }
    }
}
