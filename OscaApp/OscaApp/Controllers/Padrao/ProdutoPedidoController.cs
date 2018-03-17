using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    public class ProdutoPedidoController : Controller
    {

        private readonly IProdutoPedidoData produtoPedidoData;
        private readonly IProdutoData produtoData;
        private readonly IItemListaPrecoData ItemlistaPrecoData;
        private ContextPage contexto;

        public ProdutoPedidoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);
            this.produtoPedidoData = new ProdutoPedidoData(db);
            this.ItemlistaPrecoData = new ItemListaPrecoData(db);
        

            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateProdutoPedido(string idPedido, string idListaPreco)
        {
            ProdutoPedidoViewModel modelo = new ProdutoPedidoViewModel();
           
            try
            {
                modelo.contexto = contexto;
                modelo.produtoPedido = new ProdutoPedido();
                modelo.produtoPedido.idPedido = new Guid(idPedido);
            
                modelo.produtoPedido.idListaPreco = new Guid(idListaPreco);               
                modelo.produtoPedido.criadoEm = DateTime.Now;
                modelo.produtoPedido.criadoPorName = contexto.nomeUsuario;
                IEnumerable<LookupItemLista> produtos = ItemlistaPrecoData.GetAllByListaPreco(new Guid(idListaPreco));

                modelo.produtos = produtos.ToPagedList<LookupItemLista>(1, 5);


            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoPedido-get", ex.Message);
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateProdutoPedido(ProdutoPedidoViewModel entrada)
        {
            ProdutoPedido modelo = new ProdutoPedido();
            try
            {
                if (ProdutoPedidoRules.MontaProdutoPedidoCreate(entrada, out modelo, contexto))
                {
                    produtoPedidoData.Add(modelo);
                    return RedirectToAction("FormUpdatePedido", "Pedido", new { id = modelo.idPedido.ToString()});
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoPedido-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult FormUpdateProdutoPedido(string idProdutoPedido)
        {
            ProdutoPedidoViewModel modelo = new ProdutoPedidoViewModel();
            SqlGenericData sqlData = new SqlGenericData();

            try
            {
                modelo.produtoPedido = produtoPedidoData.Get(new Guid(idProdutoPedido));
                modelo.produto = new Relacao();
                modelo.produto = sqlData.RetornaRelacaoProduto(modelo.produtoPedido.idProduto);
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateProdutoPedido(ProdutoPedidoViewModel entrada)
        {
            ProdutoPedido modelo = new ProdutoPedido();
            entrada.contexto = this.contexto;

            try
            {
                if (ProdutoPedidoRules.MontaProdutoPedidoUpdate(entrada, out modelo))
                {
                    produtoPedidoData.Update(modelo);
                    SqlGenericData sqlData = new SqlGenericData();

                    return RedirectToAction("FormUpdatePedido", "Pedido", new { id = entrada.produtoPedido.idPedido.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridProdutoPedido(string idPedido )
        {
            IEnumerable<ProdutoPedidoGridViewModel> retorno = produtoPedidoData.GetAllGridViewModel(new Guid(idPedido));                     

            return View(retorno.ToPagedList<ProdutoPedidoGridViewModel>(1, 10));
        }

        public IActionResult DeleteProdutoPedido(string idProdutoPedido, string idPedido)
        {
            ProdutoPedido modelo = new ProdutoPedido();
            modelo.id = new Guid(idProdutoPedido);
            produtoPedidoData.Delete(modelo);
            return RedirectToAction("GridProdutoPedido", new { idPedido = idPedido });
        }
    }
}