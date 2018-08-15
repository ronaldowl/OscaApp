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


namespace OscaApp.Controllers
{
    [Authorize]
    public class PagamentoController : Controller
    {
        private readonly IPagamentoData pagamentoData;
        private readonly IContasReceberData contasReceberData;


        private ContextPage contexto;

        public PagamentoController(ContexDataService db, IHttpContextAccessor httpContext)
        {

            this.pagamentoData = new PagamentoData(db);
            this.contasReceberData = new ContasReceberData(db);
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreatePagamento(string idContasReceber)
        {
            PagamentoViewModel modelo = new PagamentoViewModel();
            modelo.pagamento = new Pagamento();
            modelo.contexto = contexto;
            modelo.pagamento.criadoEm = DateTime.Now;
            modelo.pagamento.criadoPorName = contexto.nomeUsuario;
            modelo.contasReceber = new Relacao();
            modelo.contasReceber.id = new Guid(idContasReceber);
            modelo.pagamento.valor = contasReceberData.Get(new Guid(idContasReceber)).valorRestante;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreatePagamento(PagamentoViewModel entrada)
        {
            Pagamento pagamento = new Pagamento();
            try
            {
                if (entrada.pagamento != null)
                {
                    if (PagamentoRules.PagamentoCreate(entrada, out pagamento, contexto))
                    {
                        pagamentoData.Add(pagamento);                      

                        return RedirectToAction("FormUpdateContasReceber", "ContasReceber",new { id = entrada.contasReceber.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,12, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePagamento-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdatePagamento(string id)
        {
            PagamentoViewModel modelo = new PagamentoViewModel();
            modelo.pagamento = new Pagamento();
            modelo.pagamento.id = new Guid(id);
            try
            {
                Pagamento retorno = new Pagamento();
                {
                    retorno = pagamentoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.pagamento = retorno;
                        //apresenta mensagem de registro atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,12, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePagamento-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdatePagamento(PagamentoViewModel entrada)
        {
            Pagamento listapreco = new Pagamento();
            entrada.contexto = this.contexto;
            try
            {
                if (PagamentoRules.PagamentoUpdate(entrada, out listapreco))
                {
                    pagamentoData.Update(listapreco);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdatePagamento", new { id = listapreco.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,12, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePagamento-post", ex.Message);
            }

            return RedirectToAction("FormUpdatePagamento", new { id = listapreco.id.ToString() });
        }
        
        public ViewResult GridPagamento(string idContasReceber)
        {
            IEnumerable<Pagamento> retorno = pagamentoData.GetAllByContasReceber(new Guid(idContasReceber));
                      
            retorno = retorno.OrderBy(x => x.dataPagamento);

            return View(retorno.ToList());
        }

        public IActionResult DeletePagamento(string idPagamento, string idContasReceber)
        {
            Pagamento modelo = new Pagamento();
            modelo.id = new Guid(idPagamento);
            pagamentoData.Delete(modelo);        
            return RedirectToAction("GridPagamento", "Pagamento", new { idContasReceber = idContasReceber });
        }
    }
}
