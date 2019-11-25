using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Receiving : INetcoreMasterChild
    {
        public Receiving()
        {
            this.createdAt = DateTime.UtcNow;
            this.receivingNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#GSRN";
            this.receivingDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Παραλαβής")]
        public string receivingId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Παραγγελίας Αγοράς")]
        public string purchaseOrderId { get; set; }

        [Display(Name = "Παραγγελία Αγοράς")]
        public PurchaseOrder purchaseOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός GSRN")]
        public string receivingNumber { get; set; }
        
        [Required]
        [Display(Name = "Ημερομηνία GSRN")]
        public DateTime receivingDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προμηθευτή")]
        public string vendorId { get; set; }

        [Display(Name = "Προμηθευτής")]
        public Vendor vendor { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Αρ. Παραλαβής Προμηθευτή")]
        public string vendorDO { get; set; }

        [StringLength(50)]
        [Display(Name = "Αριθμός τιμολογίου προμηθευτή")]
        public string vendorInvoice { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Υποκαταστήματος")]
        public string branchId { get; set; }

        [Display(Name = "Υποκατάστημα")]
        public Branch branch { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Αποθήκης")]
        public string warehouseId { get; set; }

        [Display(Name = "Αποθήκη")]
        public Warehouse warehouse { get; set; }

        [Display(Name = "Στοιχεία Παραλαβής")]
        public List<ReceivingLine> receivingLine { get; set; } = new List<ReceivingLine>();
    }
}
