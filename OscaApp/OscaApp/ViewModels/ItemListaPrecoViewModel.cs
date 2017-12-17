using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.ViewModels
{
    public class ItemListaPrecoViewModel
    {
        public ItemListaPreco itemlistaPreco { get; set; }
        public ContextPage contexto { get; set; }              
        public Relacao listaPreco { get; set; }
        public Relacao produto { get; set; }

    }
}
