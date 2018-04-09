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
    
    public class ClienteAPIController : Controller
    {
        
        private readonly SqlGenericRules sqlServices;
        private readonly ContextPage contexto;
        private readonly ClienteData clienteData;

        public ClienteAPIController(SqlGenericRules _sqlRules, IHttpContextAccessor httpContext, ContexDataService db)
        {
            this.clienteData = new ClienteData(db);
            this.sqlServices = _sqlRules;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }
     

        [Route("api/[controller]/ConsultaCnpjCpfDuplicado")]
        [HttpGet("{valor}")]
        public JsonResult ConsultaCnpjCpfDuplicado(string valor)
        {
            ResultService retorno = new ResultService();
            try
            {

                if (ClienteRules.Cnpj_CfpExistente(valor, contexto.idOrganizacao, sqlServices))
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
        [HttpGet("{valor, idCliente}")]
        public JsonResult SetStatus(string idCliente, int valor)
        {
            ResultService retorno = new ResultService();
            try
            {
                if (ClienteRules.SetStatus(valor, idCliente, clienteData, this.contexto))
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
