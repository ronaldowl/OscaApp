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
    public class ContasReceberController : Controller
    {
        private readonly IContasReceberData contasReceberData;
        private ContextPage contexto;

        public ContasReceberController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.contasReceberData = new ContasReceberData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateContasReceber()
        {
            ContasReceberViewModel modelo = new ContasReceberViewModel();
            modelo.contasReceber = new ContasReceber();
            modelo.contexto = contexto;
            modelo.contasReceber.criadoEm = DateTime.Now;
            modelo.contasReceber.criadoPorName = contexto.nomeUsuario;

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

                if (retorno != null)
                {
                    modelo.contasReceber = retorno;
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
                    contasReceberData.Update(modelo);
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

        public ViewResult GridContasReceber(string filtro, int Page)
        {
            IEnumerable<ContasReceber> retorno = contasReceberData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.titulo.StartsWith(filtro))
                                || (u.numeroReferencia.StartsWith(filtro))
                                || (u.codigo.StartsWith(filtro))
                          select u;
            }

            retorno = retorno.OrderByDescending(x => x.dataPagamento);


            if (Page == 0) Page = 1;
            return View(retorno.ToPagedList<ContasReceber>(Page, 10));
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
                log.GravaLog(1,21, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusContasReceber-get", ex.Message);
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

    }
}