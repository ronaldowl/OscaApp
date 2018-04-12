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
    public class PedidoController : Controller
    {        
        private readonly IPedidoData pedidoData;
        private readonly IProdutoPedidoData produtoPedidoData;
        private readonly IListaPrecoData listaprecoData;
        private readonly SqlGenericData Sqlservice;
        private ContextPage contexto;

        public PedidoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
             
            this.pedidoData = new PedidoData(db);
            this.produtoPedidoData = new ProdutoPedidoData(db);
            this.listaprecoData = new ListaPrecoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.Sqlservice = new SqlGenericData();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreatePedido(string idCliente)
        {
            PedidoViewModel modelo = new PedidoViewModel();

            try
            {
                modelo.contexto = contexto;
                modelo.pedido.criadoEm = DateTime.Now;
                modelo.pedido.criadoPorName = contexto.nomeUsuario;

                //Se passar o id carrega o cliente
                if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = Sqlservice.RetornaRelacaoCliente(new Guid(idCliente));

                //Prenche lista de preço para o contexto da página
                List<SelectListItem> itens = new List<SelectListItem>();
                itens = HelperAttributes.PreencheDropDownList(listaprecoData.GetAllRelacao(this.contexto.idOrganizacao));
                modelo.listaPrecos = itens;

            }
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedido-get", ex.Message);
            }
          
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreatePedido(PedidoViewModel entrada)
        {
            Pedido pedido = new Pedido();
            try
            {
                if (entrada.pedido != null)
                {
                    if (PedidoRules.PedidoCreate(entrada, out pedido, contexto))
                    {
                        pedidoData.Add(pedido);
                        return RedirectToAction("FormUpdatePedido", new { id = pedido.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedido-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdatePedido(string id)
        {
            PedidoViewModel modelo = new PedidoViewModel();

            try           
           {
                Pedido retorno = new Pedido();
          
               if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = pedidoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.pedido = retorno;
                        modelo.cliente = Sqlservice.RetornaRelacaoCliente(retorno.idCliente);
                        modelo.listapreco = Sqlservice.RetornaRelacaoListaPreco(retorno.idListaPreco);
                        //apresenta mensagem de registro atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePedido-get", ex.Message);
             }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdatePedido(PedidoViewModel entrada)
        {
            Pedido pedido = new Pedido();
            entrada.contexto = this.contexto;

            try
            {
                if (PedidoRules.PedidoUpdate(entrada, out pedido ))
                {
                    PedidoRules.CalculoPedido(ref pedido, produtoPedidoData);
                    pedidoData.Update(pedido);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdatePedido", new { id = pedido.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePedido-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridPedido(string filtro, int Page, string idCliente, int view)
        {
            try
            {                
                IEnumerable <PedidoGridViewModel> retorno ;

                if(String.IsNullOrEmpty(idCliente))
                {
                    //Se tiver filtro, busca em todas as linhas
                    if (!String.IsNullOrEmpty(filtro)) view = 4;


                    retorno = pedidoData.GetAllGridViewModel(contexto.idOrganizacao, view);
                }
                else
                {
                  
                    retorno = pedidoData.GetAllGridViewModelByCliente(new Guid(idCliente));
                }

                if (!String.IsNullOrEmpty(filtro))
                {
                    retorno = from u in retorno
                              where
                                (u.cliente.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.pedido.codigoPedido.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.cliente.nomeCliente.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))


                              select u;
                }

                retorno = retorno.OrderBy(x => x.pedido.codigoPedido); 

                if (Page == 0) Page = 1;
                return View(retorno.ToPagedList<PedidoGridViewModel>(Page, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,4, this.contexto.idUsuario, this.contexto.idOrganizacao, "GridPedido", ex.Message);
            }

            return View();
        }

        [HttpPost]
        public IActionResult FormStatusPedido(PedidoViewModel entrada)
        {
            Pedido pedido = new Pedido();
            entrada.contexto = this.contexto;

            try
            {
                if (PedidoRules.PedidoUpdateStatus(entrada, out pedido))
                {
                     pedidoData.Update(pedido);

                    return RedirectToAction("FormUpdatePedido", new { id = pedido.id.ToString() });
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
        public ViewResult FormStatusPedido(string id)
        {
            PedidoViewModel modelo = new PedidoViewModel();
            modelo.contexto = this.contexto;

            try
            {
                Pedido retorno = new Pedido();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = pedidoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.pedido = retorno;    
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 4, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusPedido-get", ex.Message);
            }
            return View(modelo);
        }
    }
}
