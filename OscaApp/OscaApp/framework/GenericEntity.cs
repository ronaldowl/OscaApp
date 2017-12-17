using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.framework.Models
{
   public abstract class   GenericEntity
    {
      

        [Key]
        public Guid id { get; set; }

        [NotMapped]
        public int entityType { get; set; }
        public Guid criadoPor { get; set; }
        public string criadoPorName { get; set; }
        public DateTime criadoEm { get; set; }
        public Guid modificadoPor { get; set; }
        public string modificadoPorName { get; set; }
        public DateTime modificadoEm { get; set; }
        public Guid idOrganizacao { get; set; }
        public CustomEnum.Status status { get; set; }


        public GenericEntity(int _entityType, Guid _id)
        {
            this.entityType = _entityType;
            this.id = _id;          

        }
        public GenericEntity()
        {            

        }

    }
}
