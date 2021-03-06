﻿ 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
    [Table("ListaPreco")]
    public class ListaPreco : GenericEntity
    {
        public string nome { get; set; }
        public string descricao { get; set; }
        public bool padrao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataValidade { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fimValidade { get; set; }

        public Guid idOrganizacao { get; set; }

        public ListaPreco()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = 12;
            this.padrao = false;
        }

    }
}
