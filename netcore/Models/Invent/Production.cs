using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Production : INetcoreMasterChild
    {
        public Production()
        {
            this.createdAt = DateTime.UtcNow;
            this.ProductionDate = DateTime.UtcNow.Date;
        }

        [StringLength(38)]
        [Display(Name = "Id Παραγωγής")]
        public string ProductionId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Παραγγελίας Παραγωγής")]
        public string ProductionOrderId { get; set; }

        [Display(Name = "Παραγγελία Παραγωγής")]
        public ProductionOrder ProductionOrder { get; set; }

        [StringLength(20)]        
        [Display(Name = "Αριθμός Παραγωγής")]
        public string ProductionNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        [StringLength(100)]
        [Display(Name = "Σημειώσεις")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία Παραγωγής")]
        public DateTime ProductionDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Id υποκαταστήματος")]
        public string branchId { get; set; }

        [Display(Name = "Υποκατάστημα")]
        public Branch branch { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Αποθήκης")]
        public string warehouseId { get; set; }

        [Display(Name = "Αποθήκη")]
        public Warehouse warehouse { get; set; }

        [Display(Name = "Κατάσταση")]
        public ProductionStatus ProductionStatus { get; set; }

        [Display(Name = "Στοιχεία Παραγγελίας")]
        public List<ProductionLine> ProductionLine { get; set; } = new List<ProductionLine>();

    }
}
