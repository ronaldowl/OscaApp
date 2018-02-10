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
       public List<Relacao> servicos { get; set; }
       public List<Relacao> produtos { get; set; }
       public List<Relacao> profissionais { get; set; }

    }
}
