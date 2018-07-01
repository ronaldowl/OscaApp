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
    
    public class BalcaoVendasAPIController : Controller
    {
        
        private readonly SqlGenericRules sqlServices;
        private readonly ContextPage contexto;
   

        public BalcaoVendasAPIController(SqlGenericRules _sqlRules, IHttpContextAccessor httpContext)
        {
            
            this.sqlServices = _sqlRules;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }     

        [Route("api/[controller]/ConsultaProduto")]
        [HttpGet("{filtro, idLista}")]
        public JsonResult ConsultaProduto(string filtro, string idLista)
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.ListaProdutoBalcao = sqlServices.ConsultaProduto(filtro, idLista);
                retorno.statusOperation = true;
               
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
