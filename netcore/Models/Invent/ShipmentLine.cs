using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ShipmentLine : INetcoreBasic
    {
        public ShipmentLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id στοιχείου αποστολής")]
        public string shipmentLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Αποστολής")]
        public string shipmentId { get; set; }

        [Display(Name = "Αποστολή")]
        public Shipment shipment { get; set; }

        [StringLength(38)]
        [Display(Name = "Id υποκαταστήματος")]
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

        [Display(Name = "Ποσ")]
        public float qty { get; set; }

        [Display(Name = "Ποσ Αποστολής")]
        public float qtyShipment { get; set; }

        [Display(Name = "Ποσ Αποθέματος")]
        public float qtyInventory { get; set; }
    }
}
