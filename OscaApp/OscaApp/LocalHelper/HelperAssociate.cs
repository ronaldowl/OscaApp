﻿using OscaApp.Data;
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

        public static List<AtividadeGridViewModel> ConvertToGridAtividade(List<Atividade> itens)
        {
            List<AtividadeGridViewModel> retorno = new List<AtividadeGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                AtividadeGridViewModel X = new AtividadeGridViewModel();
                X.profissional = sqldata.RetornaRelacaoProfissional(item.idProfissional);
                X.atividade = item;
                retorno.Add(X);
            }
            return retorno;
        }
        public static List<BalcaoVendasGridViewModel> ConvertToGridBalcaoVendas(List<BalcaoVendas> itens)
        {
            List<BalcaoVendasGridViewModel> retorno = new List<BalcaoVendasGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();
           
                foreach (var item in itens)
                {
                    BalcaoVendasGridViewModel X = new BalcaoVendasGridViewModel();

                    X.balcaoVendas = item;

                    if (item.idCliente != Guid.Empty) X.cliente = sqldata.RetornaCliente(item.idCliente);

                    retorno.Add(X);
                }
         
            return retorno;
        }
        public static List<PedidoRetiradaGridViewModel> ConvertToGridPedido(List<PedidoRetirada> itens)
        {
            List<PedidoRetiradaGridViewModel> retorno = new List<PedidoRetiradaGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                PedidoRetiradaGridViewModel X = new PedidoRetiradaGridViewModel();
                X.cliente = sqldata.RetornaCliente(item.idCliente);
                X.pedidoRetirada = item;

                retorno.Add(X);
            }
            return retorno;
        }
        public static List<ContasReceberGridViewModel> ConvertToGridContasReceber(List<ContasReceber> itens, ClienteData clienteData)
        {
            List<ContasReceberGridViewModel> retorno = new List<ContasReceberGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                ContasReceberGridViewModel X = new ContasReceberGridViewModel();
                //if (item.idCliente != Guid.Empty)
                //{
                //    X.cliente = clienteData.Get(item.idCliente, item.idOrganizacao);
                //}

                X.contasReceber = item;

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
                X.servico = sqldata.RetornaServico(item.idServico);               
                
                retorno.Add(X);
            }
            return retorno;
        }
        public static List<AgendamentoGridViewModel> ConvertToGridAgendamento(List<Agendamento> itens)
        {
            List<AgendamentoGridViewModel> retorno = new List<AgendamentoGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                AgendamentoGridViewModel X = new AgendamentoGridViewModel();
                X.agendamento = item;
                X.cliente = sqldata.RetornaCliente(item.idCliente);
                X.horaFim = new ItemHoraDia();
                X.horaFim.horaDia = (CustomEnum.itemHora)item.horaFim;
                X.horaInicio = new ItemHoraDia();
                X.horaInicio.horaDia = (CustomEnum.itemHora)item.horaInicio;

                if (item.tipoReferencia == CustomEnum.TipoReferencia.OrdemServico)
                {
                    X.Tiporeferencia = sqldata.RetornaRelacaoOrdemServico(item.idReferencia);
                    X.referencia = "OrdemServiço";
                }

                if (item.tipoReferencia == CustomEnum.TipoReferencia.Pedido)
                {
                    X.Tiporeferencia = sqldata.RetornaRelacaoPedido(item.idReferencia);
                    X.referencia = "Pedido";
                }

                if (item.tipoReferencia == CustomEnum.TipoReferencia.Atendimento)
                {
                    X.Tiporeferencia = sqldata.RetornaRelacaoAtendimento(item.idReferencia);
                    X.referencia = "Atendimento";
                }

                retorno.Add(X);
            }
            return retorno;
        }
        public static List<FaturamentoGridViewModel> ConvertToGridFaturamento(List<Faturamento> itens)
        {
            List<FaturamentoGridViewModel> retorno = new List<FaturamentoGridViewModel>();

          
            
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                FaturamentoGridViewModel X = new FaturamentoGridViewModel();              
                X.faturamento = item;
                Relacao relacao = new Relacao();


                if (item.origemFaturamento == CustomEnum.OrigemFaturamento.OrdemServico)
                {
                    relacao = sqldata.RetornaRelacaoOrdemServico(item.idReferencia);
                    X.faturamento.idReferencia = relacao.id;
                    X.faturamento.origemFaturamento = CustomEnum.OrigemFaturamento.OrdemServico;
                    X.codigo = relacao.codigo;
                }

                if (item.origemFaturamento == CustomEnum.OrigemFaturamento.Pedido)
                {
                    relacao = sqldata.RetornaRelacaoPedido(item.idReferencia);
                    X.faturamento.idReferencia = relacao.id;
                    X.faturamento.origemFaturamento = CustomEnum.OrigemFaturamento.Pedido;
                    X.codigo = relacao.codigo;
                }

                if (item.origemFaturamento == CustomEnum.OrigemFaturamento.Atendimento)
                {
                    relacao = sqldata.RetornaRelacaoAtendimento(item.idReferencia);
                    X.faturamento.idReferencia = relacao.id;
                    X.faturamento.origemFaturamento = CustomEnum.OrigemFaturamento.Atendimento;
                    X.codigo = relacao.codigo;
                }

                if (item.origemFaturamento == CustomEnum.OrigemFaturamento.BalcaoVendas)
                {
                    relacao = sqldata.RetornaRelacaoBalcaoVendas(item.idReferencia);
                    X.faturamento.origemFaturamento = CustomEnum.OrigemFaturamento.BalcaoVendas;
                    X.codigo = relacao.codigo;
                }
               
                retorno.Add(X);
            }       
            return retorno;
        }

        public static List<ProdutoFornecedorGridViewModel> ConvertToGridProdutoFornecedor(List<ProdutoFornecedor> itens)
        {
            List<ProdutoFornecedorGridViewModel> retorno = new List<ProdutoFornecedorGridViewModel>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                ProdutoFornecedorGridViewModel X = new ProdutoFornecedorGridViewModel();
                X.fornecedor = sqldata.RetornaFornecedor(item.idFornecedor);
                X.produtoFornecedor = item;
                X.produto = sqldata.RetornaProduto(item.idProduto);
                retorno.Add(X);
            }
            return retorno;
        }

    }
}
