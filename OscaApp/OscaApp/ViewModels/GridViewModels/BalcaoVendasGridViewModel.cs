using OscaFramework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels.GridViewModels
{
    public class BalcaoVendasGridViewModel
    {
        public BalcaoVendas balcaoVendas { get; set; }  
        public Cliente cliente { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }

    }
}
