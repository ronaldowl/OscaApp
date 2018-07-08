﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;
using OscaFramework.MicroServices;
using OscaApp.RulesServices;
using Microsoft.AspNetCore.Http;
using OscaApp.ViewModels;

namespace OscaAPI.Controllers
{

    public class BalcaoVendasAPIController : Controller
    {

        private readonly SqlGenericRules sqlServices;
        private readonly SqlGeneric sqlGeneric;
        private readonly ContextPage contexto;
        private readonly IBalcaoVendasData balcaoVendasData;

        public BalcaoVendasAPIController(SqlGeneric _sqlGeneric, SqlGenericRules _sqlRules, IHttpContextAccessor httpContext, ContexDataService db)
        {
            this.balcaoVendasData = new BalcaoVendasData(db);
            this.sqlServices = _sqlRules;
            this.sqlGeneric =  _sqlGeneric;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [Route("api/[controller]/ConsultaProduto")]
        [HttpGet("{filtro, idLista}")]
        public JsonResult ConsultaProduto(string filtro, string idLista)
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.ListaProdutoBalcao = sqlServices.ConsultaProduto(filtro, idLista);
                retorno.statusOperation = true;

                return Json(retorno);
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }

        [Route("api/[controller]/GravarVenda")]
        [HttpGet("{entrada, produtosBalcao}")]
        public JsonResult GravarVenda(BalcaoVendas modelo, ProdutoBalcao[] produtosBalcao)
        {

            ResultServiceList retorno = new ResultServiceList();
            BalcaoVendasViewModel entrada = new BalcaoVendasViewModel();
            entrada.balcaoVendas = modelo;
            entrada.contexto = this.contexto;
            Guid idBalcaoVendas = new Guid();
         

            try
            {
                idBalcaoVendas = BalcaoVendasRules.BalcaoVendasCreate(entrada, this.contexto,balcaoVendasData);

                if (BalcaoVendasRules.GravaProdutoBalcao(produtosBalcao, this.contexto, this.sqlGeneric, idBalcaoVendas))
                {
                    //Grava lançamento na tabela de faturamento
                    entrada.balcaoVendas.id = idBalcaoVendas;
                    FaturamentoRules.InsereFaturamento(entrada.balcaoVendas, this.contexto.idOrganizacao);

                    retorno.id = idBalcaoVendas.ToString();
                    retorno.statusOperation = true;

                    return Json(retorno);
                }
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);

        }
    }
}