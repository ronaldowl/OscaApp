 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    [Table("ProdutoFornecedor")]
    public class ProdutoFornecedor : GenericEntity
    {
        public string codigoProdutoFornecedor { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorCompra { get; set; }

        public Guid idOrganizacao { get; set; }
        public Guid idProduto { get; set; }
        public Guid idFornecedor { get; set; }

        public ProdutoFornecedor()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 41;          
        }

    }
}
