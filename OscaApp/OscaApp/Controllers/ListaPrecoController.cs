using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ListaPrecoController : Controller
    {
        private readonly IListaPrecoData listaPrecoData;
        private ContextPage contexto;

        public ListaPrecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {

            this.listaPrecoData = new ListaPrecoData(db);

            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));

        }





        [HttpGet]
        public IActionResult FormCreateListaPreco(){
            return View();
        }

        [HttpPost]
        public IActionResult FormCreateListaPreco( int x){
            return RedirectToAction("FormUpdateListaPreco", new {});
        }
        public IActionResult FormUpdateListaPreco()
        {
            return View();
        }


        public ViewResult GridListaPreco(string filtro, int Page)
        {
            IEnumerable<ListaPreco> retorno = listaPrecoData.GetAll(contexto.idOrganizacao);

            //realiza busca por Nome, Código, Email e CPF
            //if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.nome == filtro || A.telefone == filtro || A.cpf == filtro || A.email == filtro) select A;

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ListaPreco>(Page, 10));
        }
    }
}
