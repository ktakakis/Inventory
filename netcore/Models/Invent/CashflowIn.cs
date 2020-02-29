using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class CashflowIn : INetcoreMasterChild
    {
        public CashflowIn()
        {
            createdAt = DateTime.UtcNow;
            CashflowInNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#IN";
            CashflowInDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Εισροής")]
        public string CashflowInId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Παραγγελία Εισροής")]
        public string MoneyTransferOrderId { get; set; }

        [Display(Name = "Εντολή μεταφοράς")]
        public MoneyTransferOrder MoneyTransferOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός")]
        public string CashflowInNumber { get; set; }

        [Required]
        [Display(Name = "Ημερομηνία")]
        public DateTime CashflowInDate { get; set; }

        [StringLength(100)]
        [Required]
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

    }
}
