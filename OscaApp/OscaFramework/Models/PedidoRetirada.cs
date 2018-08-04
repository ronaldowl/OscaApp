using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OscaFramework.Models.CustomEnum;

namespace OscaFramework.Models
{
    [Table("PedidoRetirada")]
    public class PedidoRetirada : GenericEntity
    {
        public String codigo { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idProfissional { get; set; }
       
        public string anotacao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime dataFechamento { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime dataEntrega { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime dataRetirada { get; set; }

        public CustomEnum.tipoPagamento tipoPagamento { get; set; }
        public CustomEnum.codicaoPagamento condicaoPagamento { get; set; }
       
        public CustomEnumStatus.StatusPedidoRetirada statusPedidoRetirada { get; set; }
        public CustomEnum.Periodo periodo { get; set; }

        public int quantidade1 { get; set; }
        public int quantidade2 { get; set; }

        public decimal valor1 { get; set; }
        public decimal valor2 { get; set; }

        public decimal valorTotal1 { get; set; }
        public decimal valorTotal2 { get; set; }
     
        public ModeloCacamba modeloCacamba1 { get; set; }
        public ModeloCacamba modeloCacamba2 { get; set; }

        ////public Guid idendereco1 { get; set; }
        ////public Guid idendereco2 { get; set; }

        public int quantidade { get; set; }    
            
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valorTotal { get; set; }

        public PedidoRetirada()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 33;
        }
    }
}
