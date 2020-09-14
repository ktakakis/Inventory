using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class RoastingLevel : INetcoreBasic
    {
        public RoastingLevel()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Επίπεδο Ψησίματος")]
        public string RoastingLevelId { get; set; }

        [StringLength(50)]
        [Display(Name = "Επίπεδο Ψησίματος")]
        [Required]
        public string RoastingLevelName { get; set; }

        [Display(Name = "Περιγραφή")]
        public string Description { get; set; }

        [Display(Name = "Εικόνα")]
        public byte[] File { get; set; }

    }
}
