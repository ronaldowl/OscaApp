using System;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaApp.Data;
using OscaFramework.Models;

namespace OscaApp.ViewModels
{
    public class EnderecoViewModel : GenericEntity
    {      
        public Endereco endereco { get; set; }   
           
        public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }

        public EnderecoViewModel()
        {
            this.endereco = new  Endereco();

        }
    }
}
