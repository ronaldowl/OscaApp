
using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;

namespace OscaApp.ViewModels
{
    public class ClienteViewModel : ViewModelBase
    {
        public Cliente cliente { get; set; }                
        public List<Endereco> enderecos { get; set; }
        public ContextPage contexto { get; set; }

        public ClienteViewModel()
        {
            this.cliente = new Models.Cliente();
        }
    }
}
