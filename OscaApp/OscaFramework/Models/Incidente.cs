using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaFramework.Models
{
    [Table("Incidente")]
    public class Incidente : GenericEntity
    {
        // CTOR PADRÃO
        public Incidente()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 11;
        } // end of ctor padrão

        // PROPERTIES
        public string codigo { get; set; }
        
        public string titulo { get; set; }

        public string descricao { get; set; }
        
        public Guid idOrganizacao { get; set; }


    } // end of class Incidente


} // end of namespace OscaFramework.Models
