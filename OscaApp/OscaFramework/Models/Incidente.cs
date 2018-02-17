using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    public class Incidente : GenericEntity
    {
        public Incidente()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 11;
        } // end of ctor padrão
    } // end of class Incidente


} // end of namespace OscaFramework.Models
