using OscaApp.Data;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;


namespace OscaApp.ViewModels
{
    public class ListaPrecoViewModel
    {
        public ListaPreco listaPreco { get; set; }
        public ContextPage contexto { get; set; }

        public String nome { get; set; }
        public DateTime dataValidade { get; set; }

    }
}
