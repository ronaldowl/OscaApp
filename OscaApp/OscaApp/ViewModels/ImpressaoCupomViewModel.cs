using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;
using System.Collections.Generic;

namespace OscaApp.ViewModels
{
    public class ImpressaoCupomViewModel
    {
       public BalcaoVendas balcaoVendas { get; set; }
       public Produto produto { get; set; }
       public Organizacao organizacao { get; set; }
       public Cliente cliente { get; set; }
       public OrgConfig orgConfig { get; set; }
       public List<ProdutoBalcao> produtosBalcao { get; set; }
       public ContextPage contexto { get; set; }

       
    }
}
