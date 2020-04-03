using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Customer : INetcoreMasterChild
    {
        public Customer()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Πελάτη")]
        public string customerId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα πελάτη")]
        [Required]
        public string customerName { get; set; }

        [StringLength(50)]
        [Display(Name = "Δραστηριότητα")]
        [Required]
        public string CompanyActivity { get; set; }

        [StringLength(50)]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }

        [StringLength(50)]
        [Display(Name = "Α.Φ.Μ.")]
        public string VATRegNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Εφορία")]
        public string TaxOffice { get; set; }

        [Display(Name = "Μέγεθος επιχείρησης")]
        public BusinessSize size { get; set; }

        [Display(Name = "Τηλέφωνο γραφείου")]
        [StringLength(20)]
        public string officePhone { get; set; }

        [Display(Name = "Κινητό τηλέφωνο")]
        [StringLength(20)]
        public string mobilePhone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20)]
        public string fax { get; set; }

        [Display(Name = "Email εργασίας")]
        [StringLength(50)]
        public string workEmail { get; set; }

        [Display(Name = "Ιστοσελίδα")]
        [StringLength(50)]
        public string WebSite { get; set; }

        [Display(Name = "Εκπτωση Τιμολογίου")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal? OrderDiscount { get; set; }

        [Display(Name = "Εκπτωση Καλής Συνεργασίας")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal TaxDiscount { get; set; }

        [Display(Name = "Ενεργός")]
        public Boolean Active { get; set; }

        [Display(Name = "Μετράνε οι πωλήσεις")]
        public Boolean SalesCount { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Συνεργάτη")]
        public string EmployeeId { get; set; }
         
        [Display(Name = "Συνεργάτης")]
        public Employee Employee { get; set; }

        [Display(Name = "Διευθύνσεις πελάτη")]
        public List<CustomerLine> CustomerLine { get; set; } = new List<CustomerLine>();

        [Display(Name = "Κατάλογοι πελάτη")]
        public List<Catalog> Catalog { get; set; } = new List<Catalog>();

    }
}
