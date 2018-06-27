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
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class FaturamentoController : Controller
    {

        private ContextPage contexto;
        private SqlGenericData sqlService;

        public FaturamentoController(IHttpContextAccessor httpContext, SqlGenericData _sqlService)
        {
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.sqlService = _sqlService;
        }

        [TempData]
        public string StatusMessage { get; set; }


        [HttpGet]
        public ViewResult FormViewFaturamento(string id)
        {
            Faturamento modelo = new Faturamento();

            modelo = sqlService.RetornaFaturamento(id);

            return View(modelo);
        }

        [HttpGet]
        public ViewResult GridFaturamento()
        {
            List<FaturamentoGridViewModel> grid = new List<FaturamentoGridViewModel>();

            

            grid = HelperAssociate.ConvertToGridFaturamento(sqlService.ConsultaFaturamento(DateTime.Now.Date.ToString("yyyy-MM-dd"), DateTime.Now.Date.ToString("yyyy-MM-dd"), contexto.idOrganizacao.ToString()));

            return View(grid);
        }

        [HttpPost]
        public ViewResult GridFaturamento(DateTime dataInicio, DateTime dataFim, int Page)
        {        

            List<FaturamentoGridViewModel> grid = new List<FaturamentoGridViewModel>();

           

            grid = HelperAssociate.ConvertToGridFaturamento(sqlService.ConsultaFaturamento(dataInicio.ToString("yyyy-MM-dd"), dataFim.ToString("yyyy-MM-dd"), contexto.idOrganizacao.ToString()));

            return View(grid);
        }
    }
}