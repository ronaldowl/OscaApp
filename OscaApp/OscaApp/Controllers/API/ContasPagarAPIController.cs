using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;

namespace OscaAPI.Controllers
{
    
    public class ContasPagarAPIController : Controller
    {
        private readonly IContasPagarData serviceData;
        

        public ContasPagarAPIController(ContexDataService db)
        {
            this.serviceData = new ContasPagarData(db);          
        }    

        [Route("api/[controller]/Delete")]
        [HttpGet("{id}")]
        public JsonResult Delete(string id)
        {
            ResultService retorno = new ResultService();
            try
            {
                ContasPagar modelo = new ContasPagar();
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
    }
}
