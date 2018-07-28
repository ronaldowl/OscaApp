using System;
 
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("DetalheMovimentacaoProduto")]
    public class DetalheMovimentacaoProduto : GenericEntity
    {
        public String nome { get; set; }
        public Guid idOrganizacao { get; set; }

        public DetalheMovimentacaoProduto()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 38;
        }
    }
}
