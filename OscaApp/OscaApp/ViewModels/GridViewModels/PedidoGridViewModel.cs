
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.ViewModels.GridViewModels
{
    public class PedidoGridViewModel
    {
        public Pedido pedido { get; set; }      
        public Cliente cliente { get; set; }
    }
}
