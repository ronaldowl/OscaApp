using OscaFramework.Models;
using System;
using System.Collections.Generic;
using OscaApp.framework.Models;
using OscaApp.Data;
using OscaApp.Models;
 


namespace OscaApp.ViewModels
{
    public class ClienteViewModel : ViewModelBase
    {
        public Cliente cliente { get; set; }                
        public List<Endereco> enderecos { get; set; }
        public List<Pedido> pedidos { get; set; }
        public List<OrdemServico> ordensServico { get; set; }
        public List<Atendimento> atendimentos { get; set; }
        public List<ContasReceber> contasReceber { get; set; }

        public ContextPage contexto { get; set; }
        public Relacao contato { get; set;}         
        public string StatusMessage { get; set; }
     
        public ClienteViewModel()
        {
            this.cliente = new  Cliente();
            this.contato = new Relacao();
        }
    }
}
