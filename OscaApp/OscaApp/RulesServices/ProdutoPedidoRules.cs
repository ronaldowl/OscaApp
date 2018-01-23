﻿using OscaApp.Data;
using OscaApp.framework.Models;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.RulesServices
{
    public static class ProdutoPedidoRules
    {
        public static bool MontaProdutoPedidoCreate(ProdutoPedidoViewModel entrada,out ProdutoPedido modelo, ContextPage contexto )
        {
            modelo = new ProdutoPedido ();
            modelo = entrada.produtoPedido;
            modelo.idProduto = entrada.produto.id;

            if (modelo.idProduto != null)
            {
                //*************Executa cálculo

                if (modelo.tipoDesconto == CustomEnum.tipoDesconto.Money)
                {                  
                    modelo.total = modelo.valor * modelo.quantidade;
                    modelo.valorDesconto = modelo.valorDescontoMoney;
                    modelo.totalGeral = modelo.total - modelo.valorDesconto;
                    
                }
                else {

                    modelo.total = modelo.valor * modelo.quantidade;
                    modelo.valorDesconto =  modelo.total -  modelo.valorDescontoPercentual / 100;
                    modelo.totalGeral = modelo.total - modelo.valorDesconto;

                }

                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool MontaProdutoPedidoUpdate(ProdutoPedidoViewModel entrada,out ProdutoPedido modelo)
        {
                modelo = new ProdutoPedido();

            ////************ Objetos de controle de acesso ***************
            modelo = entrada.produtoPedido;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************

            return true;
        }

        //public static List<ItemProdutoLista> RetornaItemListaProduto(List<ItemListaPreco> itens)
        //{
        //    List<ItemProdutoLista> retorno = new List<ItemProdutoLista>();
        //    SqlGenericData sqldata = new SqlGenericData();

        //    foreach (var item in itens)
        //    {
        //        ItemProdutoLista X = new ItemProdutoLista();
        //        X.id = item.id;
        //        X.dataCriacao = item.criadoEm;
        //        X.idListaPreco = item.idListaPreco;
        //        X.nomeListaPreco = sqldata.RetornaListaPreco(item.idListaPreco).nome;
        //        X.valorVenda = item.valor.ToString("N2");
        //        retorno.Add(X);
        //    }
           
        //    return retorno;
        //}
    }
}