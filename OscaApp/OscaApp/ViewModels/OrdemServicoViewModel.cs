using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaFramework.Models;
using System.Collections.Generic;

namespace OscaApp.ViewModels
{
    public class OrdemServicoViewModel
    {
       public OrdemServico ordemServico { get; set; }
       public ContextPage contexto { get; set; }
       public Relacao cliente { get; set; }
       public Relacao profissional { get; set; }
   

       public List<Relacao> servicos { get; set; }
       public List<Relacao> produtos { get; set; }
       public List<Relacao> profissionais { get; set; }
       public Relacao listaPreco { get; set; }
       public List<SelectListItem> listasPreco { get; set; }
        public Relacao categoria { get; set; }
        public List<SelectListItem> categorias { get; set; }

        public string StatusMessage { get; set; }
        
        public OrdemServicoViewModel()
        {
            this.ordemServico = new OrdemServico();
            this.ordemServico.statusOrdemServico = CustomEnumStatus.StatusOrdemServico.EmAndamento;
             
        }

    }
}
