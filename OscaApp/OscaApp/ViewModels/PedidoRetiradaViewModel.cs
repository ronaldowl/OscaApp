using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class PedidoRetiradaViewModel
    {
       public PedidoRetirada pedidoRetirada { get; set; }
       public Relacao cliente { get; set; }
       public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}
