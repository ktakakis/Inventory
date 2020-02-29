using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class MoneyTransferOrder : INetcoreMasterChild
    {
        public MoneyTransferOrder()
        {
            createdAt = DateTime.UtcNow;
            MoneyTransferOrderDate = DateTime.UtcNow;
            MoneyTransferOrderStatus = MoneyTransferOrderStatus.Draft;
            isIssued = false;
            isReceived = false;
        }

        [StringLength(38)] 
        [Display(Name = "Id Μεταφοράς")]
        public string MoneyTransferOrderId { get; set; }

        [StringLength(20)]
        [Display(Name = "Αρ. Μεταφοράς")]
        public string MoneyTransferOrderNumber { get; set; }

        [Required] 
        [Display(Name = "Ημερομηνία")]
        public DateTime MoneyTransferOrderDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        [StringLength(38)]
        [Display(Name = "Από Ταμείο")]
        public string CashRepositoryIdFrom { get; set; }

        [Display(Name = "Από Ταμείο")]
        public CashRepository CashRepositoryFrom { get; set; }

        [StringLength(38)]
        [Display(Name = "Στο Ταμείο")]
        public string CashRepositoryIdTo { get; set; }

        [Display(Name = "Στο Ταμείο")]
        public CashRepository CashRepositoryTo { get; set; }

        [Display(Name = "Ποσόν")]
        public decimal PaymentAmount { get; set; }
         
        [Display(Name = "Κατάσταση")]
        public MoneyTransferOrderStatus MoneyTransferOrderStatus { get; set; }

        [Display(Name = "Έχει Εκδοθεί")]
        public bool isIssued { get; set; }

        [Display(Name = "Έχει παραληφθεί")]
        public bool isReceived { get; set; }

    }
}
