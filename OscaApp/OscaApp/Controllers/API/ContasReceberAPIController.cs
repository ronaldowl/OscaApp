using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;
using OscaFramework.MicroServices;

namespace OscaAPI.Controllers
{
    
    public class ContasReceberAPIController : Controller
    {
        private readonly IContasReceberData serviceData;
        private readonly SqlGenericRules sqlServices;


        public ContasReceberAPIController(ContexDataService db, SqlGenericRules _sqlRules)
        {
            this.serviceData = new ContasReceberData(db);
            this.sqlServices = _sqlRules;
        }    

        [Route("api/[controller]/Delete")]
        [HttpGet("{id}")]
        public JsonResult Delete(string id)
        {
            ResultService retorno = new ResultService();
            try
            {
                ContasReceber modelo = new ContasReceber();
                modelo.id = new Guid(id);

                serviceData.Delete(modelo);
                retorno.statusOperation = true;
               return Json(retorno);
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }

        [Route("api/[controller]/RetornaValorEmAberto")]
        [HttpGet("{id}")]
        public JsonResult RetornaValorEmAberto(string id)
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.valor = sqlServices.RetornaValorEmAberto(id);
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
