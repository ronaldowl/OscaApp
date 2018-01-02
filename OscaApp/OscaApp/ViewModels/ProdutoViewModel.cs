using OscaApp.Data;
using OscaApp.Models;
using System.Collections.Generic;

namespace OscaApp.ViewModels
{
    public class ProdutoViewModel
    {
        public Produto produto { get; set; }
        public ContextPage contexto { get; set; }
        public List<ItemProdutoLista> itensListaPreco { get; set; }

        public ProdutoViewModel()
        {
            this.produto = new Models.Produto();

        }
    }
}
