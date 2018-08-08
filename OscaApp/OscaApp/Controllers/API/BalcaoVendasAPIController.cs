using System;
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
        private readonly IContasReceberData contaReceberData;
        private readonly IProdutoData produtoData;
        private readonly OrgConfig orgConfig;
        private readonly IOrgConfigData orgConfigData;


        private readonly ClienteData clienteData;

        public BalcaoVendasAPIController(SqlGeneric _sqlGeneric, SqlGenericRules _sqlRules, IHttpContextAccessor httpContext, ContexDataService db)
        {
            this.balcaoVendasData = new BalcaoVendasData(db);
            this.contaReceberData = new ContasReceberData(db);
            this.produtoData = new ProdutoData(db);
            this.orgConfigData = new OrgConfigData(db);
            this.sqlServices = _sqlRules;
            this.sqlGeneric =  _sqlGeneric;
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.clienteData = new ClienteData(db);
            this.orgConfig = this.orgConfigData.Get(this.contexto.idOrganizacao);
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
        public JsonResult GravarVenda(BalcaoVendas modelo, ProdutoBalcao[] produtosBalcao, Cliente cliente)
        {

            ResultServiceList retorno = new ResultServiceList();
            BalcaoVendasViewModel entrada = new BalcaoVendasViewModel();
            entrada.balcaoVendas = modelo;
            entrada.contexto = this.contexto;
            Guid idBalcaoVendas = new Guid();
                      
         
            try
            {
                if (cliente.id != Guid.Empty)
                {
                    entrada.cliente = new Relacao();
                    entrada.cliente.id = cliente.id;
                }
                else
                {
                    if (cliente.nomeCliente != null)
                    {
                        entrada.cliente = new Relacao();
                        entrada.cliente.id = ClienteRules.CreateClienteResumo(cliente, this.contexto, clienteData);
                    }
                }

                idBalcaoVendas = BalcaoVendasRules.BalcaoVendasCreate(entrada, this.contexto,balcaoVendasData);

                if (BalcaoVendasRules.GravaProdutoBalcao(produtosBalcao, this.contexto, this.sqlGeneric, idBalcaoVendas))
                {
                    //Grava lançamento na tabela de faturamento
                    entrada.balcaoVendas.id = idBalcaoVendas;
                    
                    //Grava Parcelas
                    if (entrada.balcaoVendas.condicaoPagamento == CustomEnum.codicaoPagamento.Prazo)
                    {
                        ContasReceberRules.GravaParcela(entrada.balcaoVendas, this.contaReceberData, this.contexto, this.orgConfig);
                    }

                    if (entrada.balcaoVendas.condicaoPagamento == CustomEnum.codicaoPagamento.Avista)
                    {
                        //Grava Debito
                        if (entrada.balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.CartaoDebito)
                        {
                            ContasReceberRules.GravaDebito(entrada.balcaoVendas, this.contaReceberData, this.contexto, this.orgConfig);
                        }

                        if (entrada.balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Dinheiro || entrada.balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Online)
                        {
                            FaturamentoRules.InsereFaturamento(entrada.balcaoVendas, this.contexto.idOrganizacao);
                        }

                        if (entrada.balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Tranferencia || entrada.balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Deposito || entrada.balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Cheque)
                        {
                            FaturamentoRules.InsereFaturamento(entrada.balcaoVendas, this.contexto.idOrganizacao);

                        }
                    }

                    if (entrada.balcaoVendas.condicaoPagamento == CustomEnum.codicaoPagamento.Consignado)
                    {
                        //Lançamento Manual
                    }

                    //Baixa Estoque
                    ProdutoRules.BaixaProdutoBalcao(produtosBalcao, contexto, produtoData);

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

        [Route("api/[controller]/RetornaValorVendaDiario")]
        [HttpGet()]
        public JsonResult RetornaValorVendaDiario()
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.valor = sqlServices.RetornaValorDiarioVenda( this.contexto.idOrganizacao);
                retorno.statusOperation = true;

                return Json(retorno);
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }
    }
}
