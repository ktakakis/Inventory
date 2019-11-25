using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public interface IBaseCommunication
    {
        [Display(Name = "Κινητό τηλέφωνο")]
        [StringLength(20)]
        string mobilePhone { get; set; }

        [Display(Name = "Τηλέφωνο γραφείου")]
        [StringLength(20)]
        string officePhone { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20)]
        string fax { get; set; }

        [Display(Name = "Προσωπικό Email")]
        [StringLength(50)]
        string personalEmail { get; set; }

        [Display(Name = "Email εργασίας")]
        [StringLength(50)]
        string workEmail { get; set; }

    }
}
