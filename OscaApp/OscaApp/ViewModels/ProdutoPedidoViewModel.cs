using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.ViewModels
{
    public class ProdutoPedidoViewModel
    {
        public ProdutoPedido produtoPedido { get; set; }
        public Relacao produto { get; set; }
        public ContextPage contexto { get; set; }
    }
}
