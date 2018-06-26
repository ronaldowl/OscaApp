using System;
 
using System.ComponentModel.DataAnnotations.Schema;
using static OscaFramework.Models.CustomEnum;

namespace OscaFramework.Models
{
    
    public class Faturamento : GenericEntity
    {
        public decimal valor { get; set; } 
        public Guid idOrganizacao { get; set; }
        public Guid idReferencia { get; set; }
        public DateTime dataFaturamento { get; set; }
        public OrigemFaturamento origemFaturamento { get; set; }


        public Faturamento()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 30;
        }
    }
}
