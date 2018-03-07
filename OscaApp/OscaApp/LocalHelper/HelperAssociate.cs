using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels.GridViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.framework
{
    /// <summary>
    /// Class especializada em retornar itens para Grid e Lookp que apresentam dados de duas ou mais tabelas
    /// </summary>
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
                X.listapreco = sqldata.RetornaListaPreco(item.idListaPreco);
                X.itemlistaPreco = item;

                retorno.Add(X);
            }
            return retorno;
        }
        public static List<OrdemServicoGridViewModel> ConvertToGridOrdemServico(List<OrdemServico> itens)
        {
            List<OrdemServicoGridViewModel> retorno = new List<OrdemServicoGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                OrdemServicoGridViewModel X = new OrdemServicoGridViewModel();
                X.cliente = sqldata.RetornaRelacaoCliente(item.idCliente);
                X.ordemServico = item;
                X.profissional = sqldata.RetornaRelacaoProfissional(item.idProfissional);

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
        public static List<ServicoOrdemGridViewModel> ConvertToGridServicoOrdem(List<ServicoOrdem> itens)
        {
            List<ServicoOrdemGridViewModel> retorno = new List<ServicoOrdemGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                ServicoOrdemGridViewModel X = new ServicoOrdemGridViewModel();
                X.servico = sqldata.RetornaServico(item.idServico);
                X.servicoOrdem = item;
                retorno.Add(X);
            }
            return retorno;
        }
        public static List<ProdutoOrdemGridViewModel> ConvertToGridProdutoOrdem(List<ProdutoOrdem> itens)
        {
            List<ProdutoOrdemGridViewModel> retorno = new List<ProdutoOrdemGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                ProdutoOrdemGridViewModel X = new ProdutoOrdemGridViewModel();
                X.produto = sqldata.RetornaProduto(item.idProduto);
                X.produtoOrdem = item;
                retorno.Add(X);
            }
            return retorno;
        }
        public static List<AtendimentoGridViewModel> ConvertToGridAtendimento(List<Atendimento> itens)
        {
            List<AtendimentoGridViewModel> retorno = new List<AtendimentoGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                AtendimentoGridViewModel X = new AtendimentoGridViewModel();
                X.atendimento = item;
                X.cliente = sqldata.RetornaCliente(item.idCliente);
                X.servico = sqldata.RetornaServico(item.idReferencia);                
                X.horaInicio = new ItemHoraDia();
                X.horaInicio.horaDia = (CustomEnum.itemHora)item.horaInicio;
                X.horaFim = new ItemHoraDia();
                X.horaFim.horaDia = (CustomEnum.itemHora)item.horaFim;
                X.horaFim.HoraFormatada = ((CustomEnum.itemHora)item.horaFim).ToString();

                retorno.Add(X);
            }
            return retorno;
        }
    }
}
