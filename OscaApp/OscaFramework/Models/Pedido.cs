﻿using System;
 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("pedido")]
    public class Pedido : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idListaPreco { get; set; }

        public string codigoPedido { get; set; }
        public string anotacao { get; set; }

        //[DataType(DataType.Currency)]
        //[Column(TypeName = "money(5,2)")]
        //[Column(TypeName = "decimal(5, 2)")]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]

        public decimal valorTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorFrete { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDesconto { get; set; }

        public decimal valorDescontoPercentual { get; set; }    
        
        public DateTime dataFechamento { get; set; }

        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
        public CustomEnum.codicaoPagamento condicaoPagamento { get; set; }
        public CustomEnum.tipoPagamento tipoPagamento { get; set; }
        public CustomEnum.metodoEntrega metodoEntrega { get; set; }
        public CustomEnumStatus.StatusPedido statusPedido { get; set; }

        [NotMapped]
        public List<ProdutoPedido> produtosPedido { get; set; }


        public Pedido()
        {

            this.status = CustomEnumStatus.Status.Ativo;
            this.statusPedido = CustomEnumStatus.StatusPedido.EmAndamento;
            this.entityType = 4;
            

        }

    }
}
