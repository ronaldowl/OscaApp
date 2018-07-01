using OscaFramework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using System.Collections.Generic;

namespace OscaApp.ViewModels
{
    public class BalcaoVendasViewModel
    {
        public BalcaoVendas balcaoVendas { get; set; }

        public ContextPage contexto { get; set; }
        
        public Relacao listapreco { get; set; }

        public List<SelectListItem> listaPrecos { get; set; }

        public List<ProdutoPedido> produtosPedido { get; set; }

        public string StatusMessage { get; set; }
  
        public BalcaoVendasViewModel()
        {

            this.balcaoVendas = new BalcaoVendas();
            this.listaPrecos = new List<SelectListItem>();

        }
    }
}
