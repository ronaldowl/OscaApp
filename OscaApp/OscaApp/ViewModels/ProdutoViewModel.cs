using OscaApp.Data;
using OscaApp.Models;
using System.Collections.Generic;
using OscaFramework.Models;

namespace OscaApp.ViewModels
{
    public class ProdutoViewModel
    {
        public Produto produto { get; set; }
        public ContextPage contexto { get; set; }
        public List<ItemProdutoLista> itensListaPreco { get; set; }

        public string StatusMessage { get; set; }

        public ProdutoViewModel()
        {
            this.produto = new  Produto();

        }
    }
}
