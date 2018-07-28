using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("MovimentacaoProduto")]
    public class MovimentacaoProduto : GenericEntity
    {
        public String nome { get; set; }
        public Guid idOrganizacao { get; set; }

        public MovimentacaoProduto()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 37;
        }
    }
}
