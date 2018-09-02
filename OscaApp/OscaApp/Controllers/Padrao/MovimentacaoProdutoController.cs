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
    public class MovimentacaoProdutoController : Controller
    {
        private readonly IMovimentacaoProdutoData modeloData;
        private ContextPage contexto;

        public MovimentacaoProdutoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new MovimentacaoProdutoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateMovimentacaoProduto()
        {
            MovimentacaoProdutoViewModel modelo = new MovimentacaoProdutoViewModel();
            modelo.movimentacaoProduto = new MovimentacaoProduto();
            modelo.contexto = contexto;
            modelo.movimentacaoProduto.criadoEm = DateTime.Now;
            modelo.movimentacaoProduto.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateMovimentacaoProduto(MovimentacaoProdutoViewModel entrada)
        {
            MovimentacaoProduto modelo = new MovimentacaoProduto();

            try
            {
                if (entrada.movimentacaoProduto != null)
                {
                    if (MovimentacaoProdutoRules.MovimentacaoProdutoCreate(entrada, out modelo, contexto))
                    {
                        modeloData.Add(modelo);
                        return RedirectToAction("FormUpdateMovimentacaoProduto", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 37, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateMovimentacaoProduto-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateMovimentacaoProduto(string id)
        {
            MovimentacaoProdutoViewModel modelo = new MovimentacaoProdutoViewModel();
            modelo.movimentacaoProduto = new MovimentacaoProduto();
            modelo.movimentacaoProduto.id = new Guid(id);

            MovimentacaoProduto retorno = new MovimentacaoProduto();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.movimentacaoProduto.id);

                if (retorno != null)
                {
                    modelo.movimentacaoProduto = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateMovimentacaoProduto(MovimentacaoProdutoViewModel entrada)
        {
            MovimentacaoProduto modelo = new MovimentacaoProduto();
            entrada.contexto = this.contexto;
            try
            {
                if (MovimentacaoProdutoRules.MovimentacaoProdutoUpdate(entrada, out modelo))
                {
                    modeloData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateMovimentacaoProduto", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 37, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateMovimentacaoProduto-post", ex.Message);
            }

            return RedirectToAction("FormUpdateMovimentacaoProduto", new { id = modelo.id.ToString() });
        }

        public ViewResult GridMovimentacaoProduto(string filtro, int Page)
        {
            IEnumerable<MovimentacaoProduto> retorno = modeloData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where u.nome.ToLower().Contains(filtro.ToLower())
                          select u;
            }
            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<MovimentacaoProduto>(Page, 20));
        }
    }
}