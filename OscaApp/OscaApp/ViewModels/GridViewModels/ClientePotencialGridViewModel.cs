using OscaFramework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels.GridViewModels
{
    public class ClientePotencialGridViewModel
    {
        public ClientePotencial clientePotencial { get; set; }   
        public Organizacao org { get; set; }

    }
}
