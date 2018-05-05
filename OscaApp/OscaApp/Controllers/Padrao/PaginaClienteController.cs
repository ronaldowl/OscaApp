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

namespace OscaApp.Controllers
{
 
    public class PaginaClienteController : Controller
    {
        private readonly ClientePotencialData clientePotencialData;
      
        public string urlAmbiente { get; set; }

        private readonly SqlGenericData sqlData;
   
       
        public PaginaClienteController(ContexDataService db, SqlGenericData _sqlData)
        {
            this.clientePotencialData = new ClientePotencialData(db);        
         
            this.sqlData = _sqlData;
        }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string StatusMessageLead { get; set; }
        [TempData]
        public string idOrganizacao { get; set; }
                  
        
        [HttpGet]
        public ViewResult FormularioEntrada(string id)
        {
            PaginaClienteViewModel modelo = new PaginaClienteViewModel();
            modelo.clientePotencial = new ClientePotencial();
            idOrganizacao = id;
            if (!String.IsNullOrEmpty(StatusMessageLead))
            {
                //apresenta mensagem de cliente atualizado com sucesso
                modelo.StatusMessageLead = StatusMessageLead;
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormularioEntrada(PaginaClienteViewModel entrada)
        {

            ClientePotencial modelo = new ClientePotencial();           

            try
            {
                if (entrada.clientePotencial != null)
                {
                    if (ClientePotencialRules.MontaClientePotencialCreateFomulario(entrada, out modelo, new Guid (idOrganizacao)))
                    {
                        clientePotencialData.Add(modelo);
                        StatusMessageLead = "Enviado com Sucesso!";
                        return RedirectToAction("FormularioEntrada", new {id = idOrganizacao });
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
               

            }
            return View();
        }
           
    }
}
