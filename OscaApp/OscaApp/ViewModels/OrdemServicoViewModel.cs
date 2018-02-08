using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaFramework.Models;

namespace OscaApp.ViewModels
{
    public class OrdemServicoViewModel
    {
       public OrdemServico ordemServico { get; set; }
       public ContextPage contexto { get; set; }
       public Relacao cliente { get; set; }
       public Relacao servico { get; set; }
       public Relacao profissional { get; set; }

    }
}
