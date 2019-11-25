using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class TransferOrder : INetcoreMasterChild
    {
        public TransferOrder()
        {
            this.createdAt = DateTime.UtcNow;
            this.transferOrderNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#TO";
            this.transferOrderDate = DateTime.UtcNow;
            this.transferOrderStatus = TransferOrderStatus.Draft;
            this.isIssued = false;
            this.isReceived = false;
        }

        [StringLength(38)]
        [Display(Name = "Id Μεταφοράς")]
        public string transferOrderId { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός Μεταφοράς")]
        public string transferOrderNumber { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία Μεταφοράς")]
        public DateTime transferOrderDate { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "PIC Name")]
        public string picName { get; set; }

        [StringLength(38)]
        [Display(Name = "Από υποκατάστημα Id")]
        public string branchIdFrom { get; set; }

        [Display(Name = "Από υποκατάστημα")]
        public Branch branchFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Από Αποθήκη Id")]
        public string warehouseIdFrom { get; set; }

        [Display(Name = "Από Αποθήκη")]
        public Warehouse warehouseFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Στο υποκατάστημα Id")]
        public string branchIdTo { get; set; }

        [Display(Name = "Στο υποκατάστημα")]
        public Branch branchTo { get; set; }

        [StringLength(38)]
        [Display(Name = "Στην Αποθήκη Id")]
        public string warehouseIdTo { get; set; }

        [Display(Name = "Στην Αποθήκη")]
        public Warehouse warehouseTo { get; set; }

        [Display(Name = "Κατάσταση παραγγελίας Μεταφοράς")]
        public TransferOrderStatus transferOrderStatus { get; set; }

        [Display(Name = "Έχει Εκδοθεί")]
        public bool isIssued { get; set; }

        [Display(Name = "Έχει παραληφθεί")]
        public bool isReceived { get; set; }

        [Display(Name = "Στοιχεία Μεταφοράς")]
        public List<TransferOrderLine> transferOrderLine { get; set; } = new List<TransferOrderLine>();
    }
}
