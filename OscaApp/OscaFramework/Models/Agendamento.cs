using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using static OscaFramework.Models.CustomEnum;
using static OscaFramework.Models.CustomEnumStatus;

namespace OscaFramework.Models
{
    [Table("Agendamento")]
    public class Agendamento : GenericEntity
    {
        public Guid idOrganizacao { get; set; }
        public Guid idCliente { get; set; }
        public Guid idReferencia { get; set; }
        public Guid idProfissional { get; set; }    


        public string codigo { get; set; }        
        

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataAgendada { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dataFechamento { get; set; }

        public int horaInicio { get; set; }
        public int horaFim { get; set; }


        public StatusAgendamento statusAgendamento { get; set; }
        public TipoReferencia tipoReferencia { get; set; }   

        public Agendamento()
        {
            this.entityType = 3;
            this.status = CustomEnumStatus.Status.Ativo;
            this.statusAgendamento = StatusAgendamento.agendado;
        } // ctor padrão
    }
}
