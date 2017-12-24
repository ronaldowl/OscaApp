using System;
using OscaApp.framework.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaApp.Models
{
    [Table("Endereco")]
    public class Endereco : GenericEntity
    {
      
        //Propriedades locais
        public String logradouro { get; set; }
        public String cep { get; set; }
        public String cidade { get; set; }
        public String bairro { get; set; }

        public String numero { get; set; }
        public String complemento { get; set; }
        public String anotacao { get; set; }
      
        public Guid idCliente { get; set; }
        public string idClienteName { get; set; }
         
        public CustomEnum.TipoEndereco TipoEndereco {get; set;}
        public CustomEntityEnum.Estado estado { get; set; }
        public Guid idOrganizacao { get; set; }

        public Endereco()
        {
            this.status = CustomEnum.Status.Ativo;
        }
    }
}
