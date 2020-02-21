using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class CashflowOut : INetcoreMasterChild
    {
        public CashflowOut()
        {
            this.createdAt = DateTime.UtcNow;
            this.CashflowOutNumber = DateTime.UtcNow.Date.ToString("yyyyMMdd") + Guid.NewGuid().ToString().Substring(0, 5).ToUpper() + "#OUT";
            this.CashflowOutDate = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Εκροής")]
        public string CashflowOutId { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Id Εντολής")]
        public string MoneyTransferOrderId { get; set; }

        [Display(Name = "Εντολή μεταφοράς χρημάτων")]
        public MoneyTransferOrder MoneyTransferOrder { get; set; }

        [StringLength(20)]
        [Required]
        [Display(Name = "Αριθμός Εκροής")]
        public string CashflowOutNumber { get; set; } 

        [Required]
        [Display(Name = "Ημερομηνία Εκροής")]
        public DateTime CashflowOutDate { get; set; }

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
