using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ReceivingLine : INetcoreBasic
    {
        public ReceivingLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Παραλαβής")]
        public string receivingLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Παραλαβής")]
        public string receivingId { get; set; }

        [Display(Name = "Παραλαβή")]
        public Receiving receiving { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Υποκαταστήματος")]
        public string branchId { get; set; }

        [Display(Name = "Υποκατάστημα")]
        public Branch branch { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Αποθήκης")]
        public string warehouseId { get; set; }

        [Display(Name = "Αποθήκη")]
        public Warehouse warehouse { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string productId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product product { get; set; }

        [Display(Name = "Ποσ Παραγγελίας")]
        public float qty { get; set; }

        [Display(Name = "Ποσ Παραλαβής")]
        public float qtyReceive { get; set; }

        [Display(Name = "Ποσ Αποθέματος")]
        public float qtyInventory { get; set; }
    }
}
