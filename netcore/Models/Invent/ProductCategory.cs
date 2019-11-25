using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class ProductCategory: INetcoreBasic
    {
        public ProductCategory()
        {
            this.createdAt = DateTime.UtcNow;
        }
        [StringLength(38)]
        [Display(Name = "Id Κατηγορίας")]
        public string productcategoryId { get; set; }
        [StringLength(50)]
        [Display(Name = "Ονομασία κατηγορίας")]
        [Required]
        public string categoryName { get; set; }


    }
}
