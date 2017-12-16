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
using System.Threading.Tasks;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {

        private readonly ProdutoData produtoData;
        private ContextPage contexto;
       


        public ProdutoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));

        }

    
        [HttpPost]
        public IActionResult FormUpdateProduto(ProdutoViewModel entrada)
        {
            Produto modelo = new Produto();
            entrada.contexto = this.contexto;

            try
            {
                if (ProdutoRules.MontaProdutoUpdate(entrada, out modelo))
                {
                    produtoData.Update(modelo);
                    return RedirectToAction("FormUpdateProduto", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }

            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }

            return RedirectToAction("FormUpdateProduto", new { id = modelo.id.ToString() });
        }

        [HttpGet]
        public ViewResult FormUpdateProduto(string id)
        {
            ProdutoViewModel modelo = new ProdutoViewModel();
            modelo.produto = new Produto();
            modelo.produto.id = new Guid(id);

            Produto retorno = new Produto();
            //Formulario com os dados do cliente
            if (!String.IsNullOrEmpty(id))
            {
             
                retorno = produtoData.Get(modelo.produto.id, contexto.idOrganizacao);


                //TODO Formata campos

                if (retorno != null)
                {
                    modelo.produto = retorno;                   
                }
            }

            return View(modelo);
        }

        [HttpGet]
        public ViewResult FormCreateProduto()
        {

            ProdutoViewModel modelo = new ProdutoViewModel();
            modelo.produto = new Produto();
            modelo.contexto = contexto;
            modelo.produto.criadoEm = DateTime.Now;
            modelo.produto.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateProduto(ProdutoViewModel entrada)
        {

            Produto prod = new Produto();

            try
            {
                if (entrada.produto.nome != null)
                {
                    if (ProdutoRules.MontaProdutoCreate(entrada, out prod, contexto))
                    {
                        produtoData.Add(prod);

                        return RedirectToAction("FormUpdateProduto", new { id = prod.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }
            return View();
        }

        public ViewResult GridProduto(string filtro, int Page)
        {
            IEnumerable<Produto> retorno = produtoData.GetAll(contexto.idOrganizacao);

           
            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.codigo == filtro || A.nome == filtro || A.codigo == filtro || A.codigoFabricante == filtro) select A;

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Produto>(Page, 10));
        }
    }
}
