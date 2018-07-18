using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("ServicoPedidoRetirada")]
    public class ServicoPedidoRetirada : GenericEntity
    {
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        public Guid idOrganizacao { get; set; }

        public ServicoPedidoRetirada()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 20;
        }
    }
}
