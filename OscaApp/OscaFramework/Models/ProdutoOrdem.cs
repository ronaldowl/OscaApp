using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    [Table("ProdutoOrdem")]
    public class ProdutoOrdem : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idOrdemServico { get; set; }        
        public Guid idProduto { get; set; }
        public Guid idListaPreco { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDesconto { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDescontoMoney { get; set; }

       [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal total { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal totalGeral { get; set; }

        public int valorDescontoPercentual { get; set; }

        public int quantidade { get; set; }
        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
      

        // Ctors
        public ProdutoOrdem()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType =    18;
        } // end of ctor padrão

    }
}
