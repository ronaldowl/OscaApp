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
using OscaApp.RulesServices;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class PedidoRetiradaController : Controller
    {
        private readonly IPedidoRetiradaData modeloData;
        private readonly IClienteData clienteData;
        private readonly IOrgConfigData orgConfigData;
        private readonly IOrganizacaoData organizacaoData;
        private ContextPage contexto;
        private readonly SqlGenericData Sqlservice;

        public PedidoRetiradaController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.modeloData = new PedidoRetiradaData(db);
            this.clienteData = new ClienteData(db);
            this.orgConfigData = new OrgConfigData(db);
            this.organizacaoData = new OrganizacaoData(db);
            this.Sqlservice = new SqlGenericData();
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreatePedidoRetirada()
        {
            PedidoRetiradaViewModel modelo = new PedidoRetiradaViewModel();
            modelo.pedidoRetirada = new PedidoRetirada();
            modelo.contexto = contexto;
            modelo.pedidoRetirada.criadoEm = DateTime.Now;
            modelo.pedidoRetirada.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreatePedidoRetirada(PedidoRetiradaViewModel entrada)
        {
            PedidoRetirada modelo = new PedidoRetirada();

            try
            {
                if (entrada.pedidoRetirada != null)
                {
                    if (PedidoRetiradaRules.PedidoRetiradaCreate(entrada, out modelo, contexto))
                    {
                        modeloData.Add(modelo);
                        return RedirectToAction("FormUpdatePedidoRetirada", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 33, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePedidoRetirada-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdatePedidoRetirada(string id)
        {
            PedidoRetiradaViewModel modelo = new PedidoRetiradaViewModel();
            modelo.pedidoRetirada = new PedidoRetirada();
            modelo.pedidoRetirada.id = new Guid(id);

            PedidoRetirada retorno = new PedidoRetirada();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.pedidoRetirada.id, contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.pedidoRetirada = retorno;
                    modelo.cliente = Sqlservice.RetornaRelacaoCliente(retorno.idCliente);

                    if (modelo.pedidoRetirada.idProfissional != null)
                    {
                        modelo.profissional = Sqlservice.RetornaRelacaoProfissional(retorno.idProfissional);
                    }

                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdatePedidoRetirada(PedidoRetiradaViewModel entrada)
        {
            PedidoRetirada modelo = new PedidoRetirada();
            entrada.contexto = this.contexto;
            try
            {
                if (PedidoRetiradaRules.PedidoRetiradaUpdate(entrada, out modelo))
                {
                    modeloData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdatePedidoRetirada", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 33, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePedidoRetirada-post", ex.Message);
            }

            return RedirectToAction("FormUpdatePedidoRetirada", new { id = modelo.id.ToString() });
        }

        public ViewResult GridPedidoRetirada(string filtro, int Page)
        {
            IEnumerable<PedidoRetirada> retorno = modeloData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)
                          select u;
            }
            retorno = retorno.OrderBy(x => x.codigo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<PedidoRetirada>(Page, 20));
        }
        [HttpGet]
        public ViewResult ImpressaoPedidoRetirada(string id)
        {
            ImpressaoPedidoRetiradaViewModel modelo = new ImpressaoPedidoRetiradaViewModel();

            modelo.pedidoRetirada = new PedidoRetirada();
            modelo.pedidoRetirada.id = new Guid(id);
            modelo.cliente = new Cliente();
            modelo.orgConfig = new OrgConfig();
            modelo.organizacao = new Organizacao();

            PedidoRetirada retorno = new PedidoRetirada();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.pedidoRetirada.id, contexto.idOrganizacao);
                modelo.cliente = clienteData.Get(retorno.idCliente, contexto.idOrganizacao);
                modelo.orgConfig = orgConfigData.Get(contexto.idOrganizacao);
                modelo.organizacao = organizacaoData.Get(contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.pedidoRetirada = retorno;
                }
            }
            return View(modelo);
        }

    }
}