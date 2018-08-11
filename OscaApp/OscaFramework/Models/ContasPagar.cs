using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("ContasPagar")]
    public class ContasPagar : GenericEntity
    {
        public String codigo { get; set; }
        public String titulo { get; set; }
        public Guid idOrganizacao { get; set; }
        public string anotacao { get; set; }
        public string numeroReferencia { get; set; }        
        public DateTime dataPagamento { get; set; }
        public DateTime dataFechamento { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        public CustomEnum.OrigemContaPagar origemContaPagar { get; set; }
        public CustomEnum.TipoLancamento tipoLancamento { get; set; }
        public CustomEnumStatus.StatusContaPagar statusContaPagar { get; set; }


        public ContasPagar()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 20;
        }
    }
}
