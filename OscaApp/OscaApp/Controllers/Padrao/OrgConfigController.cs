using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.Controllers
{
    [Authorize]
    public class OrgConfigController : Controller
    {
        private readonly IOrgConfigData modeloData;
        private ContextPage contexto;

        public OrgConfigController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new OrgConfigData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormUpdateOrgConfig()
        {
            OrgConfigViewModel modelo = new OrgConfigViewModel();
            modelo.orgConfig = new OrgConfig();        

            OrgConfig retorno = new OrgConfig();
       
            if (contexto.idOrganizacao != null)
            {
                retorno = modeloData.Get(contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.orgConfig = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateOrgConfig(OrgConfigViewModel entrada)
        {
            OrgConfig modelo = new OrgConfig();
            entrada.contexto = this.contexto;
            try
            {
                if (OrgConfigRules.OrgConfigUpdate(entrada, out modelo))
                {
                    modeloData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateOrgConfig", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 34, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateOrgConfig-post", ex.Message);
            }

            return RedirectToAction("FormUpdateOrgConfig", new { id = modelo.id.ToString() });
        }
    }
}