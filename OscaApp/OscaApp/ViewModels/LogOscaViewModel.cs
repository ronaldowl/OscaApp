using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;

namespace OscaApp.ViewModels
{
    public class LogOscaViewModel
    {
        public LogOscaModel logOsca { get; set; }
        public ContextPage Contexto { get; set; }

        public Guid IdUsuario { get; set; }
        public string Evento { get; set; }
        public int CodigoEntidade { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
