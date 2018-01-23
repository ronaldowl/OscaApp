﻿using System;
using OscaApp.framework.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OscaApp.Models
{
    [Table("OrdemServico")]
    public class OrdemServico : GenericEntity
    {
        public String codigo { get; set; }
        public DateTime dataEntrada { get; set; }
        public Guid idOrganizacao { get; set; }

        public OrdemServico()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 5;
        }
    }
}
