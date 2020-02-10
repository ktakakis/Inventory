using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class PaymentReceive : INetcoreBasic
    {
        public PaymentReceive() 
        {
            this.createdAt = DateTime.UtcNow;
            this.PaymentDate = DateTime.UtcNow.Date;
        }
        [StringLength(38)]
        [Display(Name = "Id Τύπου Είσπραξης")]
        public string PaymentReceiveId { get; set; }

        [Display(Name = "Αριθμός Είσπραξης")]
        public string PaymentReceiveName { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Τιμολογίου")]
        public string InvoiceId { get; set; }

        [Display(Name = "Τιμολόγιο")]
        public Invoice invoice { get; set; }
        
        [Display(Name = "Ημερομηνία")]
        public DateTime PaymentDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Τρόπου Είσπραξης")]
        public string PaymentTypeId { get; set; }

        [Display(Name = "Τρόπος Είσπραξης")]
        public PaymentType paymentType { get; set; }

        [Display(Name = "Ποσόν")]
        public decimal PaymentAmount { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Συνεργάτη")]
        public string EmployeeId { get; set; }

        [Display(Name = "Συνεργάτης")]
        public Employee Employee { get; set; }

        [Display(Name = "Πλήρης Είσπραξη")]
        public bool IsFullPayment { get; set; } = true;
    }
}
