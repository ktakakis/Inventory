using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Το {0} πρέπει να είναι τουλάχιστον {2} και με μέγιστο {1} χαρακτήρες.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Κωδικός πρόσβασης")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Επιβεβαίωση κωδικού πρόσβασης")]
        [Compare("Password", ErrorMessage = "Ο κωδικός πρόσβασης και ο κωδικός επιβεβαίωσης δεν ταιριάζουν.")]
        public string ConfirmPassword { get; set; }
    }
}
