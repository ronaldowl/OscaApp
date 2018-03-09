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
    public class AtividadeController : Controller
    {
        private readonly IAtividadeData atividadeData;
        private ContextPage contexto;

        public AtividadeController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.atividadeData = new AtividadeData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateAtividade()
        {
            AtividadeViewModel modelo = new AtividadeViewModel();
            modelo.atividade = new Atividade();
            modelo.contexto = contexto;
            modelo.atividade.criadoEm = DateTime.Now;
            modelo.atividade.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateAtividade(AtividadeViewModel entrada)
        {
            Atividade modelo = new Atividade();

            try
            {
                if (entrada.atividade != null)
                {
                  if  (AtividadeRules.AtividadeCreate(entrada, out modelo, contexto)) {
                        atividadeData.Add(modelo);
                        return RedirectToAction("FormUpdateAtividade", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 27, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateAtividade-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateAtividade(string id)
        {
            AtividadeViewModel modelo = new AtividadeViewModel();
            modelo.atividade = new Atividade();
            modelo.atividade.id = new Guid(id);

            Atividade retorno = new Atividade();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = atividadeData.Get(modelo.atividade.id, contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.atividade = retorno;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateAtividade(AtividadeViewModel entrada)
        {
            Atividade modelo = new Atividade();
            entrada.contexto = this.contexto;
            try
            {
                if (AtividadeRules.AtividadeUpdate(entrada, out modelo))
                {
                    atividadeData.Update(modelo);
                    return RedirectToAction("FormUpdateAtividade", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 27, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateAtividade-post", ex.Message);
            }

            return RedirectToAction("FormUpdateAtividade", new { id = modelo.id.ToString() });
        }

        public ViewResult GridAtividade(string filtro, int Page)
        {
            IEnumerable<Atividade> retorno = atividadeData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.dataAlvo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Atividade>(Page, 10));
        }
    }
}
