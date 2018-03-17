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
    public class ComunicadoController : Controller
    {
        private readonly IComunicadoData comunicadoData;
        private ContextPage contexto;

        public ComunicadoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.comunicadoData = new ComunicadoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateComunicado()
        {
            ComunicadoViewModel modelo = new ComunicadoViewModel();
            modelo.comunicado = new Comunicado();
            modelo.Contexto = contexto;
            modelo.comunicado.criadoEm = DateTime.Now;
            modelo.comunicado.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateComunicado(ComunicadoViewModel entrada)
        {
            Comunicado modelo = new Comunicado();

            try
            {
                if (entrada.comunicado != null)
                {
                  if  (ComunicadoRules.ComunicadoCreate(entrada, out modelo, contexto)) {
                        comunicadoData.Add(modelo);
                        return RedirectToAction("FormUpdateComunicado", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 19, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateComunicado-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateComunicado(string id)
        {
            ComunicadoViewModel modelo = new ComunicadoViewModel();
            modelo.comunicado = new Comunicado();
            modelo.comunicado.id = new Guid(id);

            Comunicado retorno = new Comunicado();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = comunicadoData.Get(modelo.comunicado.id, contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.comunicado = retorno;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateComunicado(ComunicadoViewModel entrada)
        {
            Comunicado modelo = new Comunicado();
            entrada.Contexto = this.contexto;
            try
            {
                if (ComunicadoRules.ComunicadoUpdate(entrada, out modelo))
                {
                    comunicadoData.Update(modelo);
                    return RedirectToAction("FormUpdateComunicado", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 19, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateComunicado-post", ex.Message);
            }

            return RedirectToAction("FormUpdateComunicado", new { id = modelo.id.ToString() });
        }

        public ViewResult GridComunicado(string filtro, int Page)
        {
            IEnumerable<Comunicado> retorno = comunicadoData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where u.titulo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase) select u;
            }

            retorno = retorno.OrderBy(x => x.dataInicio);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Comunicado>(Page, 10));
        }

        public ViewResult QuadroComunicado(string filtro, int Page)
        {
            IEnumerable<Comunicado> retorno = comunicadoData.GetAll(contexto.idOrganizacao);

            DateTime hoje = DateTime.Now.AddDays(1);
             retorno = from A in retorno where (A.dataInicio <= DateTime.Now) && (A.dataFim > hoje) select A;

            retorno = retorno.OrderBy(x => x.dataInicio);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Comunicado>(Page, 10));
        }

        public ViewResult QuadroComunicadoResumido(string filtro, int Page)
        {
            IEnumerable<Comunicado> retorno = comunicadoData.GetAll(contexto.idOrganizacao);

            //realiza busca por Nome, Código, Email e CPF
            DateTime hoje = DateTime.Now.AddDays(1);
            retorno = from A in retorno where (A.dataInicio <= DateTime.Now) && (A.dataFim > hoje) select A;

            retorno = retorno.OrderBy(x => x.dataInicio);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Comunicado>(Page, 10));
        }
    }
}
