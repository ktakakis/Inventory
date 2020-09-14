using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ProductionOrder: INetcoreMasterChild
    {
        public ProductionOrder()
        {
            this.createdAt = DateTime.UtcNow;
            this.ProductionOrderDate = DateTime.UtcNow.Date;
            this.RequiredDeliveryDate = ProductionOrderDate.AddDays(5);
            this.ProductionOrderStatus = ProductionOrderStatus.Draft;
        }

        [StringLength(38)]
        [Display(Name = "Id Παραγωγής")]
        public string ProductionOrderId { get; set; }

        [Display(Name = "Αριθμός Παραγωγής")]
        public string ProductionOrderNumber { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Υποκατάστημα")]
        public string branchId { get; set; }

        [Display(Name = "Υποκατάστημα")]
        public Branch branch { get; set; }

        [StringLength(100)] 
        [Display(Name = "Περιγραφή")]
        public string Description { get; set; } 

        [Display(Name = "Ημερ. Παραγγελίας")]
        public DateTime ProductionOrderDate { get; set; }

        [Display(Name = "Ημερ. Παράδοσης")]
        public DateTime RequiredDeliveryDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Σημειώσεις")]
        public string Notes { get; set; }

        [Display(Name = "Κατάσταση")] 
        public ProductionOrderStatus ProductionOrderStatus { get; set; }

        [Display(Name = "Στοιχεία Παραγγελίας")] 
        public List<ProductionOrderLine> ProductionOrderLine { get; set; } = new List<ProductionOrderLine>();


    }
}
