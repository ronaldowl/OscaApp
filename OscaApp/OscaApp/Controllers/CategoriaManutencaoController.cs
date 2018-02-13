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
    public class CategoriaManutencaoController : Controller
    {
        private readonly SqlGenericServices sqlServices;
        

        public CategoriaManutencaoController(SqlGenericServices _sqlServices)
        {
            this.sqlServices = _sqlServices;            
        }

        public ViewResult GridCategoriaManutencao(string filtro, int Page)
        {
            IEnumerable<CategoriaManutencao> retorno = sqlServices.RetornaCategoriaManutencao();

            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<CategoriaManutencao>(Page, 10));
        }
        public ViewResult LookupCategoriaManutencao(string filtro, int Page)
        {
            IEnumerable<CategoriaManutencao> retorno = sqlServices.RetornaCategoriaManutencao();


            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<CategoriaManutencao>(Page, 10));
        }
    }
}
