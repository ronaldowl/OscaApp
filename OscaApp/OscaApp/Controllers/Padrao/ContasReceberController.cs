using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using OscaFramework.Models;
using OscaFramework.MicroServices;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ContasReceberController : Controller
    {
        private readonly IContasReceberData contasReceberData;
        private ContextPage contexto;
        private readonly SqlGenericData sqlData;
        private readonly IBalcaoVendasData balcaoVendasData;
        private readonly IPedidoData pedidoData;
        private readonly IOrdemServicoData ordemServicoData;
        private readonly IAtendimentoData atendimentoData;
        private readonly IPagamentoData pagamentoData;


        public ContasReceberController(ContexDataService db, IHttpContextAccessor httpContext, SqlGenericData _sqlData)
        {
            this.contasReceberData = new ContasReceberData(db);
            this.pagamentoData = new PagamentoData(db);
            //this.balcaoVendasData   = new BalcaoVendasData(db);
            //this.pedidoData         = new PedidoData(db);
            //this.ordemServicoData   = new OrdemServicoData(db);
            //this.atendimentoData    = new AtendimentoData(db);
            this.sqlData = _sqlData;
            this.contexto = new ContextPage().ExtractContext(httpContext);

        
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateContasReceber(string idCliente)
        {
            ContasReceberViewModel modelo = new ContasReceberViewModel();
            modelo.contasReceber = new ContasReceber();
            modelo.contexto = contexto;
            modelo.contasReceber.criadoEm = DateTime.Now;
            modelo.contasReceber.criadoPorName = contexto.nomeUsuario;

            //Se passar o id carrega o cliente
            if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = sqlData.RetornaRelacaoCliente(new Guid(idCliente));


            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateContasReceber(ContasReceberViewModel entrada)
        {
            ContasReceber modelo = new ContasReceber();

            try
            {
                if (entrada.contasReceber != null)
                {
                    if (ContasReceberRules.ContasReceberCreate(entrada, out modelo, contexto))
                    {
                        contasReceberData.Add(modelo);
                        return RedirectToAction("FormUpdateContasReceber", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 21, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateContasReceber-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateContasReceber(string id)
        {
          

            ContasReceberViewModel modelo = new ContasReceberViewModel();
            modelo.contasReceber = new ContasReceber();
            modelo.contasReceber.id = new Guid(id);

            ContasReceber retorno = new ContasReceber();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = contasReceberData.Get(modelo.contasReceber.id);

                if (retorno.idCliente != null) modelo.cliente = sqlData.RetornaRelacaoCliente(retorno.idCliente);

                if (retorno != null)
                {
                    modelo.contasReceber = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateContasReceber(ContasReceberViewModel entrada)
        {
            ContasReceber modelo = new ContasReceber();
            entrada.contexto = this.contexto;
            try
            {
                if (ContasReceberRules.ContasReceberUpdate(entrada, out modelo))
                {


                    if (entrada.contasReceber.statusContaReceber == CustomEnumStatus.StatusContaReceber.recebido)
                    {

                        //Valida se houve Pagamento total
                        if (entrada.contasReceber.valorPago == entrada.contasReceber.valor)
                        {
                          
                            contasReceberData.Update(modelo);

                            FaturamentoRules.InsereFaturamento((int)entrada.contasReceber.origemContaReceber, entrada.contasReceber.id, entrada.contasReceber.valor, this.contexto.idOrganizacao);
                        }
                        else
                        {
                            StatusMessage = "Valor Pago inconsistente, favor verificar";

                        }
                    }
                    else
                    {
                        ContasReceberRules.CalculoPagamento(ref modelo, pagamentoData, contasReceberData);
                        contasReceberData.Update(modelo);
                        StatusMessage = "Registro Atualizado com Sucesso!";
                    }



                    return RedirectToAction("FormUpdateContasReceber", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 21, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateContasReceber-post", ex.Message);
            }

            return RedirectToAction("FormUpdateContasReceber", new { id = modelo.id.ToString() });
        }

        public ViewResult GridContasReceber(string filtro, int Page, int view)
        {
            IEnumerable<ContasReceberGridViewModel> retorno;

            ViewBag.viewContexto = view;

            retorno = contasReceberData.GetAll(contexto.idOrganizacao, view);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where u.contasReceber.titulo.ToLower().Contains(filtro.ToLower()) || u.contasReceber.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase) || u.contasReceber.numeroReferencia.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)
                          select u;
            }

            retorno = retorno.OrderByDescending(x => x.contasReceber.dataPagamento);


            if (Page == 0) Page = 1;
            return View(retorno.ToPagedList<ContasReceberGridViewModel>(Page, 50));
        }

        public ViewResult GridContasReceberCliente(string idCliente, int page, string filtro, int view)
        {
            IEnumerable<ContasReceber> retorno;
            retorno = contasReceberData.GetAllByIdCliente(new Guid(idCliente), view);

            ViewBag.viewContexto = view;
            ViewBag.idCliente = idCliente;
            ViewBag.nomeCliente = sqlData.RetornaRelacaoCliente(new Guid(idCliente)).idName;

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where u.titulo.ToLower().Contains(filtro.ToLower()) || u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase) || u.numeroReferencia.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)
                          select u;
            }

            retorno = retorno.OrderByDescending(x => x.dataPagamento);

            if (page == 0) page = 1;

            return View(retorno.ToPagedList<ContasReceber>(page, 20));
        }


        [HttpGet]
        public ViewResult FormStatusContasReceber(string id)
        {
            ContasReceberViewModel modelo = new ContasReceberViewModel();
            modelo.contexto = this.contexto;
            try
            {
                ContasReceber retorno = new ContasReceber();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = contasReceberData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.contasReceber = retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 21, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusContasReceber-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormStatusContasReceber(ContasReceberViewModel entrada)
        {
            ContasReceber modelo = new ContasReceber();
            entrada.contexto = this.contexto;

            try
            {
                if (ContasReceberRules.ContasReceberUpdate(entrada, out modelo))
                {

                    contasReceberData.UpdateStatus(modelo);

                    return RedirectToAction("FormUpdateContasReceber", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 21, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusContasReceber-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridContasReceberDia()
        {
            IEnumerable<ContasReceber> retorno;


            retorno = (IEnumerable<ContasReceber>)contasReceberData.GetAllDia(contexto.idOrganizacao);

            retorno = from u in retorno
                      where
                        (u.dataPagamento.Date == DateTime.Now.Date) & (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado)

                      select u;

            retorno = retorno.OrderByDescending(x => x.dataPagamento);

            return View(retorno.ToPagedList<ContasReceber>(1, 10));
        }


    }
}