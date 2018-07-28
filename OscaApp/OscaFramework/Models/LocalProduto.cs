using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("LocalProduto")]
    public class LocalProduto : GenericEntity
    {
        public String nome { get; set; }
        public Guid idOrganizacao { get; set; }

        public LocalProduto()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 36;
        }
    }
}
