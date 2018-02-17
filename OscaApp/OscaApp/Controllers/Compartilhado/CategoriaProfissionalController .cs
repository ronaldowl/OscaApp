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
    public class CategoriaProfissionalController : Controller
    {
        private readonly ICategoriaProfissionalData categoriaProfissionalData;
        private ContextPage contexto;

        public CategoriaProfissionalController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.categoriaProfissionalData = new CategoriaProfissionalData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        public ViewResult GridCategoriaProfissional(string filtro, int Page)
        {
            IEnumerable<CategoriaProfissional> retorno =categoriaProfissionalData.GetAll();

            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<CategoriaProfissional>(Page, 10));
        }
    }
}
