using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OscaFramework.Models
{
    [Table("Atendimento")]
    public class Atendimento : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public string codigo { get; set; }
        public DateTime dataAtendimento { get; set; }

        public Atendimento()
        {
            this.entityType = 3;
            this.status = CustomEnumStatus.Status.Ativo;
        } // ctor padrão
    }
}
