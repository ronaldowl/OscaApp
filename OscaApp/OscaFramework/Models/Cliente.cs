using System;
using System.Collections.Generic;
 

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace OscaFramework.Models
{
    [Table("cliente")]
    public class Cliente : GenericEntity
    {       

        //Propriedades locais
        public String codigo { get; set; }
        public String cnpj_cpf { get; set; } 
        public CustomEnum.TipoPessoa tipoPessoa { get; set; }        
        public String nomeCliente { get; set; }
        public String razaoSocial { get; set; }
        public String email { get; set; }
        public String telefone { get; set; }       
        public String celular { get; set; }    
        public String anotacao { get; set; }
        public CustomEnum.Sexo sexo { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idContato { get; set; }


        public Cliente()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 1;
        }

    }
}
