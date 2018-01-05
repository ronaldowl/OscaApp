using System;
using OscaApp.framework.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaApp.Models
{
    [Table("pedido")]
    public class Pedido : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idListaPreco { get; set; }

        public string codigoPedido { get; set; }
        public int quantidade { get; set; }

        public decimal valorTotal { get; set; }
        public decimal valorFrete { get; set; }
        public decimal valorDescontoMoney { get; set; }
        public decimal valorDescontoPercentual { get; set; }       

        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
        public CustomEnum.codicaoPagamento condicaoPagamento { get; set; }
        public CustomEnum.tipoPagamento tipoPagamento { get; set; }
        public CustomEnum.metodoEntrega metodoEntrega { get; set; }
        public CustomEnumStatus.StatusPedido statusPedido { get; set; }

        [NotMapped]
        public List<ProdutoPedido> produtosPedido { get; set; }

    }
}
