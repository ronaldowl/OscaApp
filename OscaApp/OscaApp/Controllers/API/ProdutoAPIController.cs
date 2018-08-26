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
    
    public class ProdutoAPIController : Controller
    {
        
        private readonly SqlGenericRules sqlServices;
        private readonly ContextPage contexto;
        private readonly ProdutoData produtoData;

        public ProdutoAPIController(SqlGenericRules _sqlRules, IHttpContextAccessor httpContext, ContexDataService db)
        {
            this.produtoData = new ProdutoData(db);
            this.sqlServices = _sqlRules;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }    

 
        [Route("api/[controller]/SetStatus")]
        [HttpGet("{valor, idRegistro}")]
        public JsonResult SetStatus(string idRegistro, int valor)
        {
            ResultService retorno = new ResultService();
            try
            {
                if (ProdutoRules.SetStatus(valor, idRegistro, produtoData, this.contexto))
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
