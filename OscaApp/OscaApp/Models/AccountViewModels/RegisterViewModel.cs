using OscaApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;


namespace OscaApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
       
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha {0} deve ter no minumo {2} e no máximo {1} characters de tamanho", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação devem ser iguais")]
        public string ConfirmPassword { get; set; }

        public string msgOrganizacao { get; set; }

        public  Organizacao organizacao { get; set; }
        public ContextPage contexto { get; set; }

        public string idOrganizacao { get; set; }
        public Boolean sucesso { get; set; }

    }
}
