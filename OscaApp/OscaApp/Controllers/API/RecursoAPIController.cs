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
    
    public class RecursoAPIController : Controller
    {
        private readonly IRecursoData serviceData;
        

        public RecursoAPIController(ContexDataService db)
        {
            this.serviceData = new RecursoData(db);          
        }    

        [Route("api/[controller]/Delete")]
        [HttpGet("{id}")]
        public JsonResult Delete(string id)
        {
            ResultService retorno = new ResultService();
            try
            {
                Recurso modelo = new Recurso();
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
