using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("OrgConfig")]
    public class OrgConfig : GenericEntity
    {
        public String nome { get; set; }
        public Guid idOrganizacao { get; set; }

        public OrgConfig()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 34;
        }
    }
}
