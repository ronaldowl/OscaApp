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
    public class BancoController : Controller
    {
        private readonly IBancoData bancoData;
        private ContextPage contexto;

        public BancoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.bancoData = new BancoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        public ViewResult GridBanco(string filtro, int Page)
        {
            IEnumerable<Banco> retorno = bancoData.GetAll();

            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Banco>(Page, 10));
        }

        public ViewResult LookupBanco(string filtro, int Page)
        {
            IEnumerable<Banco> retorno = bancoData.GetAll();

            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Banco>(Page, 10));
        }

    }
}
