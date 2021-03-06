﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    [Table("Atividade")]
    public class Atividade : GenericEntity
    {
        public string descricao { get; set; }
        public int tipo { get; set; }
        public DateTime dataAlvo { get; set; }
        public DateTime dataFechamento { get; set; }

        public string assunto { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idProfissional { get; set; }
        public CustomEnumStatus.StatusAtividade statusAtividade { get; set; }

        public Atividade()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 27;
        }
    } 
}
