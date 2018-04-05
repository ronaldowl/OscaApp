using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using OscaApp.ViewModels.GridViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using OscaFramework.Models;


namespace OscaApp.Controllers
{
    public class ItemListaPrecoController : Controller
    {

        private readonly IProdutoData produtoData;
        private readonly IListaPrecoData listaprecoData;
        private readonly IItemListaPrecoData ItemlistaPrecoData;
        private ContextPage contexto;

        public ItemListaPrecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);
            this.listaprecoData = new ListaPrecoData(db);
            this.ItemlistaPrecoData = new ItemListaPrecoData(db);

            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateItemListaPreco(string idProduto)
        {
            ItemListaPrecoViewModel modelo = new ItemListaPrecoViewModel();

            try
            {
                modelo.contexto = contexto;
                modelo.produto = produtoData.GetRelacao(new Guid(idProduto));

                modelo.itemlistaPreco.criadoEm = DateTime.Now;
                modelo.itemlistaPreco.criadoPorName = contexto.nomeUsuario;

                //Prenche lista de preço para o contexto da página
                List<SelectListItem> itens = new List<SelectListItem>();
                itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));
                modelo.listaPrecos = itens;

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateItemListaPreco-get", ex.Message);
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateItemListaPreco(ItemListaPrecoViewModel entrada)
        {
            ItemListaPreco itemlistaPreco = new ItemListaPreco();
            try
            {
                if (entrada.itemlistaPreco != null)
                {
                    if (ItemListaPrecoRules.ItemListaPrecoCreate(entrada, out itemlistaPreco, contexto))
                    {
                        ItemlistaPrecoData.Add(itemlistaPreco);
                        return RedirectToAction("FormUpdateItemListaPreco", new { id = itemlistaPreco.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateItemListaPreco-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateItemListaPreco(string id)
        {
            ItemListaPrecoViewModel modelo = new ItemListaPrecoViewModel();

            try
            {
                ItemListaPreco retorno = new ItemListaPreco();
                modelo.contexto = this.contexto;

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = ItemlistaPrecoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.itemlistaPreco = retorno;

                        //Prenche lista de preço para o contexto da página
                        List<SelectListItem> itens = new List<SelectListItem>();
                        itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));
                        modelo.listaPrecos = itens;
                        //Preenche produto
                        modelo.produto = produtoData.GetRelacao(modelo.itemlistaPreco.idProduto);
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateItemListaPreco-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateItemListaPreco(ItemListaPrecoViewModel entrada)
        {
            ItemListaPreco itemlistaPreco = new ItemListaPreco();
            entrada.contexto = this.contexto;

            try
            {
                if (ItemListaPrecoRules.ItemListaPrecoUpdate(entrada, out itemlistaPreco))
                {
                    ItemlistaPrecoData.Update(itemlistaPreco);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateItemListaPreco", new { id = itemlistaPreco.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateItemListaPreco-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridItemListaPreco(string idProduto, int Page, string Filtro)
        {
            IEnumerable<LookupItemLista> modelo = ItemlistaPrecoData.GetAllByIdProduto(new Guid(idProduto));

            if (!String.IsNullOrEmpty(Filtro))
            {
                modelo = from u in modelo
                         where
                            (u.produto.codigo.StartsWith(Filtro, StringComparison.InvariantCultureIgnoreCase))
                            || (u.produto.nome.StartsWith(Filtro, StringComparison.InvariantCultureIgnoreCase))
                          select u;
            }


            modelo = modelo.OrderBy(x => x.produto.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(modelo.ToPagedList<LookupItemLista>(Page, 10));

        }
          

        public ViewResult LookupProdutoOrdem(string idListaPreco, int Page, string Filtro)
        {
            
            return View( );

        }
        public PartialViewResult GridLookupProdutoOrdem(string idListaPreco, int Page, string Filtro)
        {
            IEnumerable<LookupItemLista> retorno = ItemlistaPrecoData.GetAllByListaPreco(new Guid(idListaPreco));

            if (!String.IsNullOrEmpty(Filtro))
            {
                retorno = from u in retorno
                          where
                            (u.produto.codigo.StartsWith(Filtro, StringComparison.InvariantCultureIgnoreCase))
                            || (u.produto.nome.StartsWith(Filtro, StringComparison.InvariantCultureIgnoreCase))
                          select u;
            }
            retorno = retorno.OrderBy(x => x.produto.nome);
  
            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return PartialView(retorno.ToPagedList<LookupItemLista>(Page, 5));

        }
        public ViewResult LookupProdutoPedido(string idListaPreco, int Page, string Filtro)
        {

            return View();

        }
        public PartialViewResult GridLookupProdutoPedido(string idListaPreco, int Page, string Filtro)
        {
            IEnumerable<LookupItemLista> retorno = ItemlistaPrecoData.GetAllByListaPreco(new Guid(idListaPreco));


            if (!String.IsNullOrEmpty(Filtro))
            {
                retorno = from u in retorno
                          where
                            (u.produto.codigo.StartsWith(Filtro,StringComparison.InvariantCultureIgnoreCase))
                            || (u.produto.nome.StartsWith(Filtro, StringComparison.InvariantCultureIgnoreCase))
                          select u;
            }
            retorno = retorno.OrderBy(x => x.produto.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return PartialView(retorno.ToPagedList<LookupItemLista>(Page, 5));

        }
    }
}
