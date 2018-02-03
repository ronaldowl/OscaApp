using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaApp.Models
{
    [Table("Profissional")]
    public class Profissional : GenericEntity
    {
    
        public String codigo { get; set; }
        public String nomeProfissional { get; set; }
        public String numeroConta { get; set; }
        public String agencia { get; set; }
        public Boolean comissionado { get; set; }
        public int percentualComissao { get; set; }
        public Guid idOrganizacao { get; set; }

        public CustomEnum.tipoConta tipoConta { get; set; }

        public Profissional()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 17;
        }
    }
}