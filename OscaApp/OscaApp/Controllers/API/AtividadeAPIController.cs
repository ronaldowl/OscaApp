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
    
    public class AtividadeAPIController : Controller
    {
        private readonly IAtividadeData atividadeData;
        

        public AtividadeAPIController(ContexDataService db)
        {
            this.atividadeData = new AtividadeData(db);          
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        [Route("api/[controller]/Delete")]
        [HttpGet("{id}")]
        public JsonResult Delete(string id)
        {
            ResultService retorno = new ResultService();
            try
            {
                Atividade modelo = new Atividade();
                modelo.id = new Guid(id);

                atividadeData.Delete(modelo);
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
