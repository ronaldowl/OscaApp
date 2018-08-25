using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.ViewModels;
using OscaFramework.MicroServices;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class LogOscaController : Controller
    {
        private ContextPage contexto;
        private LogOsca logOscaData;

        public LogOscaController(IHttpContextAccessor httpContext)
        {
            //this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.logOscaData = new LogOsca();
        }

        public ViewResult GridLogOsca(int filtro)
        {

            IEnumerable<LogOsca> modelo = logOscaData.getAll(contexto.idOrganizacao);

            if (filtro > 0) modelo = from A in modelo where (A.codigoErro == filtro) select A;

            return View(modelo.ToList());
        }

        public ViewResult GridLogAcesso()
        {
            SqlGenericManager sqlService = new SqlGenericManager();

            return View(sqlService.ListaLogAcesso(this.contexto.idOrganizacao.ToString()));
        }

        [HttpGet]
        public ViewResult FormViewLogOsca(string id)
        {
            LogOscaViewModel modelo = new LogOscaViewModel();

            modelo.logOsca = new LogOsca();
            modelo.Contexto = contexto;
            modelo.logOsca.dataCriacao = DateTime.Now;
            modelo.logOsca = logOscaData.get(new Guid(id));

            return View(modelo);
        }
        
    }
}
