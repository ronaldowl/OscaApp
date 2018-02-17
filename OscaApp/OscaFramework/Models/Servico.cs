using System;
using System.Collections.Generic;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace OscaFramework.Models
{
    [Table("Servico")]
    public class Servico : GenericEntity
    {
        public String codigo { get; set; }
        public String nomeServico { get; set; }
        public String descricao { get; set; }
        public Guid idOrganizacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        public CustomEnum.FormaVendaProduto formaVendaServico { get; set; }

        public Servico()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 6;
        }
    }
}
