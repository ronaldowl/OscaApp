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
 



namespace OscaApp.Controllers
{
    public class ServicoOrdemController : Controller
    {

        private readonly IServicoOrdemData servicoOrdemData;
        private readonly IServicoData servicoData;
        private readonly IItemListaPrecoData ItemlistaPrecoData;
        private ContextPage contexto;

        public ServicoOrdemController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.servicoData = new ServicoData(db);
            this.servicoOrdemData = new ServicoOrdemData(db);     
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateServicoOrdem(string idOrdem )
        {
            ServicoOrdemViewModel modelo = new ServicoOrdemViewModel();
           
            try
            {
                modelo.contexto = contexto;
                modelo.servicoOrdem = new ServicoOrdem();
                modelo.ordemServico.id = new Guid(idOrdem);                  
              
                modelo.servicoOrdem.criadoEm = DateTime.Now;
                modelo.servicoOrdem.criadoPorName = contexto.nomeUsuario;    
                
          
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoPedido-get", ex.Message);
            }

            return View(modelo);
        }

        //[HttpPost]
        //public IActionResult FormCreateProdutoPedido(ProdutoPedidoViewModel entrada)
        //{
        //    ProdutoPedido modelo = new ProdutoPedido();
        //    try
        //    {
        //        if (ProdutoPedidoRules.MontaProdutoPedidoCreate(entrada, out modelo, contexto))
        //        {
        //            produtoPedidoData.Add(modelo);
        //            return RedirectToAction("FormUpdatePedido","Pedido", new { id = modelo.idPedido.ToString()});
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogOsca log = new LogOsca();
        //        log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoPedido-post", ex.Message);
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult FormUpdateProdutoPedido(ProdutoPedidoViewModel entrada)
        //{
        //    ProdutoPedido modelo = new ProdutoPedido();
        //    entrada.contexto = this.contexto;

        //    try
        //    {
        //        if (ProdutoPedidoRules.MontaProdutoPedidoUpdate(entrada, out modelo))
        //        {
        //            produtoPedidoData.Update(modelo);
        //            SqlGenericData sqlData = new SqlGenericData();

        //            return RedirectToAction("GridProdutoPedido", new { idPedido = sqlData.RetornaRelacaoPedidoPorProdutoPedido(modelo.id).id });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogOsca log = new LogOsca();
        //        log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-post", ex.Message);
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult FormUpdateProdutoPedido(string idProdutoPedido)
        //{
        //    ProdutoPedidoViewModel modelo = new ProdutoPedidoViewModel();
        //    SqlGenericData sqlData = new SqlGenericData();

        //    try
        //    {
        //        modelo.produtoPedido = produtoPedidoData.Get(new Guid(idProdutoPedido));
        //        modelo.produto = new  Relacao();
        //        modelo.produto = sqlData.RetornaRelacaoProduto(modelo.produtoPedido.idProduto);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogOsca log = new LogOsca();
        //        log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-get", ex.Message);
        //    }
        //    return View(modelo);
        //}

        //public ViewResult GridProdutoPedido(string idPedido )
        //{
        //    IEnumerable<ProdutoPedido> retorno = produtoPedidoData.GetByPedidoId(new Guid(idPedido));                     

        //    return View(retorno.ToPagedList<ProdutoPedido>(1, 10));
        //}

        //public IActionResult DeleteProdutoPedido(string idProdutoPedido, string idPedido)
        //{
        //    ProdutoPedido modelo = new ProdutoPedido();
        //    modelo.id = new Guid(idProdutoPedido);
        //    produtoPedidoData.Delete(modelo);
        //    return RedirectToAction("GridProdutoPedido", new { idPedido = idPedido });
        //}
    }
}