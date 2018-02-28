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
    public class ProdutoOrdemController : Controller
    {

        private readonly IProdutoOrdemData produtoOrdemData;
        private readonly IProdutoData produtoData; 
        private ContextPage contexto;

        public ProdutoOrdemController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.produtoData = new ProdutoData(db);
            this.produtoOrdemData = new ProdutoOrdemData(db);     
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [HttpGet]
        public ViewResult FormCreateProdutoOrdem(string id,string idListaPreco )
        {
            ProdutoOrdemViewModel modelo = new ProdutoOrdemViewModel();
           
            try
            {
                modelo.contexto = contexto;
                modelo.produtoOrdem = new ProdutoOrdem();
                modelo.ordemServico.id = new Guid(id);
                modelo.listaPreco = new Relacao();
                modelo.listaPreco.id = new Guid(idListaPreco);              
                modelo.produtoOrdem.criadoEm = DateTime.Now;
                modelo.produtoOrdem.criadoPorName = contexto.nomeUsuario;    
                
          
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProdutoPedido-get", ex.Message);
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateProdutoOrdem(ProdutoOrdemViewModel entrada)
        {
            ProdutoOrdem modelo = new ProdutoOrdem();
            try
            {
                if (entrada.produtoOrdem != null)
                {
                    if (ProdutoOrdemRules.ProdutoOrdemCreate(entrada, out modelo, contexto))
                    {
                        SqlGenericData sqlData = new SqlGenericData();
                        produtoOrdemData.Add(modelo);
                        return RedirectToAction("GridProdutoOrdem", "ProdutoOrdem", new { id = sqlData.RetornaRelacaoOrdemServicoPorIDProdutoOrdem(modelo.id).id });

                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateServicoOrdem-get", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult FormUpdateProdutoOrdem(string id)
        {
            ProdutoOrdemViewModel modelo = new ProdutoOrdemViewModel();
            SqlGenericData sqlData = new SqlGenericData();

            try
            {
                modelo.produtoOrdem = produtoOrdemData.Get(new Guid(id));
                modelo.produto = new Relacao();
                modelo.ordemServico = new Relacao();
                modelo.ordemServico = sqlData.RetornaRelacaoOrdemServicoPorIDProdutoOrdem(new Guid(id));

                modelo.produto = sqlData.RetornaRelacaoProduto(modelo.produtoOrdem.idProduto);
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 13, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoOrdem-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateProdutoOrdem(ProdutoOrdemViewModel entrada)
        {
            ProdutoOrdem modelo = new ProdutoOrdem();             

            try
            {
                if (ProdutoOrdemRules.ProdutoOrdemUpdate(entrada, out modelo, this.contexto))
                {
                    produtoOrdemData.Update(modelo);                    
                    return RedirectToAction("GridProdutoOrdem", "ProdutoOrdem", new { id = entrada.ordemServico.id });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 16, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProdutoOrdem-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridProdutoOrdem(string id)
        {
            IEnumerable<ProdutoOrdemGridViewModel> retorno = produtoOrdemData.GetAllGridViewModel(new Guid(id));

            return View(retorno.ToPagedList<ProdutoOrdemGridViewModel>(1, 10));
        }

        public IActionResult DeleteProdutoOrdem(string id, string idOrdem)
        {
            ProdutoOrdem modelo = new ProdutoOrdem();
            modelo.id = new Guid(id);
            produtoOrdemData.Delete(modelo);
            return RedirectToAction("GridProdutoOrdem", new { id = idOrdem });
        }
    }
}