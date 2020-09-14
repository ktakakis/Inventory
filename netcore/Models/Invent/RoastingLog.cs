using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class RoastingLog : INetcoreMasterChild
    {
        public RoastingLog()
        {
            this.createdAt = DateTime.UtcNow;
            this.RoastingDate = DateTime.Today;
            this.StartTime = DateTime.UtcNow;
            this.EndTime = StartTime.AddMinutes(12);
        }
        [StringLength(38)]
        [Display(Name = "Id Ψησίματος")]
        public string RoastingLogId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα Ψησίματος")]
        [Required]
        public string RoasterName { get; set; }

        [StringLength(38)]
        [Required]
        [Display(Name = "Επίπεδο ψησίματος")]
        public string RoastingLevelId { get; set; }

        [Display(Name = "Επίπεδο ψησίματος")]
        public RoastingLevel RoastingLevel { get; set; }

        [Display(Name = "Ημερομηνία")]
        public DateTime RoastingDate { get; set; }

        [Display(Name = "Ώρα Έναρξης")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Ώρα Λήξης")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Αρχικό Βάρος(γρ)")]
        public int StartWeight { get; set; }

        [Display(Name = "Τελικό Βάρος(γρ)")]
        public int FinishWeight { get; set; }

        [Display(Name = "Ποσοστό φύρας %")]
        public decimal LossPercent { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string ProductId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product Product { get; set; }

        [Display(Name = "Θερμ. έναρξης")]
        public decimal StartTemp { get; set; }

        [Display(Name = "Θερμ. Περιβάλλοντος")]
        public decimal AmbientTemp { get; set; }

        [Display(Name = "Θερμ. Γεμίσματος")]
        public decimal AfterFillingTemp { get; set; }

        [Display(Name = "Ώρα 1ου κρακ")]
        public DateTime FirstCrackTime { get; set; }

        [Display(Name = "Ώρα 2ου κρακ")]
        public DateTime? SecondCrackTime { get; set; }

    }
}
