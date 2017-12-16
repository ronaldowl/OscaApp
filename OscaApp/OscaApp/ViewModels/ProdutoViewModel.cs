using OscaApp.Data;
using OscaApp.Models;

namespace OscaApp.ViewModels
{
    public class ProdutoViewModel
    {
        public Produto produto { get; set; }
        public ContextPage contexto { get; set; }

        public ProdutoViewModel()
        {
            this.produto = new Models.Produto();

        }
    }
}
