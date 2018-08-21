using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using OscaFramework.Models;

namespace OscaApp.ViewModels.GridViewModels
{
    public class ProdutoFornecedorGridViewModel
    {
        public  ProdutoFornecedor produtoFornecedor { get; set; }
        public  Produto produto { get; set; }
        public  Fornecedor fornecedor { get; set; }

    }
}
