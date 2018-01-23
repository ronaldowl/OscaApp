using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;


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
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
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
                if (entrada.comunicado.titulo!=null)
                {
                  if  (ComunicadoRules.ComunicadoCreate(entrada, out modelo, contexto)) {
                        comunicadoData.Add(modelo);
                        return RedirectToAction("FormUpdateComunicado", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                //TODO: Gravar exce��o no LOG
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
                //TODO: Gravar exce��o no LOG
            }

            return RedirectToAction("FormUpdateComunicado", new { id = modelo.id.ToString() });
        }

        public ViewResult GridComunicado(string filtro, int Page)
        {
            IEnumerable<Comunicado> retorno = comunicadoData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.dataInicio);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Comunicado>(Page, 10));
        }
    }
}