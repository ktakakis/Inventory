using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class PaymentType : INetcoreBasic
    {
        [StringLength(38)]
        [Display(Name = "Id Τρόπου Είσπραξης")]
        public string PaymentTypeId { get; set; }

        [Required]
        [Display(Name = "Τρόπος Είσπραξης")]
        public string PaymentTypeName { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        [Display(Name = "Εισπράξεις")]
        public List<PaymentReceive> PaymentReceive { get; set; } = new List<PaymentReceive>();

    }
}
