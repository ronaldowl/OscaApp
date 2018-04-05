using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using X.PagedList;

namespace OscaApp.ViewModels
{
    public class ServicoOrdemViewModel
    {
        public ServicoOrdem servicoOrdem { get; set; }
        public Relacao servico { get; set; }
        public Relacao ordemServico { get; set; }
        public ContextPage contexto { get; set; }
        public IPagedList<Servico> servicos { get; set; }

        public string StatusMessage { get; set; }

        public ServicoOrdemViewModel()
        {
            this.ordemServico = new Relacao();
            this.servico = new Relacao();
            this.servicoOrdem = new ServicoOrdem();

        }
    }
}
