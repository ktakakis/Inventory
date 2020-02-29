using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Employee : INetcoreBasic, IBaseAddress
    {
        [StringLength(38)]
        [Display(Name = "Id Συνεργάτη")]
        public string EmployeeId { get; set; }

        public Employee()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(50)]
        [Display(Name = "Όνομα Χρήστη")]
        [Required]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Επίθετο")]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Εμφανιζόμενο Όνομα")]
        public string DisplayName { get; set; }

        //IBaseAddress
        [Display(Name = "Διεύθυνση")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Πόλη")]
        [StringLength(30)]
        public string city { get; set; }

        [Display(Name = "Νομός")]
        [StringLength(30)]
        public string province { get; set; }

        [Display(Name = "Χώρα")]
        [StringLength(30)]
        public string country { get; set; }
        //IBaseAddress

        [Display(Name ="Προμήθεια")]
        public decimal? Commission { get; set; }

        [Display(Name = "Κινητό Τηλέφωνο")]
        public string mobilePhone { get; set; }

        [Display(Name = "Τηλέφωνο Εργασίας")]
        public string officePhone { get; set; }

        [Display(Name = "Φαξ")]
        public string fax { get; set; }

        [Display(Name = "Προσωπικό E-mail")]
        public string personalEmail { get; set; }

        [Display(Name = "Ενεργός")]
        public Boolean Active { get; set; }

        [Display(Name = "Αποδέκτης Πληρωμών")]
        public Boolean PaymentReceiver { get; set; }

        [Display(Name = "Προεπιλεγμένος Μεταφορέας")]
        public Boolean DefaultCarrier { get; set; }

        [Display(Name = "Παραγγελίες Πώλησης")]
        public List<SalesOrder> SalesOrder { get; set; } = new List<SalesOrder>();

        [Display(Name = "Αποστολές Προϊόντων")]
        public List<Shipment> Shipment { get; set; } = new List<Shipment>();

        [Display(Name = "Εισπράξεις")]
        public List<PaymentReceive> PaymentReceive { get; set; } = new List<PaymentReceive>();


    }
}
