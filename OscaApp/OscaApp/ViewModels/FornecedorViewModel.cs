using System;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;


namespace OscaApp.ViewModels
{
    public class FornecedorViewModel
    {
        public Fornecedor fornecedor { get; set; }
        public ContextPage contexto { get; set; }

        public String nomeFornecedor { get; set; }
        public String cnpj { get; set; }
        public String nomeVendedor { get; set; }
        public String telefone { get; set; }
        public String email { get; set; }
        public String anotacao { get; set; }
    }
}

