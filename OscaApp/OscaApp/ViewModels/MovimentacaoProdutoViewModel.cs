using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class MovimentacaoProdutoViewModel
    {
       public MovimentacaoProduto movimentacaoProduto { get; set; }
       public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}
