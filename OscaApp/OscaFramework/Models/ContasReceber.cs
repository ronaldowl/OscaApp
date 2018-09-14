using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("ContasReceber")]
    public class ContasReceber : GenericEntity
    {
        public String codigo { get; set; }
        public String titulo { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idReference { get; set; }

        public string anotacao { get; set; }
        public string numeroReferencia { get; set; }
        public DateTime dataPagamento { get; set; }
        public DateTime dataRecebimento { get; set; }
        

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valorPago { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valorRestante { get; set; }

        public CustomEnum.OrigemContaReceber origemContaReceber { get; set; }
        public CustomEnum.TipoLancamento tipoLancamento { get; set; }
        public CustomEnumStatus.StatusContaReceber statusContaReceber { get; set; }



        public ContasReceber()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 21;
        }
    }
}
