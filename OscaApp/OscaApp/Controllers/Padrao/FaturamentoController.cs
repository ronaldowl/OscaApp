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
    public class FaturamentoController : Controller
    {
        private readonly IRecursoData modeloData;
        private ContextPage contexto;

        public FaturamentoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new RecursoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

  
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
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        public ViewResult GridRecurso(string filtro, int Page)
        {
            IEnumerable<Recurso> retorno = modeloData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)
                          ||(u.nome.ToLower().Contains(filtro.ToLower()))
                          select u;
            }
            retorno = retorno.OrderBy(x => x.codigo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Recurso>(Page, 20));
        }
    }
}