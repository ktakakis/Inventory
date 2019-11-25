using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class TransferOut : INetcoreMasterChild
    {
        public TransferOut()
        {
            this.createdAt = DateTime.UtcNow;
            this.transferOutNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#OUT";
            this.transferOutDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Εξαγωγής")]
        public string transferOutId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Εντολής μεταφοράς")]
        public string transferOrderId { get; set; }

        [Display(Name = "Εντολή μεταφοράς")]
        public TransferOrder transferOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός Εξαγωγής")]
        public string transferOutNumber { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία Εξαγωγής")]
        public DateTime transferOutDate { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }
        

        [StringLength(38)]
        [Display(Name = "Από Id Υποκαταστήματος")]
        public string branchIdFrom { get; set; }

        [Display(Name = "Από υποκατάστημα")]
        public Branch branchFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Από Id Αποθήκης")]
        public string warehouseIdFrom { get; set; }

        [Display(Name = "Από Αποθήκη")]
        public Warehouse warehouseFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Στο Id Υποκαταστήματος")]
        public string branchIdTo { get; set; }

        [Display(Name = "Στο υποκατάστημα")]
        public Branch branchTo { get; set; }

        [StringLength(38)]
        [Display(Name = "Στο Id Αποθήκης")]
        public string warehouseIdTo { get; set; }

        [Display(Name = "Στην Αποθήκη")]
        public Warehouse warehouseTo { get; set; }

        [Display(Name = "Στοιχεία Εξαγωγής")]
        public List<TransferOutLine> transferOutLine { get; set; } = new List<TransferOutLine>();
    }
}
