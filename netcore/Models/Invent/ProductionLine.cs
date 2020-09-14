using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ProductionLine : INetcoreBasic
    {
        public ProductionLine()
        {
            this.createdAt = DateTime.UtcNow;
        }
         
        [StringLength(38)]
        [Display(Name = "Id στοιχείου παραγωγής")]
        public string ProductionLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Παραγγελία Παραγωγής")]
        public string ProductionId { get; set; }

        [Display(Name = "Παραγγελία Παραγωγής")]
        public Production Production { get; set; }

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

    }
}
