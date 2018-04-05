using OscaFramework.Models;
using System;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;


namespace OscaApp.ViewModels
{
    public class ContatoViewModel
    {
        public Contato contato { get; set; }
        public ContextPage contexto { get; set; }
        public string StatusMessage { get; set; }
    }
}
