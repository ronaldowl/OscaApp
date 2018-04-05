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
    public class ListaPrecoController : Controller
    {
        private readonly IListaPrecoData listaPrecoData;
        private ContextPage contexto;

        public ListaPrecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {

            this.listaPrecoData = new ListaPrecoData(db);

            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateListaPreco()
        {
            ListaPrecoViewModel modelo = new ListaPrecoViewModel();
            modelo.listaPreco = new ListaPreco();
            modelo.contexto = contexto;
            modelo.listaPreco.criadoEm = DateTime.Now;
            modelo.listaPreco.criadoPorName = contexto.nomeUsuario;

            //Inicia campo a partir de um ano
            modelo.dataValidade = DateTime.Now.AddYears(1);

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateListaPreco(ListaPrecoViewModel entrada)
        {
            ListaPreco listaPreco = new ListaPreco();
            try
            {
                if (entrada.listaPreco != null)
                {
                    if (ListaPrecoRules.ListaPrecoCreate(entrada, out listaPreco, contexto))
                    {
                        listaPrecoData.Add(listaPreco);
                        StatusMessage = "Registro Atualizado com Sucesso!";

                        return RedirectToAction("FormUpdateListaPreco", new { id = listaPreco.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,12, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateListaPreco-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateListaPreco(string id)
        {
            ListaPrecoViewModel listapreco = new ListaPrecoViewModel();
            listapreco.listaPreco = new ListaPreco();
            listapreco.listaPreco.id = new Guid(id);
            try
            {
                ListaPreco retorno = new ListaPreco();
                {
                    retorno = listaPrecoData.Get(new Guid(id), contexto.idOrganizacao);

                    if (retorno != null)
                    {
                        listapreco.listaPreco = retorno;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,12, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateListaPreco-get", ex.Message);
            }
            return View(listapreco);
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
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateListaPreco", new { id = listapreco.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,12, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateListaPreco-post", ex.Message);
            }

            return RedirectToAction("FormUpdateListaPreco", new { id = listapreco.id.ToString() });
        }
        
        public ViewResult GridListaPreco(string filtro, int Page)
        {
            IEnumerable<ListaPreco> retorno = listaPrecoData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.nome.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))
                               
                          select u;
            }
            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ListaPreco>(Page, 10));
        }
    }
}
