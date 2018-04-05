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
using X.PagedList;
using OscaFramework.Models;
using OscaFramework.MicroServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Controllers
{
    [Authorize]
    public class OrdemServicoController : Controller
    {
        private readonly IOrdemServicoData ordemServicoData;
        private readonly IServicoOrdemData servicoOrdemData;
        private readonly IProdutoOrdemData produtoOrdemData;
      
        private ContextPage contexto;
        private readonly SqlGenericData sqlData;
        private readonly IListaPrecoData listaprecoData;

        public OrdemServicoController(SqlGenericData _sqlData, ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.sqlData = _sqlData;
            this.ordemServicoData = new OrdemServicoData(db);
            this.servicoOrdemData = new ServicoOrdemData(db);
            this.listaprecoData = new ListaPrecoData(db);
            this.produtoOrdemData = new ProdutoOrdemData(db);

            this.contexto =  new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateOrdemServico(string idCliente)
        {
            OrdemServicoViewModel modelo = new OrdemServicoViewModel();

            //Se passar o id carrega o regitro relacionado, usado sempre em telas com lookup
            if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = sqlData.RetornaRelacaoCliente(new Guid(idCliente));
                        
            modelo.contexto = contexto;
            modelo.ordemServico.criadoEm = DateTime.Now;
            modelo.ordemServico.criadoPorName = contexto.nomeUsuario;
  

            //Prenche lista de preço para o contexto da página
            List<SelectListItem> itens = new List<SelectListItem>();
            itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));
            modelo.listasPreco = itens;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateOrdemServico(OrdemServicoViewModel entrada)
        {
            OrdemServico modelo = new OrdemServico();
            entrada.contexto = this.contexto;
            try
            {
                if (entrada.ordemServico != null)
                {
                  if  (OrdemServicoRules.OrdemServicoCreate(entrada, out modelo)) {
                        ordemServicoData.Add(modelo);
                        return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 5, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateOrdemServico-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateOrdemServico(string id)
        {
            OrdemServicoViewModel modelo = new OrdemServicoViewModel();  
            modelo.ordemServico.id = new Guid(id);           
       
            if (!String.IsNullOrEmpty(id))
            {

                modelo.ordemServico = ordemServicoData.Get(modelo.ordemServico.id );
                modelo.contexto = this.contexto;
                SqlGeneric sqlServices = new SqlGeneric();

                if (modelo.ordemServico != null)
                {                   
                    modelo.cliente = sqlData.RetornaRelacaoCliente(modelo.ordemServico.idCliente);
                    modelo.listaPreco = sqlData.RetornaRelacaoListaPreco(modelo.ordemServico.idListaPreco);

                    //Prenche lista de preço para o contexto da página
                    List<SelectListItem> itens = new List<SelectListItem>();
                    itens = HelperAttributes.PreencheDropDownList(sqlServices.RetornaRelacaoCategoriaManutencao());
                    modelo.categorias = itens;
                
                    if (modelo.ordemServico.idProfissional != null) modelo.profissional = sqlData.RetornaRelacaoProfissional(modelo.ordemServico.idProfissional);

                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateOrdemServico(OrdemServicoViewModel entrada)
        {
            OrdemServico modelo = new OrdemServico();
            entrada.contexto = this.contexto;
            try
            {
                if (OrdemServicoRules.OrdemServicoUpdate(entrada, out modelo))
                {
                    OrdemServicoRules.CalculoOrdem(ref modelo, servicoOrdemData, produtoOrdemData);
                    ordemServicoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 5, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateOrdemServico-post", ex.Message);
            }

            return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString() });
        }

        public ViewResult GridOrdemServico(string filtro, int Page, string idCliente)
        {
            IEnumerable<OrdemServicoGridViewModel> retorno;

            if (String.IsNullOrEmpty(idCliente)) {

                retorno = ordemServicoData.GetAllGridViewModel(contexto.idOrganizacao);
            }
            else
            {
                retorno = ordemServicoData.GetAllGridViewModelByCliente(new Guid(idCliente));

            }

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                 (u.cliente.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.ordemServico.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.cliente.idName.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))


                          select u;
            }

            retorno = retorno.OrderBy(x => x.ordemServico.codigo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<OrdemServicoGridViewModel>(Page, 10));
        }

        public ViewResult GridLookupOrdemServico(string filtro, int Page)
        {
            IEnumerable<OrdemServicoGridViewModel> retorno = ordemServicoData.GetAllGridViewModel(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                                (u.cliente.codigo.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))||
                                (u.ordemServico.codigo.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))||
                                (u.cliente.idName.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))

                          select u;
            }
            retorno = retorno.OrderBy(x => x.ordemServico.codigo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<OrdemServicoGridViewModel>(Page, 10));
        }

        public ViewResult LookupOrdemServico()
        {
            IEnumerable<OrdemServico> retorno = ordemServicoData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.codigo); 

            return View(retorno.ToPagedList<OrdemServico>(1,10));
        }

        [HttpGet]
        public ViewResult FormStatusOrdemServico(string id)
        {
            OrdemServicoViewModel modelo = new OrdemServicoViewModel();
            modelo.contexto = this.contexto;
            try
            {
                OrdemServico retorno = new OrdemServico();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = ordemServicoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.ordemServico = retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 5, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusOrdemServico-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormStatusOrdemServico(OrdemServicoViewModel entrada)
        {
            OrdemServico modelo = new OrdemServico();
            entrada.contexto = this.contexto;

            try
            {
                if (OrdemServicoRules.OrdemServicoUpdateStatus(entrada, out modelo))
                { 
                    OrdemServicoRules.CalculoOrdem(ref modelo, servicoOrdemData, produtoOrdemData);
                    ordemServicoData.UpdateStatus(modelo);

                    return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 5, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusOrdemServico-post", ex.Message);
            }
            return View();
        }


    }
}
