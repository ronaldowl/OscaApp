using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class LogOscaController : Controller
    {
        private readonly LogOsca logOscaData;
        private ContextPage contexto;

        public LogOscaController(ContexDataService db, IHttpContextAccessor httpContext)
        {

            logOscaData = new LogOsca();

            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));

        }

        [HttpGet]
        public ViewResult FormViewLogOsca(string id)
        {
            LogOscaViewModel modelo = new LogOscaViewModel();

            modelo.logOsca = new Models.LogOscaModel();
            modelo.Contexto = contexto;
            modelo.logOsca.criadoEm = DateTime.Now;

            modelo.logOsca = logOscaData.get(new Guid(id));

            return View(modelo);
        }

        public ViewResult GridLogOsca(string filtro, int Page)
        {
            List<LogOsca> modelo = logOscaData.getAll(contexto.idOrganizacao);
            //IEnumerable< LogOsca > modelo2 = logOscaData.getAll(contexto.idOrganizacao);

            return View(modelo);
        }
    }
}
