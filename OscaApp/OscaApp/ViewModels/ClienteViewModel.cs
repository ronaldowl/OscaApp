﻿using OscaFramework.Models;
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
        public Relacao contato { get; set;
        }
        public ClienteViewModel()
        {
            this.cliente = new  Cliente();
            this.contato = new Relacao();
        }
    }
}
