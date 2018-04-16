using OscaApp.Models;
using OscaApp.Data;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class AtendimentoViewModel
    {
        public  Atendimento atendimento { get; set; }
        public  Relacao cliente { get; set; }
        public  Relacao servico { get; set; }     
        public  Relacao profissional { get; set; }

        public ItemHoraDia horaInicio { get; set; }
        public ItemHoraDia horaFim { get; set; }
        public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}
