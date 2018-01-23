using System;
using OscaApp.framework.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaApp.Models
{
    [Table("ContasReceber")]
    public class ContasReceber : GenericEntity
    {
        public String codigo { get; set; }
        public String titulo { get; set; }
        public Guid idOrganizacao { get; set; }

        public ContasReceber()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 21;
        }
    }
}
