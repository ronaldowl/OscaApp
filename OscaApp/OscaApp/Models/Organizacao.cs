using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class Organizacao : GenericEntity
    {
        [Required]
        [StringLength(30, ErrorMessage = "O {0} nome da empresa deve ter no minimo  {2} e no máximo {1} characters de tamanho", MinimumLength = 3)]
        public string nomeOrganizao { get; set; }
        public string cpf_cnpj { get; set; }
        public DateTime dataPagamento { get; set; }
        public CustomEnum.TipoPessoa tipoPessoa { get; set; }
        public CustomEnum.Status statusOrganizacao { get; set; }
        public CustomEnum.Plano plano { get; set; }
        public int quantidadeUsuario { get; set; }
        public decimal totalPagar { get; set; }
        public decimal valorPorUsuario { get; set; }        
        public string nomeAdministrador { get; set; }
        public string emailAdministrador { get; set; }
        public string telefoneAdministrador { get; set; }
        public string celularAdministrador { get; set; }
        public string logradouro { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string pais { get; set; }
        public string cep { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string anotacao { get; set; }
        public string nomeAmigavel { get; set; }


    }
}
