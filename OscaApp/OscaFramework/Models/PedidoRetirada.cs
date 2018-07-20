using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("PedidoRetirada")]
    public class PedidoRetirada : GenericEntity
    {
        public String codigo { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idProfissional { get; set; }

        public decimal valorTotal { get; set; }
        public string anotacao { get; set; }
        public DateTime dataFechamento { get; set; }

        public CustomEnum.tipoPagamento tipoPagamento { get; set; }
        public CustomEnum.codicaoPagamento condicaoPagamento { get; set; }
        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
        public CustomEnumStatus.StatusPedidoRetirada statusPedidoRetirada { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDesconto { get; set; }




        public PedidoRetirada()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 33;
        }
    }
}
