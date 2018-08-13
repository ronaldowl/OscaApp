using OscaApp.Data;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class PagamentoViewModel
    {
        public Pagamento pagamento { get; set; }
        public ContextPage contexto { get; set; }      
        public Relacao contasReceber { get; set; }
        public string StatusMessage { get; set; }
    }
}
