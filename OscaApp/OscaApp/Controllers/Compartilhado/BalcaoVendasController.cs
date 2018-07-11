using System;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using OscaApp.RulesServices;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using OscaApp.framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class BalcaoVendasController : Controller
    {        
        private readonly IBalcaoVendasData balcaoVendasData;    
        private readonly IListaPrecoData listaprecoData;
        private readonly SqlGenericData Sqlservice;
        private readonly SqlGeneric sqlGeneric;


        private ContextPage contexto;

        public BalcaoVendasController(ContexDataService db, IHttpContextAccessor httpContext)
        {
             
            this.balcaoVendasData = new BalcaoVendasData(db);        
            this.listaprecoData = new ListaPrecoData(db);
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.Sqlservice = new SqlGenericData();
            this.sqlGeneric = new SqlGeneric();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult BalcaoVendasView(string id)
        {
            BalcaoVendasViewModel modelo = new BalcaoVendasViewModel();

            try
            {
                modelo.contexto = this.contexto;
                modelo.balcaoVendas = balcaoVendasData.Get(new Guid(id));
                modelo.listapreco = Sqlservice.RetornaRelacaoListaPreco(modelo.balcaoVendas.idListaPreco);
                modelo.produtosBalcao = sqlGeneric.RetornaProdutoBalcaoByBalcao(new Guid(id));
            }
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "BalcaoVendasView-get", ex.Message);
            }

            return View(modelo);
        }


        [HttpGet]
        public ViewResult BalcaoVendas(string idCliente)
        {
            BalcaoVendasViewModel modelo = new BalcaoVendasViewModel();

            try
            {
                modelo.contexto = contexto;
                modelo.balcaoVendas.criadoEm = DateTime.Now;
                modelo.balcaoVendas.criadoPorName = contexto.nomeUsuario;
          
                //Prenche lista de preço para o contexto da página
                List<SelectListItem> itens = new List<SelectListItem>();
                itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao) );
                modelo.listaPrecos = itens;

            }
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedido-get", ex.Message);
            }
          
            return View(modelo);
        }
            
        public ViewResult GridBalcaoVendas(string filtro, int Page, string idCliente, int view)
        {
            try
            {                
                IEnumerable <BalcaoVendasGridViewModel> retorno ;

                ViewBag.viewContexto = view;

             
                    retorno = balcaoVendasData.GetAllGridViewModel(contexto.idOrganizacao);
                             
 
                if (Page == 0) Page = 1;
                return View(retorno.ToPagedList<BalcaoVendasGridViewModel>(Page, 100));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,4, this.contexto.idUsuario, this.contexto.idOrganizacao, "GridPedido", ex.Message);
            }

            return View();
        }


        [HttpPost]
        public IActionResult FormStatusBalcaoVendas(BalcaoVendasViewModel entrada)
        {          
            try
            {
                if (entrada != null)
                {
                    entrada.balcaoVendas.statusBalcaoVendas = CustomEnumStatus.StatusBalcaoVendas.Cancelado;
                    entrada.balcaoVendas.modificadoEm = DateTime.Now;
                    entrada.balcaoVendas.modificadoPor = contexto.idUsuario;
                    entrada.balcaoVendas.modificadoPorName = contexto.nomeUsuario;

                    balcaoVendasData.UpdateStatus(entrada.balcaoVendas);

                    SqlGenericRules sqlGenericRules = new SqlGenericRules();
                    sqlGenericRules.CancelaFaturamentoBalcao(entrada.balcaoVendas.id.ToString());

                    return RedirectToAction("BalcaoVendasView", new { id = entrada.balcaoVendas.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusPedido-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormStatusBalcaoVendas(string id)
        {
            BalcaoVendasViewModel modelo = new BalcaoVendasViewModel();
            modelo.contexto = this.contexto;

            try
            {
                BalcaoVendas retorno = new BalcaoVendas();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = balcaoVendasData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.balcaoVendas = retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusBalcaoVendas-get", ex.Message);
            }
            return View(modelo);
        }

    }
}
