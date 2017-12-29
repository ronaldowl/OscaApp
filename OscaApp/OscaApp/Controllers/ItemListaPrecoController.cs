using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Controllers
{
    public class ItemListaPrecoController : Controller
    {

        private readonly IProdutoData produtoData;
        private ContextPage contexto;

        public ItemListaPrecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {

            this.produtoData = new ProdutoData(db);

            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));

        }

        [HttpGet]
        public ViewResult FormCreateItemListaPreco(string idProduto)
        {
            ItemListaPrecoViewModel modelo = new ItemListaPrecoViewModel();
            modelo.itemlistaPreco = new ItemListaPreco();
            try
            {
                modelo.contexto = contexto;
                modelo.produto.id = new Guid(idProduto);
                modelo.produto = produtoData.GetRelacao(new Guid(idProduto));

            }
            catch (Exception)
            {

                throw;
            }

            return View(modelo);
        }
    }
}
