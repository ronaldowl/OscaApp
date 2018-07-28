using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class DetalheMovimentacaoProdutoViewModel
    {
       public DetalheMovimentacaoProduto detalheMovimentacaoProduto { get; set; }
       public ContextPage contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}
