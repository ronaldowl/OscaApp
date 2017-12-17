using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ViewResult FormCreateListaPreco()
        {
            ListaPrecoViewModel modelo = new ListaPrecoViewModel();
            modelo.listaPreco = new ListaPreco();
            modelo.contexto = contexto;
            modelo.listaPreco.criadoEm = DateTime.Now;
            modelo.listaPreco.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateListaPreco(ListaPrecoViewModel entrada)
        {
            ListaPreco listaPreco = new ListaPreco();
         try
            {
                if (entrada.listaPreco.nome != null)
                {
                    if (ListaPrecoRules.ListaPrecoCreate(entrada, out listaPreco, contexto))
                    {
                        listaPrecoData.Add(listaPreco);
                        return RedirectToAction("FormUpdateListaPreco", new { id = listaPreco.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }
            return View();
        }

        [HttpPost]
        public IActionResult FormUpdateListaPreco(ListaPrecoViewModel entrada)
        {
            ListaPreco listapreco = new ListaPreco();
            entrada.contexto = this.contexto;
            try
            {
                if (ListaPrecoRules.ListaPrecoUpdate(entrada, out listapreco))
                {
                    listaPrecoData.Update(listapreco);
                    return RedirectToAction("FormUpdateListaPreco", new { id = listapreco.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }

            return RedirectToAction("FormUpdateListaPreco", new { id = listapreco.id.ToString() });
        }

        [HttpGet]
        public ViewResult FormUpdateListaPreco(string id)
        {
            ListaPrecoViewModel listapreco = new ListaPrecoViewModel();
            listapreco.listaPreco= new ListaPreco();
            listapreco.listaPreco.id = new Guid(id);

            ListaPreco retorno = new ListaPreco();
            {
                retorno = listaPrecoData.Get(new Guid(id), contexto.idOrganizacao);

                if (retorno != null)
                {
                    listapreco.listaPreco = retorno;
                }
            }
            return View(listapreco);
        }


        public ViewResult GridListaPreco(string filtro, int Page)
        {
            IEnumerable<ListaPreco> retorno = listaPrecoData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ListaPreco>(Page, 10));
        }
    }
}
