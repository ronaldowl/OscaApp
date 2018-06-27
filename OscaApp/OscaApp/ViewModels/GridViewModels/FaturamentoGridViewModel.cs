using OscaFramework.Models;


namespace OscaApp.ViewModels.GridViewModels
{
    public class FaturamentoGridViewModel
    {
        public Faturamento faturamento { get; set; }
        public Cliente cliente { get; set; }
        public int qtd { get; set; }
        public decimal total {get;set;}
    }
}
