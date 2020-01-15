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
        public string NumberSequenceName { get; set; }
        [Required]
        public string Module { get; set; }
        [Required]
        public string Prefix { get; set; }
        public int LastNumber { get; set; }
        public int MyProperty { get; set; }
    }
}
