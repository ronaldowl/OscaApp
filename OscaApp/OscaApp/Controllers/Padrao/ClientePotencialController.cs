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
using OscaFramework.Models;
using OscaFramework.MicroServices;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ClientePotencialController : Controller
    {
        private readonly ClientePotencialData clientePotencialData;
      
        public string urlAmbiente { get; set; }

        private readonly SqlGenericData sqlData;
        private ContextPage contexto;
       
        public ClientePotencialController(ContexDataService db, IHttpContextAccessor httpContext, SqlGenericData _sqlData)
        {
            this.clientePotencialData = new ClientePotencialData(db);        
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.sqlData = _sqlData;
        }

        [TempData]
        public string StatusMessage { get; set; }              

        [HttpGet]
        public ViewResult FormCreateClientePotencial()
        {
            ClientePotencialViewModel modelo = new ClientePotencialViewModel();
            modelo.clientePotencial = new ClientePotencial();
            modelo.contexto = contexto;
            modelo.clientePotencial.criadoEm = DateTime.Now;
            modelo.clientePotencial.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateClientePotencial(ClientePotencialViewModel entrada)
        {

            ClientePotencial modelo = new ClientePotencial();
            entrada.contexto = contexto;

            try
            {
                if (entrada.clientePotencial != null)
                {
                    if (ClientePotencialRules.MontaClientePotencialCreate(entrada, out modelo, contexto))
                    {
                        clientePotencialData.Add(modelo);

                        return RedirectToAction("FormUpdateClientePotencial", new { id = modelo.id.ToString() });
                    }
                }
                else
                {

                    //Apresenta mensagem para o usuário
                    return RedirectToAction("ContexError", "CustomError", new { entityType = 1 });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateClientePotencial-post", ex.Message);

            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateClientePotencial(string id)
        {
            ClientePotencialViewModel modelo = new ClientePotencialViewModel();

            try
            {
                ClientePotencial retorno = new ClientePotencial();
                //Formulario com os dados do cliente
                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = clientePotencialData.Get(new Guid(id));

                    
                    if (retorno != null)
                    {
                        modelo.clientePotencial = retorno;                  

                        //apresenta mensagem de cliente atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateClientePotencial-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateClientePotencial(ClientePotencialViewModel entrada)
        {
            ClientePotencial modelo = new ClientePotencial();
            try
            {
                if (ClientePotencialRules.MontaClientePotencialUpdate(entrada, out modelo, this.contexto))
                {
                    clientePotencialData.Update(modelo);                                        
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateClientePotencial", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateClientePotencial-post", ex.Message);
            }

            return RedirectToAction("FormUpdateClientePotencial", new { id = modelo.id.ToString() });
        }  


        public ViewResult GridClientePotencial(string filtro, int Page,int view)
        {
            try
            {               
               IEnumerable<ClientePotencial> retorno = clientePotencialData.GetAll(contexto.idOrganizacao, view);

                ViewBag.viewContexto = view;

                if (!String.IsNullOrEmpty(filtro))
                {
                    retorno = from u in retorno
                              where (u.nomeCliente.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                    (u.email.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))
                              select u;

                }
                retorno = retorno.OrderBy(x => x.nomeCliente);

                if (Page == 0) Page = 1;

                return View(retorno.ToPagedList<ClientePotencial>(Page, 20));

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
