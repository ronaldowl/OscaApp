using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("ContasPagar")]
    public class ContasPagar : GenericEntity
    {
        public String codigo { get; set; }
        public String titulo { get; set; }
        public Guid idOrganizacao { get; set; }

        public ContasPagar()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 20;
        }
    }
}
