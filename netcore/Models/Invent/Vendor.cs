using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Vendor : INetcoreMasterChild, IBaseAddress, IBaseCommunication
    {
        public Vendor()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id προμηθευτή")]
        public string vendorId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα προμηθευτή")]
        [Required]
        public string vendorName { get; set; }

        [StringLength(50)]
        [Display(Name = "Περιγραφή")]
        public string description { get; set; }
        
        [Display(Name = "Μέγεθος επιχείρησης")]
        public BusinessSize size { get; set; }

        [Display(Name = "Α.Φ.Μ.")]
        [StringLength(15)]
        public string TaxNumber { get; set; }

        [Display(Name = "Εφορία")]
        [StringLength(50)]
        public string TaxOffice { get; set; }
        [Display(Name = "Ιστοσελίδα")]
        [StringLength(100)]
        public string WebSite { get; set; }

        //IBaseAddress
        [Display(Name = "Διεύθυνση 1")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Ταχ. Κωδικός")]
        [StringLength(6)]
        public string PostCode { get; set; }

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

        //IBaseCommunication
        [Display(Name = "Κινητό Τηλέφωνο")]
        public string mobilePhone { get; set; }
        [Display(Name = "Τηλέφωνο Εργασίας")]
        public string officePhone { get; set; }
        [Display(Name = "Φαξ")]
        public string fax { get; set; }
        [Display(Name = "Προσωπικό E-mail")]
        public string personalEmail { get; set; }
        [Display(Name = "E-mail Εργασίας")]
        public string workEmail { get; set; }
        //IBaseCommunication

        [Display(Name = "Επαφές Προμηθευτή")]
        public List<VendorLine> vendorLine { get; set; } = new List<VendorLine>();
    }
}
