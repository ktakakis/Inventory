using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class INetcoreBasic
    {
        [Display(Name = "Ημ. Δημιουργίας")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
