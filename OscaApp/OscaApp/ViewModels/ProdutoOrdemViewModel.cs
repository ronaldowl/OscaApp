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
    public class ProdutoOrdemViewModel
    {
        public ProdutoOrdem produtoOrdem { get; set; }
        public Relacao produto { get; set; }
        public Relacao ordemServico { get; set; }
        public Relacao listaPreco { get; set; }
        public ContextPage contexto { get; set; }

        public ProdutoOrdemViewModel()
        {
            this.ordemServico = new Relacao();
            this.produto = new Relacao();
            this.produtoOrdem = new ProdutoOrdem();
            this.listaPreco = new Relacao();

        }
    }
}
