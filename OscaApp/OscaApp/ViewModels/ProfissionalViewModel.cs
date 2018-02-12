using OscaFramework.Models;

using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;

namespace OscaApp.ViewModels
{
    public class ProfissionalViewModel : ViewModelBase
    {
        public Profissional profissional { get; set; }
        public ContextPage contexto { get; set; }
        public Relacao banco { get; set; }
        public Relacao usuario { get; set; }

        public ProfissionalViewModel()
        {

            this.banco = new Relacao();

        }
    }

}
