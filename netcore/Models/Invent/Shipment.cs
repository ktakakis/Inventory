using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Shipment : INetcoreMasterChild
    {
        public Shipment()
        {
            this.createdAt = DateTime.UtcNow;
            //this.shipmentNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#DO";
            this.shipmentDate = DateTime.UtcNow;
            this.expeditionType = ExpeditionType.Internal;
            this.expeditionMode = ExpeditionMode.Land;
        }

        [StringLength(38)]
        [Display(Name = "Id Αποστολής")]
        public string shipmentId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Παραγγελίας Πώλησης")]
        public string salesOrderId { get; set; }

        [Display(Name = "Παραγγελία Πώλησης")]
        public SalesOrder salesOrder { get; set; }

        [StringLength(20)]        
        [Display(Name = "Αριθμός Αποστολής")]
        public string shipmentNumber { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία Αποστολής")]
        public DateTime shipmentDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Πελάτη")]
        public string customerId { get; set; }

        [Display(Name = "Πελάτης")]
        public Customer customer { get; set; }

        [StringLength(50)]
        [Display(Name = "Παραγγελία Αγοράς Πελάτη")]
        public string customerPO { get; set; }

        [StringLength(50)]
        [Display(Name = "Αριθμός Τιμολογίου")]
        public string invoiceNumber { get; set; }

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

        [StringLength(38)]
        [Display(Name = "Μεταφορέας")]
        public string EmployeeId { get; set; }

        [Display(Name = "Μεταφορέας")]
        public Employee Employee { get; set; }



        [Display(Name = "Τύπος αποστολής")]
        public ExpeditionType expeditionType { get; set; }

        [Display(Name = "Τρόπος Αποστολής")]
        public ExpeditionMode expeditionMode { get; set; }

        [Display(Name = "Στοιχεία Αποστολής")]
        public List<ShipmentLine> shipmentLine { get; set; } = new List<ShipmentLine>();

        [Display(Name = "Παραστατικά")]
        public List<Invoice> invoice { get; set; } = new List<Invoice>();
    }
}
