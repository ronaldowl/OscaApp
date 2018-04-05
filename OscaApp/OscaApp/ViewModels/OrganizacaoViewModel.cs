using OscaFramework.Models;
using OscaApp.Data;
using OscaApp.Models;

namespace OscaApp.ViewModels
{
    public class OrganizacaoViewModel
    {
        public Organizacao organizacao { get; set; }
        public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }

        public OrganizacaoViewModel()
        {
            this.organizacao = new  Organizacao();
        }
    }

   
}
