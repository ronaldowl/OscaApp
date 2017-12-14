using System;
using System.Collections.Generic;
using OscaApp.framework.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
 

namespace OscaApp.Models
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
        public CustomEnum.Status status { get; set; }


        public Cliente() {
            this.status = CustomEnum.Status.Ativo;
        }

    }
}
