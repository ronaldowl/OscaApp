using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ProdutoPedidoRules
    {
        public static bool MontaProdutoPedidoCreate(ProdutoPedidoViewModel entrada, out ProdutoPedido modelo, ContextPage contexto)
        {
            modelo = new ProdutoPedido();         
            modelo = entrada.produtoPedido;
            modelo.idProduto = entrada.produto.id;

            if (modelo.idProduto != null)
            {               

                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                //************* Executa calculo ************
                ProdutoPedidoRules.CalculaProdutoPedido(ref modelo);

                return true;
            }

            return false;
        }
        public static bool MontaProdutoPedidoUpdate(ProdutoPedidoViewModel entrada, out ProdutoPedido modelo)
        {
            modelo = new ProdutoPedido();         
          

            ////************ Objetos de controle de acesso ***************
            modelo = entrada.produtoPedido;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************

            //************* Executa calculo ************
            ProdutoPedidoRules.CalculaProdutoPedido(ref modelo);

            return true;
        }

        public static  void   CalculaProdutoPedido(ref ProdutoPedido modelo)
        {
                      
            if (modelo.tipoDesconto == CustomEnum.tipoDesconto.Money)
            {
                modelo.total = modelo.valor * modelo.quantidade;
                modelo.valorDesconto = modelo.valorDescontoMoney;
                modelo.totalGeral = modelo.total - modelo.valorDesconto;
            }
            else
            {
                modelo.total = modelo.valor * modelo.quantidade;
                modelo.valorDesconto = (modelo.total / 100) * modelo.valorDescontoPercentual;
                modelo.totalGeral = modelo.total - modelo.valorDesconto;
            }          
        }
    }
}
