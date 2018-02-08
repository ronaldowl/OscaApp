using System;
using System.Collections.Generic;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace OscaFramework.Models
{
    [Table("Servico")]
    public class Servico : GenericEntity
    {
        public String codigo { get; set; }
        public String nomeServico { get; set; }
        public Guid idOrganizacao { get; set; }

        public Servico()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 6;
        }
    }
}
