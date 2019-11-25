using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class CustomerLine : INetcoreBasic
    {
        public CustomerLine()
        {
            this.createdAt = DateTime.UtcNow;
        }
        [StringLength(38)]
        [Display(Name = "Id Στοιχείου Πελάτη")]
        public string customerLineId { get; set; }
        //IBaseAddress
        [Display(Name = "Διεύθυνση")]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Πόλη")]
        [StringLength(30)]
        public string city { get; set; }

        [Display(Name = "Ταχ. Κωδικός")]
        [StringLength(5)]
        public string PostCode { get; set; }

        [Display(Name = "Νομός")]
        [StringLength(30)]
        public string province { get; set; }

        [Display(Name = "Χώρα")]
        [StringLength(30)]
        public string country { get; set; }

        [Display(Name = "Γεωγραφικό πλάτος")]
        [StringLength(30)]
        public string LocLat { get; set; }

        [Display(Name = "Γεωγραφικό μήκος")]
        [StringLength(30)]
        public string LocLong { get; set; }
        //IBaseAddress

        [StringLength(38)]
        [Display(Name = "Id Πελάτη")]
        public string customerId { get; set; }

        [Display(Name = "Πελάτης")]
        public Customer customer { get; set; }

        [Display(Name = "Ποσοστό Φ.Π.Α.")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public string ProductVAT { get; set; }

    }
}
