using OscaApp.Data;
using OscaFramework.Models;
using System;

namespace OscaApp.ViewModels
{
    public class ServicoPedidoRetiradaViewModel
    {
        public ServicoPedidoRetirada servicoPedidoRetirada { get; set; }
        public ContextPage contexto { get; set; }

        public String StatusMessage { get; set; }
    }
}
