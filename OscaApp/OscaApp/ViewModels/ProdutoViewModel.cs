using OscaApp.Data;
using OscaApp.Models;
using System.Collections.Generic;
using OscaFramework.Models;
using Microsoft.AspNetCore.Http;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.ViewModels
{
    public class ProdutoViewModel
    {
        public Produto produto { get; set; }
        public ContextPage contexto { get; set; }
        public List<ItemProdutoLista> itensListaPreco { get; set; }
        public List<ProdutoFornecedorGridViewModel> produtoFornecedor { get; set; }

        public string StatusMessage { get; set; }

        public ProdutoViewModel()
        {
            this.produto = new  Produto();

        }
    }
}
