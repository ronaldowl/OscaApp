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

        [HttpPost]
        public IActionResult FormCreateServicoOrdem(ServicoOrdemViewModel entrada)
        {
            ServicoOrdem modelo = new ServicoOrdem();
            try
            {
                if (ServicoOrdemRules.ServicoOrdemCreate(entrada, out modelo, contexto))
                {
                    SqlGenericData sqlData = new SqlGenericData();


                    servicoOrdemData.Add(modelo);
                    return RedirectToAction("FormUpdateOrdemServico", new { idOrdem = sqlData.RetornaRelacaoOrdemServicoPorIDServicoOrdem(modelo.id).id });

                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateServicoOrdem-post", ex.Message);
            }
            return View();
        }


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

        [HttpGet]
        public IActionResult FormUpdateServicoOrdem(string idServicoOrdem)
        {
            ServicoOrdemViewModel modelo = new ServicoOrdemViewModel();
            SqlGenericData sqlData = new SqlGenericData();

            try
            {
                modelo.servicoOrdem = servicoOrdemData.Get(new Guid(idServicoOrdem));
                modelo.servico = new Relacao();
                modelo.servico = sqlData.RetornaRelacaoServico(modelo.servico.id);
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoPedido-get", ex.Message);
            }
            return View(modelo);
        }

        public ViewResult GridServicoOrdem(string idOrdem)
        {
            IEnumerable<ServicoOrdem> retorno = servicoOrdemData.GetByServicoOrdemId(new Guid(idOrdem));

            return View(retorno.ToPagedList<ServicoOrdem>(1, 10));
        }

        //public IActionResult DeleteProdutoPedido(string idProdutoPedido, string idPedido)
        //{
        //    ProdutoPedido modelo = new ProdutoPedido();
        //    modelo.id = new Guid(idProdutoPedido);
        //    produtoPedidoData.Delete(modelo);
        //    return RedirectToAction("GridProdutoPedido", new { idPedido = idPedido });
        //}
    }
}