using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    public class ProdutoPedido : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idPedido { get; set; }
        public Guid idItemListaPreco { get; set; }
        public Guid idProduto { get; set; }

        public decimal valor { get; set; }
        public decimal valorDescontoMoney { get; set; }
        public int valorDescontoPercentual { get; set; }
        public int quantidade { get; set; }
        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
        
    }
}
