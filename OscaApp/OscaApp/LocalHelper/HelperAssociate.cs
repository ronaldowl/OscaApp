using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels.GridViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.framework
{
    public static class HelperAssociate
    {
        public static List<LookupItemLista> ConvertToLookupItemLista(List<ItemListaPreco> itens)
        {
            List<LookupItemLista> retorno = new List<LookupItemLista>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                LookupItemLista X = new LookupItemLista();
                X.produto = sqldata.RetornaProduto(item.idProduto);
                X.itemlistaPreco = item;

                retorno.Add(X);
            }
            return retorno;
        }

        public static List<PedidoGridViewModel> ConvertToGridPedido(List<Pedido> itens)
        {
            List<PedidoGridViewModel> retorno = new List<PedidoGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                PedidoGridViewModel X = new PedidoGridViewModel();
                X.cliente = sqldata.RetornaCliente(item.idCliente);
                X.pedido = item;

                retorno.Add(X);
            }
            return retorno;
        }

        public static List<ProdutoPedidoGridViewModel> ConvertToGridProdutoPedido(List<ProdutoPedido> itens)
        {
            List<ProdutoPedidoGridViewModel> retorno = new List<ProdutoPedidoGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                ProdutoPedidoGridViewModel X = new ProdutoPedidoGridViewModel();
                X.produto = sqldata.RetornaProduto(item.idProduto);
                X.produtoPedido = item;
                retorno.Add(X);
            }
            return retorno;
        }

    }
}
