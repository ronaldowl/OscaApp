using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static OscaFramework.Models.CustomEnum;
using static OscaFramework.Models.CustomEnumStatus;

namespace OscaFramework.Models
{
    [Table("Atendimento")]
    public class Atendimento : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idProfissional { get; set; }
        public Guid idServico { get; set; }


        public string codigo { get; set; }
        public string titulo { get; set; }
        public string problema { get; set; }
        public string diagnostico { get; set; }      
        public string observacao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataAgendada { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataFechamento { get; set; }       
            
        public StatusAtendimento statusAtendimento { get; set; }
     
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valor { get; set; }

        public Atendimento()
        {
            this.entityType = 3;
            this.status = CustomEnumStatus.Status.Ativo;
            this.statusAtendimento = StatusAtendimento.agendado;
        } // ctor padrão
    }
}
