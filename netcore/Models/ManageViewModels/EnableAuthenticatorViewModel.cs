using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace netcore.Models.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
            [Required]
            [StringLength(7, ErrorMessage = "Το {0} πρέπει να είναι τουλάχιστον {2} και με μέγιστο {1} χαρακτήρες.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Κωδικός επαλήθευσης")]
            public string Code { get; set; }

            [BindNever]
            public string SharedKey { get; set; }

            [BindNever]
            public string AuthenticatorUri { get; set; }
    }
}
