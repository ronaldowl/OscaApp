using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class ImpressaoPedidoRetiradaViewModel
    {
       public PedidoRetirada pedidoRetirada { get; set; }
        public Organizacao organizacao { get; set; }
       public Cliente cliente { get; set; }
       public OrgConfig orgConfig { get; set; }
       public ContextPage contexto { get; set; }

       
    }
}
