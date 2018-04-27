using OscaFramework.Models;
using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;
 


namespace OscaApp.ViewModels
{
    public class ClientePotencialViewModel : ViewModelBase
    {
        public ClientePotencial clientePotencial { get; set; }               
    
        public ContextPage contexto { get; set; }
        
        public string StatusMessage { get; set; }
     
        public ClientePotencialViewModel()
        {
            this.clientePotencial = new ClientePotencial();
           
        }
    }
}
