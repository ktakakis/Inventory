using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.ManageViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Το {0} πρέπει να είναι τουλάχιστον {2} και με μέγιστο {1} χαρακτήρες.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Νέος Κωδικός")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Επιβεβαίωση νέου κωδικού πρόσβασης")]
        [Compare("NewPassword", ErrorMessage = "Ο νέος κωδικός πρόσβασης και ο κωδικός επιβεβαίωσης δεν ταιριάζουν.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
