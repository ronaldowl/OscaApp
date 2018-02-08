using System;
using System.Collections.Generic;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("Fornecedor")]
    public class Fornecedor : GenericEntity
    {
    
        public String nomeFornecedor { get; set; }
        public String cnpj { get; set; }
        public String nomeVendedor { get; set; }
        public String telefone { get; set; }
        public String email { get; set; }
        public String anotacao { get; set; }
        public Guid idOrganizacao { get; set; }


        public Fornecedor()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 14;
        }
    }
}
