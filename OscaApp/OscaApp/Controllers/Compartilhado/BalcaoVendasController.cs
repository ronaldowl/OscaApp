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
        private readonly IClienteData clienteData;
        private readonly IOrganizacaoData organizacaoData;
        private readonly IOrgConfigData orgConfigData;
        private readonly IListaPrecoData listaprecoData;
        private readonly IProdutoData produtoData;
        private readonly SqlGenericData Sqlservice;
        private readonly SqlGeneric sqlGeneric;


        private ContextPage contexto;

        public BalcaoVendasController(ContexDataService db, IHttpContextAccessor httpContext)
        {

            this.balcaoVendasData = new BalcaoVendasData(db);
            this.listaprecoData = new ListaPrecoData(db);
            this.clienteData = new ClienteData(db);
            this.organizacaoData = new OrganizacaoData(db);
            this.orgConfigData = new OrgConfigData(db);
            this.produtoData = new ProdutoData(db);
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
            BalcaoVendas retorno = new BalcaoVendas();
            try
            {
                modelo.contexto = this.contexto;
                retorno = balcaoVendasData.Get(new Guid(id));
                modelo.balcaoVendas = retorno;
                modelo.listapreco = Sqlservice.RetornaRelacaoListaPreco(modelo.balcaoVendas.idListaPreco);
                modelo.produtosBalcao = sqlGeneric.RetornaProdutoBalcaoByBalcao(new Guid(id));

                if (retorno.idCliente != null) modelo.cliente = Sqlservice.RetornaRelacaoCliente(retorno.idCliente);

            }
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "BalcaoVendasView-get", ex.Message);
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult BalcaoVendasView(BalcaoVendasViewModel entrada)
        {

            BalcaoVendas modelo = new BalcaoVendas();
            entrada.contexto = this.contexto;
            modelo = entrada.balcaoVendas;

            if (entrada.cliente.id != Guid.Empty) modelo.idCliente = entrada.cliente.id;

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = contexto.idUsuario;
            modelo.modificadoPorName = contexto.nomeUsuario;

            try
            {
                balcaoVendasData.Update(modelo);                                   
                StatusMessage = "Registro Atualizado com Sucesso!";            
              
            } 
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 11, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePerfilAcesso-post", ex.Message);
            }

            return RedirectToAction("BalcaoVendasView", new { id = modelo.id.ToString() });

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
                itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));
                modelo.listaPrecos = itens;

                //Se passar o id carrega o cliente
                if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = Sqlservice.RetornaRelacaoCliente(new Guid(idCliente));

            }
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedido-get", ex.Message);
            }

            return View(modelo);
        }

        public ViewResult GridBalcaoVendas(string filtro, int Page, int view, DateTime inicio, DateTime fim)
        {
            try
            {
                IEnumerable<BalcaoVendasGridViewModel> retorno;

                if (inicio.Year > 1800)
                {
                    view = 3;
                    ViewBag.viewContexto = view;
                }
                else { ViewBag.viewContexto = view; }




                if (!String.IsNullOrEmpty(filtro))
                {
                    retorno = balcaoVendasData.GetByCodigo(contexto.idOrganizacao, filtro);

                    if (Page == 0) Page = 1;
                    return View(retorno.ToPagedList<BalcaoVendasGridViewModel>(Page, 50));

                }

                retorno = balcaoVendasData.GetAll(contexto.idOrganizacao, view, inicio, fim);

                if (Page == 0) Page = 1;
                return View(retorno.ToPagedList<BalcaoVendasGridViewModel>(Page, 50));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "GridPedido", ex.Message);
            }

            return View();
        }

        public ViewResult GridClienteBalcaoVendas(string idCliente)
        {
            SqlGenericData genericData = new SqlGenericData();
            Guid id = new Guid(idCliente);
            try
            {
                List<BalcaoVendas> retorno;

                ViewBag.Cliente = genericData.RetornaCliente(id);

                retorno = balcaoVendasData.GetAllByIdCliente(id);

                return View(retorno);

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "GridPedido", ex.Message);
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
                    entrada.balcaoVendas.modificadoEm = DateTime.Now;
                    entrada.balcaoVendas.modificadoPor = contexto.idUsuario;
                    entrada.balcaoVendas.modificadoPorName = contexto.nomeUsuario;

                    balcaoVendasData.UpdateStatus(entrada.balcaoVendas);

                    SqlGenericRules sqlGenericRules = new SqlGenericRules();

                    if (entrada.balcaoVendas.statusBalcaoVendas == CustomEnumStatus.StatusBalcaoVendas.Cancelado)
                    {
                        sqlGenericRules.CancelaFaturamentoBalcao(entrada.balcaoVendas.id.ToString());
                    }

                    List<ProdutoBalcao> produtosBalcao = sqlGeneric.RetornaProdutoBalcaoByBalcao(entrada.balcaoVendas.id);

                    ProdutoRules.RollbackProdutoBalcao(produtosBalcao.ToArray(), contexto, produtoData);

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
        [HttpGet]
        public ViewResult ImpressaoCupom(string id)

        {
            ImpressaoCupomViewModel modelo = new ImpressaoCupomViewModel();

            modelo.balcaoVendas = new BalcaoVendas();
            modelo.balcaoVendas.id = new Guid(id);
            modelo.cliente = new Cliente();
            modelo.produto = new Produto();
            modelo.orgConfig = new OrgConfig();
            modelo.organizacao = new Organizacao();

            BalcaoVendas retorno = new BalcaoVendas();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = balcaoVendasData.Get(modelo.balcaoVendas.id);
                if (retorno.idCliente != Guid.Empty) modelo.cliente = clienteData.Get(retorno.idCliente);
                modelo.orgConfig = orgConfigData.Get(contexto.idOrganizacao);
                modelo.produtosBalcao = sqlGeneric.RetornaProdutoBalcaoByBalcao(new Guid(id));
                modelo.organizacao = organizacaoData.Get(contexto.idOrganizacao);
                //modelo.produto = produtoData.Get(retorno.produto.id,contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.balcaoVendas = retorno;
                }
            }
            return View(modelo);
        }


        public ViewResult LookupBalcaoVendas()
        {
            return View();
        }

        public ViewResult GridLookupBalcaoVendas(string filtro, int Page)
        {
            try
            {
                IEnumerable<BalcaoVendasGridViewModel> retorno = balcaoVendasData.GetAll(contexto.idOrganizacao, 4, DateTime.Now, DateTime.Now);

                if (!String.IsNullOrEmpty(filtro))
                {
                    retorno = from u in retorno
                              where (u.balcaoVendas.codigo.ToLower().Contains(filtro.ToLower())) select u;

                }   
                //Se não passar a número da página, caregar a primeira
                if (Page == 0) Page = 1;

                return View(retorno.ToPagedList<BalcaoVendasGridViewModel>(Page, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }

    }
}
