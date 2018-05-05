using OscaFramework.Models;
using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;
 


namespace OscaApp.ViewModels
{
    public class PaginaClienteViewModel : ViewModelBase
    {
        public ClientePotencial clientePotencial { get; set; }              
                 

        public string StatusMessageLead { get; set; }
        public string urlPaginaCaptura { get; set; }


        public PaginaClienteViewModel()
        {
            this.clientePotencial = new ClientePotencial();
           
        }
    }
}
