using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class TransferOrderLine : INetcoreBasic
    {
        public TransferOrderLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Αγοράς")]
        public string transferOrderLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Εντολής Μεταφοράς")]
        public string transferOrderId { get; set; }

        [Display(Name = "Εντολή Μεταφοράς")]
        public TransferOrder transferOrder { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string productId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product product { get; set; }

        [Display(Name = "Ποσ")]
        public float qty { get; set; }
    }
}
