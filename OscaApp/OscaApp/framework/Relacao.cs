using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.framework.Models
{
    public class Relacao
    {
        public Guid id { get; set; }
        public String idName { get; set; }
        public String organizacao { get; set; }
        public Guid idOrganizacao { get; set; }
        public CustomEntityEnum.Entidade tipoObjeto { get; set; }       


        public Relacao(Guid _id, CustomEntityEnum.Entidade entityType)
        {
            this.id = _id;
            this.tipoObjeto = entityType;
        }
        public Relacao(Guid _id, CustomEntityEnum.Entidade entityType, string _idName)
        {
            this.idName = _idName;
            this.id = _id;
            this.tipoObjeto = entityType;
        }
        public Relacao()
        {
        }
    }

   
}
