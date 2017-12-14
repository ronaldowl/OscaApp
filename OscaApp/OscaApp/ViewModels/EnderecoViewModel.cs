using System;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaApp.Data;

namespace OscaApp.ViewModels
{
    public class EnderecoViewModel : GenericEntity
    {      
        public Endereco endereco { get; set; }   
           
        public ContextPage contexto { get; set; }

        public EnderecoViewModel()
        {
            this.endereco = new Models.Endereco();

        }
    }
}
