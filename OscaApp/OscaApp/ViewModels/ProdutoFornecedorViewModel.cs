using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProdutoFornecedorViewModel
    {
        public ProdutoFornecedor produtoFornecedor { get; set; }
        public ContextPage contexto          { get; set; }             
        public Relacao produto               { get; set; }
        public Relacao fornecedor            { get; set; }


        public string StatusMessage { get; set; }

        public ProdutoFornecedorViewModel()
        {
            this.produto = new Relacao();
            this.fornecedor = new Relacao();
            this.produtoFornecedor = new ProdutoFornecedor();
        }

    }
}
