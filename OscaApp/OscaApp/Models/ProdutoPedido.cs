﻿using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    [Table("produtopedido")]
    public class ProdutoPedido : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idPedido { get; set; }
        public Guid idItemListaPreco { get; set; }
        public Guid idProduto { get; set; }

        [NotMapped]
        public Guid idListaPreco { get; set; }
                  
        public decimal valor { get; set; }

        //[DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public decimal valorDesconto { get; set; }

        //[DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public decimal valorDescontoMoney { get; set; }

        //[DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public decimal total { get; set; }

        //[DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        public decimal totalGeral { get; set; }

        public int valorDescontoPercentual { get; set; }

        public int quantidade { get; set; }
        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
        
    }
}
