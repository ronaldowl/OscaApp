using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ContextPage contexto          { get; set; }              
        public List<SelectListItem> listaPrecos   { get; set; }
        public Relacao produto               { get; set; }

        public ItemListaPrecoViewModel()
        {

            this.itemlistaPreco = new ItemListaPreco();
        }

    }
}
