
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("Pagamento")]
    public class Pagamento : GenericEntity
    {
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }
        
        public CustomEnum.tipoPagamento tipoPagamento { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataPagamento { get; set; }

        public Guid idOrganizacao { get; set; }
        public Guid idContasReceber { get; set; }        

        public Pagamento()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 40;
     
        }

    }
}
