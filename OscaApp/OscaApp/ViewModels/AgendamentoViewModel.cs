using OscaApp.Models;
using OscaApp.Data;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class AgendamentoViewModel
    {
        public Agendamento agendamento { get; set; }
        public Relacao cliente { get; set; }
        public Relacao servico { get; set; }    
        public Relacao os { get; set; }
        public Relacao pedido { get; set; }
        public Relacao atendimento { get; set; }
        public Relacao profissional { get; set; }
        public Relacao atividade { get; set; }


        public ItemHoraDia horaInicio { get; set; }
        public ItemHoraDia horaFim { get; set; }
        public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}
