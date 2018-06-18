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
    
    public class ListaPrecoAPIController : Controller
    {
        
        private readonly SqlGenericData sqlServices;
        private readonly ContextPage contexto;        

        public ListaPrecoAPIController(SqlGenericData _sqlRules, IHttpContextAccessor httpContext, ContexDataService db)
        {            
            this.sqlServices = _sqlRules;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }
     

        [Route("api/[controller]/ConsultaListaPrecoPadrao")]
        [HttpGet("{valor}")]
        public JsonResult ConsultaListaPrecoPadrao(string valor)
        {
            ResultService retorno = new ResultService();
            try
            {

                if (ListaPrecoRules.ConsultaListaPadrao(contexto.idOrganizacao.ToString(), sqlServices))
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

        //[Route("api/[controller]/SetStatus")]
        //[HttpGet("{valor, idCliente}")]
        //public JsonResult SetStatus(string idCliente, int valor)
        //{
        //    ResultService retorno = new ResultService();
        //    try
        //    {
        //        if (ClienteRules.SetStatus(valor, idCliente, clienteData, this.contexto))
        //        {
        //            retorno.statusOperation = true;
        //        }
        //        return Json(retorno);
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.statusMensagem = ex.Message;
        //    }

        //    return Json(retorno);
        //}
    }
}
