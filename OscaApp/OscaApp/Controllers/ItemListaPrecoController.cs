using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.Data;
using OscaApp.framework;
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
        private readonly IListaPrecoData listaprecoData;
        private ContextPage contexto;

        public ItemListaPrecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);
            this.listaprecoData = new ListaPrecoData(db);
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
        }

        [HttpGet]
        public ViewResult FormCreateItemListaPreco(string idProduto)
        {
            ItemListaPrecoViewModel modelo = new ItemListaPrecoViewModel();
           
            try
            {
                modelo.contexto = contexto;             
                modelo.produto = produtoData.GetRelacao(new Guid(idProduto));

                //Prenche lista de preço para o contexto da página
                List<SelectListItem> itens = new List<SelectListItem>();
                itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));

                modelo.listaPrecos = itens;

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateItemListaPreco-get", ex.Message);
            }

            return View(modelo);
        }
    }
}
