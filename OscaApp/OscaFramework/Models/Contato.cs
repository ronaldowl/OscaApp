using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{

    [Table("contato")]
    public class Contato : GenericEntity
    {

        //Propriedades locais

        public Cliente cliente { get; set; }
        public String nome { get; set; }
        public String telefone { get; set; }
        public String celular { get; set; }
        public String email { get; set; }
        public String cpf { get; set; }
        public String numero { get; set; }
        public String logradouro { get; set; }
        public String cep { get; set; }
        public String cidade { get; set; }
        public String bairro { get; set; }
        public String complemento { get; set; }
        public CustomEnum.Sexo sexo { get; set; }
        public Guid idOrganizacao { get; set; }

        public CustomEntityEnum.Estado estado { get; set; }

        public Contato()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 2;
        }
    }

}
