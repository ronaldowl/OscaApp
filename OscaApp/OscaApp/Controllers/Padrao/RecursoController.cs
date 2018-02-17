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
    public class RecursoController : Controller
    {
        private readonly IRecursoData modeloData;
        private ContextPage contexto;

        public RecursoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new RecursoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateRecurso()
        {
            RecursoViewModel modelo = new RecursoViewModel();
            modelo.recurso = new Recurso();
            modelo.contexto = contexto;
            modelo.recurso.criadoEm = DateTime.Now;
            modelo.recurso.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateRecurso(RecursoViewModel entrada)
        {
            Recurso modelo = new Recurso();

            try
            {
                if (entrada.recurso != null)
                {
                    if (RecursoRules.RecursoCreate(entrada, out modelo, contexto))
                    {
                        modeloData.Add(modelo);
                        return RedirectToAction("FormUpdateRecurso", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 22, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateRecurso-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateRecurso(string id)
        {
            RecursoViewModel modelo = new RecursoViewModel();
            modelo.recurso = new Recurso();
            modelo.recurso.id = new Guid(id);

            Recurso retorno = new Recurso();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.recurso.id, contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.recurso = retorno;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateRecurso(RecursoViewModel entrada)
        {
            Recurso modelo = new Recurso();
            entrada.contexto = this.contexto;
            try
            {
                if (RecursoRules.RecursoUpdate(entrada, out modelo))
                {
                    modeloData.Update(modelo);
                    return RedirectToAction("FormUpdateRecurso", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 22, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateRecurso-post", ex.Message);
            }

            return RedirectToAction("FormUpdateRecurso", new { id = modelo.id.ToString() });
        }

        public ViewResult GridRecurso(string filtro, int Page)
        {
            IEnumerable<Recurso> retorno = modeloData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.codigo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Recurso>(Page, 10));
        }
    }
}