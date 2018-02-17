using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using OscaApp.Models;
using System;
using OscaApp.RulesServices;
using OscaApp.framework;
using OscaFramework.Models;


namespace OscaApp.Controllers
{
    [Authorize]
    public class OrganizacaoController :Controller
    {
        private readonly OrganizacaoData organizacaoData;
 
        private ContextPage contexto;

        public OrganizacaoController(ContexDataManager db, IHttpContextAccessor httpContext)
        {
            this.organizacaoData = new OrganizacaoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormUpdateOrganizacao()
        {
            OrganizacaoViewModel modelo = new OrganizacaoViewModel();
            Organizacao retorno = new Organizacao();      
                      
                retorno = organizacaoData.Get(contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.organizacao = retorno;                 
                }          

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateOrganizacao(OrganizacaoViewModel entrada)
        {
            Organizacao modelo = new Organizacao();
            entrada.contexto = this.contexto;
            try
            {
                if (OrganizacaoRules.MontaOrganizacaoUpdate(entrada, out modelo))
                {
                    organizacaoData.Update(modelo);
                    return RedirectToAction("FormUpdateOrganizacao",null);
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1000, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateOrganizacao-post", ex.Message);
            }

            return RedirectToAction("FormUpdateOrganizacao", null);
        }

    }
}
