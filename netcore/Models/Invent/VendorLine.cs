using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class VendorLine : INetcoreBasic, IBasePerson, IBaseCommunication
    {
        public VendorLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Γραμμής")]
        public string vendorLineId { get; set; }

        [StringLength(20)]
        [Display(Name = "Τίτλος εργασίας")]
        [Required]
        public string jobTitle { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προμηθευτή")]
        public string vendorId { get; set; }
        
        [Display(Name = "Προμηθευτής")]
        public Vendor vendor { get; set; }

        //IBasePerson
        [Display(Name = "Όνομα")]
        [Required]
        [StringLength(20)]
        public string firstName { get; set; }

        [Display(Name = "Επίθετο")]
        [Required]
        [StringLength(20)]
        public string lastName { get; set; }

        [Display(Name = "Μεσαίο Όνομα")]
        [StringLength(20)]
        public string middleName { get; set; }

        [Display(Name = "Υποκοριστικό")]
        [StringLength(20)]
        public string nickName { get; set; }

        [Display(Name = "Γένος")]
        public Gender gender { get; set; }

        [Display(Name = "Χαιρετισμός")]
        public Salutation salutation { get; set; }
        //IBasePerson

        //IBaseCommunication
        [Display(Name = "Κινητό τηλέφωνο")]
        [StringLength(20)]
        public string mobilePhone { get; set; }

        [Display(Name = "Τηλέφωνο γραφείου")]
        [StringLength(20)]
        public string officePhone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20)]
        public string fax { get; set; }

        [Display(Name = "Προσωπικό Email")]
        [StringLength(50)]
        public string personalEmail { get; set; }

        [Display(Name = "Email εργασίας")]
        [StringLength(50)]
        public string workEmail { get; set; }
        //IBaseCommunication
    }
}
