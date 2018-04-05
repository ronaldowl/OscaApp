using System;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class ComunicadoViewModel
    {
        public Comunicado comunicado { get; set; }
        public ContextPage Contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}

