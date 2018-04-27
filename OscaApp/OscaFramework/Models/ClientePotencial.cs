using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("clientePotencial")]
    public class ClientePotencial : GenericEntity
    {          
         
        public String nomeCliente { get; set;}     
        public String email { get; set;}
        public String telefone { get; set;}       
        public String celular { get; set;}    
        public String anotacao { get; set;}
        public CustomEnum.Sexo sexo { get; set; }
        public CustomEnumStatus.StatusLead statusLead { get; set; }
        public Guid idOrganizacao { get; set; }    
        
        public ClientePotencial()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 28;
        }

    }
}
