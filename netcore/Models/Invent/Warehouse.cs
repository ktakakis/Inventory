using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Invent
{
    public class Warehouse: INetcoreBasic, IBaseAddress
    {
        public Warehouse()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Id αποθήκης")]
        public string warehouseId { get; set; }

        [StringLength(50)]
        [Display(Name = "Όνομα αποθήκης")]
        [Required]
        public string warehouseName { get; set; }

        [StringLength(50)]
        [Display(Name = "Περιγραφή αποθήκης")]
        public string description { get; set; }

        [StringLength(38)]
        [Display(Name = "Id Υποκαταστήματος")]
        public string branchId { get; set; }
        
        [Display(Name = "Υποκατάστημα")]
        public Branch branch { get; set; }

        //IBaseAddress
        [Display(Name = "Διεύθυνση 1")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Διεύθυνση 2")]
        [StringLength(50)]
        public string street2 { get; set; }

        [Display(Name = "Πόλη")]
        [StringLength(30)]
        public string city { get; set; }

        [Display(Name = "Νομός")]
        [StringLength(30)]
        public string province { get; set; }

        [Display(Name = "Χώρα")]
        [StringLength(30)]
        public string country { get; set; }
        //IBaseAddress
    }
}
