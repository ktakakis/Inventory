using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Branch: INetcoreBasic, IBaseAddress
    {
        public Branch()
        {
            this.createdAt = DateTime.UtcNow;
            this.isDefaultBranch = false;
        }

        [StringLength(38)]
        [Display(Name = "Id Υποκαταστήματος")]
        public string branchId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα υποκαταστήματος")]
        [Required]
        public string branchName { get; set; }

        [StringLength(50)]
        [Display(Name = "Περιγραφή υποκαταστήματος")]
        public string description { get; set; }

        [StringLength(50)]
        [Display(Name = "E-Mail")]
        public string email { get; set; }

        [StringLength(50)]
        [Display(Name = "Α.Φ.Μ.")]
        public string VATNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Δ.Ο.Υ.")]
        public string TaxOffice { get; set; }

        [Display(Name = "Είναι υποκατάστημα προεπιλογής;")]
        public bool isDefaultBranch { get; set; } = false;

        //IBaseAddress
        [Display(Name = "Διεύθυνση")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Τ.Κ.")] 
        [StringLength(50)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Τηλέφωνο")]
        public string OfficePhone { get; set; }

        [StringLength(50)]
        [Display(Name = "FAX")]
        public string Fax { get; set; }

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
    }
}
