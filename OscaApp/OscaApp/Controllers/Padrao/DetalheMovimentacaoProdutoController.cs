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
    public class DetalheMovimentacaoProdutoController : Controller
    {
        private readonly IDetalheMovimentacaoProdutoData modeloData;
        private ContextPage contexto;

        public DetalheMovimentacaoProdutoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new DetalheMovimentacaoProdutoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateDetalheMovimentacaoProduto()
        {
            DetalheMovimentacaoProdutoViewModel modelo = new DetalheMovimentacaoProdutoViewModel();
            modelo.detalheMovimentacaoProduto = new DetalheMovimentacaoProduto();
            modelo.contexto = contexto;
            modelo.detalheMovimentacaoProduto.criadoEm = DateTime.Now;
            modelo.detalheMovimentacaoProduto.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateDetalheMovimentacaoProduto(DetalheMovimentacaoProdutoViewModel entrada)
        {
            DetalheMovimentacaoProduto modelo = new DetalheMovimentacaoProduto();

            try
            {
                if (entrada.detalheMovimentacaoProduto != null)
                {
                    if (DetalheMovimentacaoProdutoRules.DetalheMovimentacaoProdutoCreate(entrada, out modelo, contexto))
                    {
                        modeloData.Add(modelo);
                        return RedirectToAction("FormUpdateDetalheMovimentacaoProduto", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 38, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateDetalheMovimentacaoProduto-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateDetalheMovimentacaoProduto(string id)
        {
            DetalheMovimentacaoProdutoViewModel modelo = new DetalheMovimentacaoProdutoViewModel();
            modelo.detalheMovimentacaoProduto = new DetalheMovimentacaoProduto();
            modelo.detalheMovimentacaoProduto.id = new Guid(id);

            DetalheMovimentacaoProduto retorno = new DetalheMovimentacaoProduto();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.detalheMovimentacaoProduto.id);

                if (retorno != null)
                {
                    modelo.detalheMovimentacaoProduto = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateDetalheMovimentacaoProduto(DetalheMovimentacaoProdutoViewModel entrada)
        {
            DetalheMovimentacaoProduto modelo = new DetalheMovimentacaoProduto();
            entrada.contexto = this.contexto;
            try
            {
                if (DetalheMovimentacaoProdutoRules.DetalheMovimentacaoProdutoUpdate(entrada, out modelo))
                {
                    modeloData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateDetalheMovimentacaoProduto", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 38, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateDetalheMovimentacaoProduto-post", ex.Message);
            }

            return RedirectToAction("FormUpdateDetalheMovimentacaoProduto", new { id = modelo.id.ToString() });
        }

        public ViewResult GridDetalheMovimentacaoProduto(string filtro, int Page)
        {
            IEnumerable<DetalheMovimentacaoProduto> retorno = modeloData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where u.nome.ToLower().Contains(filtro.ToLower())
                          select u;
            }
            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<DetalheMovimentacaoProduto>(Page, 20));
        }
    }
}