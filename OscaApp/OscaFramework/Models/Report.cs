using System;
 
using System.ComponentModel.DataAnnotations.Schema;
using static OscaFramework.Models.CustomEnum;

namespace OscaFramework.Models
{
    
    public class Report  
    {
        public string nomeAmigavel { get; set; }
        public string nomeRDL { get; set; }
        public Guid id { get; set; }
        public Guid idOrganizacao { get; set; }         
        public TipoReport tipoReport { get; set; }
        public CustomEnumStatus.Status status { get; set; }
        public CustomEntityEnum.Entidade entityType { get; set; }

        public Report()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType =  CustomEntityEnum.Entidade.Report;
        }
    }
}
