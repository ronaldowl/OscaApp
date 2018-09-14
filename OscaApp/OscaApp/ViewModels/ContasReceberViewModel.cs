using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class ContasReceberViewModel
    {
       public ContasReceber contasReceber { get; set; }
       public Relacao cliente { get; set; }
       public ContextPage contexto { get; set; }
       public Relacao referencia { get; set; }
       public string StatusMessage { get; set; }
    }
}
