using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;
using OscaFramework.MicroServices;
using OscaApp.RulesServices;
using Microsoft.AspNetCore.Http;

namespace OscaAPI.Controllers
{
    
    public class ClientePotencialAPIController : Controller
    {        
        
        private readonly ContextPage contexto;
        private readonly ClienteData clienteData;
        private readonly ClientePotencialData clientePotencialData;

        public ClientePotencialAPIController(SqlGenericRules _sqlRules, IHttpContextAccessor httpContext, ContexDataService db)
        {
            this.clienteData = new ClienteData(db);
            this.clientePotencialData = new ClientePotencialData(db);            
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }
     

        [Route("api/[controller]/ConsultaEmailDuplicado")]
        [HttpGet("{valor}")]
        public JsonResult ConsultaEmailDuplicado(string valor)
        {
            ResultService retorno = new ResultService();
            SqlGenericRules sqlRules = new SqlGenericRules();
            try
            {

                if (ClientePotencialRules.EmailExistente(valor, contexto.idOrganizacao, sqlRules))
                {
                    retorno.statusOperation = true;
                }
                return Json(retorno);
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }

        [Route("api/[controller]/SetStatus")]
        [HttpGet("{valor, idRegistro}")]
        public JsonResult SetStatus(string idRegistro, int valor)
        {
            ResultService retorno = new ResultService();
            SqlGenericData sqlServices = new SqlGenericData();
            try
            {
                if (ClientePotencialRules.SetStatus(valor, idRegistro, clientePotencialData, clienteData, this.contexto, sqlServices))
                {
                    
                    retorno.statusOperation = true;
                }
                return Json(retorno);
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }
    }
}
