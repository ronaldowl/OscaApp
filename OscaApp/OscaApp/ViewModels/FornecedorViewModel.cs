using System;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class FornecedorViewModel
    {
        public Fornecedor Fornecedor { get; set; }
        public ContextPage Contexto { get; set; }

        public string StatusMessage { get; set; }
    }
}

