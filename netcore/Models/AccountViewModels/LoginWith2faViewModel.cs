using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "Το {0} πρέπει να είναι τουλάχιστον {2} και με μέγιστο {1} χαρακτήρες.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Κωδικός ελέγχου ταυτότητας")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Θυμηθείτε αυτό το μηχάνημα")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
