using OscaApp.Data;
using OscaFramework.Models;
namespace OscaApp.ViewModels
{
    public class PerfilAcessoViewModel
    {
        public ContextPage Contexto { get; set; }
        public PerfilAcesso perfilAcesso { get; set; }

        public string StatusMessage { get; set; }
    } // end of class IncidenteViewModel
} // end of namespace OscaApp.ViewModels
