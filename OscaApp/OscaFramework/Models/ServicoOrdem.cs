 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    [Table("servicoOrdem")]
    public class ServicoOrdem : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idOrdemServico { get; set; }        
        public Guid idServico { get; set; }
       

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDesconto { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valorDescontoMoney { get; set; }

       [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal total { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal totalGeral { get; set; }

        public int valorDescontoPercentual { get; set; }

        public int hora { get; set; }
        public CustomEnum.tipoDesconto tipoDesconto { get; set; }
        
    }
}
