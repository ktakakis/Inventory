
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Invoice : INetcoreMasterChild
    {
        [StringLength(38)]
        [Display(Name = "Id Τιμολογίου")]
        public string InvoiceId { get; set; }
        public Invoice() 
        {
            this.createdAt = DateTime.UtcNow;
            this.InvoiceDate = DateTime.UtcNow.Date;
        }

        [Display(Name = "Αριθμός Τιμολογίου")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Id Αποστολής")]
        public string shipmentId { get; set; } 

        [Display(Name = "Αποστολή")]
        public Shipment Shipment { get; set; }

        [Display(Name = "Ημερομηνία Έκδοσης")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Οριστικοποιημένο")]
        public Boolean Finalized { get; set; }

        [Display(Name = "Εξοφλημένο")]
        public Boolean Paid { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα υποκαταστήματος")]
        public string branchName { get; set; }

        [StringLength(50)]
        [Display(Name = "Περιγραφή υποκαταστήματος")]
        public string description { get; set; }

        [Display(Name = "Διεύθυνση")]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Τ.Κ.")]
        [StringLength(50)]
        public string PostalCode { get; set; }

        [Display(Name = "Πόλη")]
        [StringLength(30)]
        public string city { get; set; }

        [StringLength(50)]
        [Display(Name = "Τηλέφωνο")]
        public string OfficePhone { get; set; }

        [StringLength(50)]
        [Display(Name = "FAX")]
        public string Fax { get; set; }

        [StringLength(50)]
        [Display(Name = "E-Mail")]
        public string email { get; set; }

        [StringLength(50)]
        [Display(Name = "Α.Φ.Μ.")]
        public string VATNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Δ.Ο.Υ.")]
        public string TaxOffice { get; set; }

        [Display(Name = "Συνεργάτης")]
        [StringLength(30)]
        public string EmployeeName { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα πελάτη")]
        public string customerName { get; set; }

        [StringLength(50)]
        [Display(Name = "Δραστηριότητα")]
        public string CustomerCompanyActivity { get; set; }

        [Display(Name = "Διεύθυνση")]
        [StringLength(50)]
        public string CustomerStreet { get; set; }

        [Display(Name = "Ταχ. Κωδικός")]
        [StringLength(5)]
        public string CustomerPostCode { get; set; }

        [Display(Name = "Πόλη")]
        [StringLength(30)]
        public string CustomerCity { get; set; }

        [Display(Name = "Χώρα")]
        [StringLength(30)]
        public string CustomerCountry { get; set; }

        [StringLength(50)]
        [Display(Name = "Α.Φ.Μ.")]
        public string CustomerVATRegNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Εφορία")]
        public string CustomerTaxOffice { get; set; }

        [Display(Name = "Σύνολο Προ Έκπτωσης")]
        public decimal TotalBeforeDiscount { get; set; }

        [Display(Name = "Συνολική έκπτωση")]
        public decimal totalDiscountAmount { get; set; }

        [Display(Name = "Σύνολο Φ.Π.Α.")]
        public decimal TotalProductVAT { get; set; }

        [Display(Name = "Τελική Αξία")]
        public decimal totalOrderAmount { get; set; }

        [StringLength(100)]
        [Display(Name = "Περιγραφή")]
        public string Comments { get; set; }

        [Display(Name = "Τηλέφωνο γραφείου")]
        [StringLength(20)]
        public string CustomerOfficePhone { get; set; }

        [Display(Name = "Κινητό τηλέφωνο")]
        [StringLength(20)]
        public string CustomerMobilePhone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20)] 
        public string CustomerFax { get; set; }

        [Display(Name = "Email εργασίας")]
        [StringLength(50)]
        public string CustomerWorkEmail { get; set; }

        [Display(Name = "Στοιχεία Τιμολογίου")]
        public List<InvoiceLine> InvoiceLine { get; set; } = new List<InvoiceLine>();

        [Display(Name = "Εισπράξεις Τιμολογίου")]
        public List<PaymentReceive> PaymentReceive { get; set; } = new List<PaymentReceive>();
    }
}
