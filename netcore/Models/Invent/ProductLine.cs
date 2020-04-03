using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ProductLine : INetcoreBasic
    {
        public ProductLine()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id Συστατικού")]
        public string ProductLineId { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Προϊόντος")]
        public string ProductId { get; set; }

        [Display(Name = "Προϊόν")]
        public Product Product { get; set; }

        [StringLength(38)]
        [Display(Name = "1η ύλη")]
        [ForeignKey("Component")]
        public string ComponentId { get; set; }

        [Display(Name = "1η ύλη")]
        public Product Component { get; set; }


        [Display(Name = "Ποσοστό")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal Percentage { get; set; }

    }
}
