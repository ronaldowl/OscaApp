
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using System.Collections.Generic;

namespace OscaApp.ViewModels
{
    public class PedidoViewModel
    {
        public Pedido pedido { get; set; }

        public ContextPage contexto { get; set; }

        public Relacao cliente { get; set; }

        public List<SelectListItem> listasPreco { get; set; }

        public List<ProdutoPedido> produtosPedido { get; set; }
    }
}
