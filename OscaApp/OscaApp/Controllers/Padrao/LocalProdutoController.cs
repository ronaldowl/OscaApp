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
    public class LocalProdutoController : Controller
    {
        private readonly ILocalProdutoData modeloData;
        private ContextPage contexto;

        public LocalProdutoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new LocalProdutoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateLocalProduto()
        {
            LocalProdutoViewModel modelo = new LocalProdutoViewModel();
            modelo.localProduto = new LocalProduto();
            modelo.contexto = contexto;
            modelo.localProduto.criadoEm = DateTime.Now;
            modelo.localProduto.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateLocalProduto(LocalProdutoViewModel entrada)
        {
            LocalProduto modelo = new LocalProduto();

            try
            {
                if (entrada.localProduto != null)
                {
                    if (LocalProdutoRules.LocalProdutoCreate(entrada, out modelo, contexto))
                    {
                        modeloData.Add(modelo);
                        return RedirectToAction("FormUpdateLocalProduto", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 36, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateLocalProduto-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateLocalProduto(string id)
        {
            LocalProdutoViewModel modelo = new LocalProdutoViewModel();
            modelo.localProduto = new LocalProduto();
            modelo.localProduto.id = new Guid(id);

            LocalProduto retorno = new LocalProduto();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.localProduto.id);

                if (retorno != null)
                {
                    modelo.localProduto = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateLocalProduto(LocalProdutoViewModel entrada)
        {
            LocalProduto modelo = new LocalProduto();
            entrada.contexto = this.contexto;
            try
            {
                if (LocalProdutoRules.LocalProdutoUpdate(entrada, out modelo))
                {
                    modeloData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateLocalProduto", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 36, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateLocalProduto-post", ex.Message);
            }

            return RedirectToAction("FormUpdateLocalProduto", new { id = modelo.id.ToString() });
        }

        public ViewResult GridLocalProduto(string filtro, int Page)
        {
            IEnumerable<LocalProduto> retorno = modeloData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where u.nome.ToLower().Contains(filtro.ToLower())
                          select u;
            }
            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<LocalProduto>(Page, 20));
        }
    }
}