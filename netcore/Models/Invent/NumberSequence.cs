using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent 
{
    public class NumberSequence: INetcoreBasic
    {
        public NumberSequence()
        {
            this.createdAt = DateTime.UtcNow;
        }
        public int NumberSequenceId { get; set; }
        [Required]
        [Display(Name ="Ονομασία")]
        public string NumberSequenceName { get; set; }
        [Required]
        [Display(Name = "Μονάδα μέτρησης")]
        public string Module { get; set; }
        [Required]
        [Display(Name = "Πρόθεμα")]
        public string Prefix { get; set; }
        [Display(Name = "Τελευταίος Αριθμός")]
        public int LastNumber { get; set; }
        
        public int MyProperty { get; set; }
    }
}
