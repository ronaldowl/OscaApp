using System;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;
using System.Collections.Generic;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.ViewModels
{
    public class FornecedorViewModel
    {
        public Fornecedor Fornecedor { get; set; }
        public ContextPage Contexto { get; set; }
        public List<ProdutoFornecedorGridViewModel> produtoFornecedor { get; set; }
        public string StatusMessage { get; set; }
    }
}

