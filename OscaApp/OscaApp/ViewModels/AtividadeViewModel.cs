using OscaApp.Data;
using OscaApp.Models;
using System.Collections.Generic;
using OscaFramework.Models;

namespace OscaApp.ViewModels
{
    public class AtividadeViewModel
    {
        public Atividade atividade{ get; set; }
        public Relacao profissional { get; set; }
        public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }

        public AtividadeViewModel()
        {
            this.atividade = new  Atividade();
        }
    }
}
