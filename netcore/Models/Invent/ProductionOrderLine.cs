using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ProductionOrderLine : INetcoreBasic
    {
        public ProductionOrderLine() 
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Παραγγελίας Παραγωγής")]
        public string ProductionOrderLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Παραγγελίας Πώλησης")]
        public string ProductionOrderId { get; set; }

        [Display(Name = "Παραγγελία Πώλησης")]
        public ProductionOrder ProductionOrder { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string ProductId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product Product { get; set; }

        [Display(Name = "Ποσ")]
        public float Qty { get; set; }

    }
}
