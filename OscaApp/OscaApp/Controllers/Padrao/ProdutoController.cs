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
using System.Threading.Tasks;
using X.PagedList;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {

        private readonly ProdutoData produtoData;
        private readonly ItemListaPrecoData itemListaPrecoData;
        private ContextPage contexto;
       


        public ProdutoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);
            this.itemListaPrecoData = new ItemListaPrecoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

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
            ItemListaPreco itemLista = new ItemListaPreco();
            SqlGenericData sqlService = new SqlGenericData();



            try
            {
                if (entrada.produto != null)
                {
                    if (ProdutoRules.MontaProdutoCreate(entrada, out prod, contexto))
                    {
                        produtoData.Add(prod);

                        //Create de item da lista
                        itemLista.idProduto = prod.id;
                        itemLista.idListaPreco = sqlService.RetornaRelacaoListaPrecoPadrao(contexto.idOrganizacao).id;
                        itemLista.valor = prod.valorCompra;
                        ItemListaPrecoRules.ItemListaPrecoCreateRelacionado(itemLista, contexto);
                        itemListaPrecoData.Add(itemLista);

                        return RedirectToAction("FormUpdateProduto", new { id = prod.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 7, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProduto-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateProduto(string id)
        {
            ProdutoViewModel modelo = new ProdutoViewModel();
            modelo.produto = new Produto();
            modelo.produto.id = new Guid(id);

            Produto retorno = new Produto();

            if (!String.IsNullOrEmpty(id))
            {

                retorno = produtoData.Get(modelo.produto.id, contexto.idOrganizacao);
                modelo.itensListaPreco = new List<ItemProdutoLista>();
                modelo.itensListaPreco = ProdutoRules.RetornaItemListaProduto(itemListaPrecoData.GetAllByProduto(modelo.produto.id));
                //apresenta mensagem de registro atualizado com sucesso
                modelo.StatusMessage = StatusMessage;

                if (retorno != null)
                {
                    modelo.produto = retorno;
                }
            }

            return View(modelo);
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
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateProduto", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 7, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProduto-post", ex.Message);
            }

            return RedirectToAction("FormUpdateProduto", new { id = modelo.id.ToString() });
        }

        public ViewResult GridProduto(string filtro, int Page)
        {
            IEnumerable<Produto> retorno = produtoData.GetAll(contexto.idOrganizacao);


            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.nome.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))  
                          select u;
            }

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Produto>(Page, 20));
        }
        public ViewResult LookupProduto(string filtro, int Page)
        {
            IEnumerable<Produto> retorno = produtoData.GetAll(contexto.idOrganizacao);


            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.nome.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))
                          select u;
            }

            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Produto>(Page, 20));
        }
    }
}
