
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
        }

        [Display(Name = "Αριθμός Τιμολογίου")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Id Αποστολής")]
        public string shipmentId { get; set; } 

        [Display(Name = "Αποστολή")]
        public Shipment Shipment { get; set; }

        [Display(Name = "Ημερομηνία Έκδοσης")]
        public DateTimeOffset InvoiceDate { get; set; }

    }
}
