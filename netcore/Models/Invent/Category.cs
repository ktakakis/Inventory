using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Category
    {

        public Category()
        {
            Categories = new List<Category>();
        }

        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Κατηγορία")]
        public string CategoryName { get; set; }

        [Display(Name = "Γονέας")]
        public int? ParentCategoryID { get; set; }
        
        [Display(Name = "Γονέας")]
        public Category ParentCategory { get; set; }
        
        [Display(Name = "Κατηγορίες")]
        public ICollection<Category> Categories { get; set; }

    }
}
