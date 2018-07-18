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



namespace OscaApp.Controllers
{
    [Authorize]
    public class ServicoPedidoRetiradaController : Controller
    {
        private readonly IServicoPedidoRetiradaData servicoPedidoRetiradaData;
        private ContextPage contexto;



        public ServicoPedidoRetiradaController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.servicoPedidoRetiradaData = new ServicoPedidoRetiradaData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateServicoPedidoRetirada()
        {
            ServicoPedidoRetiradaViewModel modelo = new ServicoPedidoRetiradaViewModel();
            modelo.servicoPedidoRetirada = new ServicoPedidoRetirada();
            modelo.contexto = contexto;
            modelo.servicoPedidoRetirada.criadoEm = DateTime.Now;
            modelo.servicoPedidoRetirada.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateServicoPedidoRetirada(ServicoPedidoRetiradaViewModel entrada)
        {
            ServicoPedidoRetirada modelo = new ServicoPedidoRetirada();

            try
            {
                if (entrada.servicoPedidoRetirada != null)
                {
                    if  (ServicoPedidoRetiradaRules.ServicoPedidoRetiradaCreate(entrada, out modelo, contexto)) {
                        servicoPedidoRetiradaData.Add(modelo);
                        return RedirectToAction("FormUpdateServicoPedidoRetirada", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 35, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateServicoPedidoRetirada-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateServicoPedidoRetirada(string id)
        {
            ServicoPedidoRetiradaViewModel modelo = new ServicoPedidoRetiradaViewModel();
            modelo.servicoPedidoRetirada = new ServicoPedidoRetirada();
            modelo.servicoPedidoRetirada.id = new Guid(id);

            ServicoPedidoRetirada retorno = new ServicoPedidoRetirada();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = servicoPedidoRetiradaData.Get(modelo.servicoPedidoRetirada.id, contexto.idOrganizacao);

                //TODO Formata campos

                if (retorno != null)
                {
                    modelo.servicoPedidoRetirada = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateServicoPedidoRetirada(ServicoPedidoRetiradaViewModel entrada)
        {
            ServicoPedidoRetirada modelo = new ServicoPedidoRetirada();
            entrada.contexto = this.contexto;
            try
            {
                if (ServicoPedidoRetiradaRules.ServicoPedidoRetiradaUpdate(entrada, out modelo))
                {
                    servicoPedidoRetiradaData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateServicoPedidoRetirada", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 35, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateServicoPedidoRetirada-post", ex.Message);
            }

            return RedirectToAction("FormUpdateServicoPedidoRetirada", new { id = modelo.id.ToString() });
        }

        public ViewResult GridServicoPedidoRetirada(string filtro, int Page)
        {
            IEnumerable<ServicoPedidoRetirada> retorno = servicoPedidoRetiradaData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno select u;
            }
            retorno = retorno.OrderBy(x => x.valor);
             

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<ServicoPedidoRetirada>(Page, 20));
        }
    }
}