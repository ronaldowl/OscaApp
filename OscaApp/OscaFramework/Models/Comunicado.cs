using System;
using System.Collections.Generic;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("Comunicado")]
    public class Comunicado : GenericEntity
    {
    
        public String titulo { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public String mensagem { get; set; }
        public Guid idOrganizacao { get; set; }


        public Comunicado()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 19;
        }
    }
}
