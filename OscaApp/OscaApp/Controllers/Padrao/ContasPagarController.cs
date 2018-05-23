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
    public class ContasPagarController : Controller
    {
        private readonly IContasPagarData contasPagarData;
        private ContextPage contexto;

        public ContasPagarController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.contasPagarData = new ContasPagarData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateContasPagar()
        {
            ContasPagarViewModel modelo = new ContasPagarViewModel();
            modelo.contasPagar = new ContasPagar();
            modelo.contexto = contexto;
            modelo.contasPagar.criadoEm = DateTime.Now;
            modelo.contasPagar.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateContasPagar(ContasPagarViewModel entrada)
        {
            ContasPagar modelo = new ContasPagar();

            try
            {
                if (entrada.contasPagar != null)
                {
                    if (ContasPagarRules.ContasPagarCreate(entrada, out modelo, contexto))
                    {
                        contasPagarData.Add(modelo);
                        return RedirectToAction("FormUpdateContasPagar", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 20, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateContasPagar-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateContasPagar(string id)
        {
            ContasPagarViewModel modelo = new ContasPagarViewModel();
            modelo.contasPagar = new ContasPagar();
            modelo.contasPagar.id = new Guid(id);

            ContasPagar retorno = new ContasPagar();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = contasPagarData.Get(modelo.contasPagar.id);

                if (retorno != null)
                {
                    modelo.contasPagar = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateContasPagar(ContasPagarViewModel entrada)
        {
            ContasPagar modelo = new ContasPagar();
            entrada.contexto = this.contexto;
            try
            {
                if (ContasPagarRules.ContasPagarUpdate(entrada, out modelo))
                {
                    contasPagarData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateContasPagar", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 20, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateContasPagar-post", ex.Message);
            }

            return RedirectToAction("FormUpdateContasPagar", new { id = modelo.id.ToString() });
        }

        public ViewResult GridContasPagar(string filtro, int Page,int view)
        {
            IEnumerable<ContasPagar> retorno = contasPagarData.GetAll(contexto.idOrganizacao, view);

            ViewBag.viewContexto = view;

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where
                  ( u.titulo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))              
                || (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) select u;
            }

            retorno = retorno.OrderByDescending(x => x.dataPagamento);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ContasPagar>(Page, 20));
        }

        [HttpGet]
        public ViewResult FormStatusContasPagar(string id)
        {
            ContasPagarViewModel modelo = new ContasPagarViewModel();
            modelo.contexto = this.contexto;
            try
            {
                ContasPagar retorno = new ContasPagar();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = contasPagarData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.contasPagar = retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 20, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusContasPagar-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormStatusContasPagar(ContasPagarViewModel entrada)
        {
            ContasPagar modelo = new ContasPagar();
            entrada.contexto = this.contexto;

            try
            {
                if (ContasPagarRules.ContasPagarUpdate(entrada, out modelo))
                {

                    contasPagarData.UpdateStatus(modelo);

                    return RedirectToAction("FormUpdateContasPagar", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 20, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusContasPagar-post", ex.Message);
            }
            return View();
        }


    }
}