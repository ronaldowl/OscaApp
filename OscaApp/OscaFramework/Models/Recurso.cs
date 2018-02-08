using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("Recurso")]
    public class Recurso : GenericEntity
    {
        public String codigo { get; set; }
        public String nome { get; set; }
        public Guid idOrganizacao { get; set; }

        public Recurso()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 20;
        }
    }
}
