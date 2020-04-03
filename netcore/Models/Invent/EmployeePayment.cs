using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class EmployeePayment : INetcoreBasic
    {
        public EmployeePayment()
        {
            this.createdAt = DateTime.UtcNow;
            this.PaymentDate = DateTime.UtcNow;
        }
        [StringLength(38)]
        [Display(Name = "Τύπος Είσπραξης")]
        public string EmployeePaymentId { get; set; }

        [Display(Name = "Αριθμός")]
        public string PaymentNumber { get; set; }

        [Display(Name = "Ημερομηνία")]
        public DateTime PaymentDate { get; set; }

        [StringLength(38)]
        [Display(Name = "Τρόπος Πληρωμής")]
        public string PaymentTypeId { get; set; }

        [Display(Name = "Τρόπος Πληρωμής")]
        public PaymentType paymentType { get; set; }

        [Display(Name = "Ποσόν")]
        public decimal PaymentAmount { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Συνεργάτη")]
        public string EmployeeId { get; set; }

        [Display(Name = "Συνεργάτης")]
        public Employee Employee { get; set; }

        [StringLength(38)]
        [Display(Name = "Ταμείο")]
        public string CashRepositoryId { get; set; }

        [Display(Name = "Ταμείο")]
        public CashRepository CashRepository { get; set; }


    }
}
