using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class TransferIn : INetcoreMasterChild
    {
        public TransferIn()
        {
            this.createdAt = DateTime.UtcNow;
            this.transferInNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#IN";
            this.transferInDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Εισαγωγής")]
        public string transferInId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Παραγγελία Μεταφοράς")]
        public string transferOrderId { get; set; }

        [Display(Name = "Παραγγελία Μεταφοράς")]
        public TransferOrder transferOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός παραλαβής εμπορευμάτων")]
        public string transferInNumber { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία παραλαβής εμπορευμάτων")]
        public DateTime transferInDate { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }


        [StringLength(38)]
        [Display(Name = "Από Id υποκαταστήματος")]
        public string branchIdFrom { get; set; }

        [Display(Name = "Από υποκατάστημα")]
        public Branch branchFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Από Id Αποθήκης")]
        public string warehouseIdFrom { get; set; }

        [Display(Name = "Από Αποθήκη")]
        public Warehouse warehouseFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Στο Id υποκαταστήματος")]
        public string branchIdTo { get; set; }

        [Display(Name = "Στο υποκατάστημα")]
        public Branch branchTo { get; set; }

        [StringLength(38)]
        [Display(Name = "Στο Id Αποθήκης")]
        public string warehouseIdTo { get; set; }

        [Display(Name = "Στην Αποθήκη")]
        public Warehouse warehouseTo { get; set; }

        [Display(Name = "Στοιχεία Εισαγωγής")]
        public List<TransferInLine> transferInLine { get; set; } = new List<TransferInLine>();
    }
}
