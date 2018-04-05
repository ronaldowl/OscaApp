using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaApp.Data;
using OscaFramework.Models;
namespace OscaApp.ViewModels
{
    public class IncidenteViewModel
    {
        public ContextPage Contexto { get; set; }
        public Incidente Incidente { get; set; }

        public string StatusMessage { get; set; }
    } // end of class IncidenteViewModel
} // end of namespace OscaApp.ViewModels
