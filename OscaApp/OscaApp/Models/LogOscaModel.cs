using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Models
{
    [Table("LogOsca")]
    public class LogOscaModel : GenericEntity
    {
        public string Id { get; set; }
        public string Evento { get; set; }

        public string Mensagem { get; set; }

        public int CodigoEntidade { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdOrganizacao { get; set; }
        public DateTime DataCriacao { get; set; }
    
        public LogOscaModel()
        {
            this.entityType = 15;
        }
    }
}
