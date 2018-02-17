using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OscaFramework.Models
{
    // Tabela Universal - não herda Generic
    [Table("Banco")]
    public class Banco
    {
        public Guid id { get; set; }
        public String codigo { get; set; }
        public String nome { get; set; }
        public String site { get; set; }
        [NotMapped]
        public CustomEnumStatus.Status status { get; set; }
        [NotMapped]
        public CustomEntityEnum.Entidade entityType { get; set; }

        public Banco()
        {
            this.status = CustomEnumStatus.Status.Ativo;
            this.entityType = CustomEntityEnum.Entidade.Banco;
        }
    }
}
