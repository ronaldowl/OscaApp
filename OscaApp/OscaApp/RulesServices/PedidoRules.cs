using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class PedidoRules
    {
        public static bool PedidoCreate(PedidoViewModel entrada,out Pedido modelo, ContextPage contexto )
        {
            modelo = new Pedido ();
            modelo = entrada.pedido;                               
                     
            if (modelo.idCliente  != null)
            {
                //Gera código do Pedido
                SqlGeneric sqlservice = new SqlGeneric();

                //Associa Cliente
                modelo.idCliente = entrada.cliente.id;

                modelo.codigoPedido = sqlservice.RetornaNovaPosicao(4, contexto.idOrganizacao);                              

                //************ Objetos de controle de acesso ******************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool PedidoUpdate(PedidoViewModel entrada,out Pedido modelo)
        {
            modelo = new Pedido();
            modelo = entrada.pedido;
            modelo.id = entrada.pedido.id;

            if (entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.Cancelado || entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.Fechado)
            {
                modelo.dataFechamento = DateTime.Now;
            }

            //************ Objetos de controle de acesso *******************
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }

        public static bool PedidoUpdateStatus(PedidoViewModel entrada, out Pedido modelo)
        {
            modelo = new Pedido();
            modelo = entrada.pedido;
            modelo.id = entrada.pedido.id;
           

            if(entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.Cancelado || entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.Fechado)
            {
                modelo.dataFechamento = DateTime.Now;
            }

            if (entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.EmAndamento || entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.AguardandoProduto)
            {
                modelo.dataFechamento = new DateTime ();
            }

            if (entrada.pedido.statusPedido == CustomEnumStatus.StatusPedido.ParaEntrega)
            {
                modelo.dataFechamento = new DateTime();
            }

            //************ Objetos de controle de acesso *******************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }

        public static void CalculoPedido(ref Pedido pedido, IProdutoPedidoData  ProdutosPedido)
        {
            List<ProdutoPedido> produtos = new List<ProdutoPedido>();

            produtos = ProdutosPedido.GetByPedidoId(pedido.id);

            decimal Total = 0;
         
            foreach (var item in produtos)
            {
                Total += item.totalGeral;
            }

            decimal totalPercentual = 0;

            if (pedido.tipoDesconto == CustomEnum.tipoDesconto.Money)
            {
                Total = Total - pedido.valorDesconto;                 
            }
            else
            {
                totalPercentual = (Total / 100) * pedido.valorDescontoPercentual;                
            }

            pedido.valorTotal = Total - totalPercentual;
            pedido.valorTotal += pedido.valorFrete;

           
        }
              
    }
}
