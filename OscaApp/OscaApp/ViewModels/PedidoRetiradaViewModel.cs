using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class PedidoRetiradaViewModel
    {
       public PedidoRetirada pedidoRetirada { get; set; }
       public Relacao cliente { get; set; }
       public Relacao profissional { get; set; }     
        
       public ContextPage contexto { get; set; }
       public string StatusMessage { get; set; }

        public PedidoRetiradaViewModel()
        {
            this.pedidoRetirada = new PedidoRetirada();
        
        }

    }
}
