using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;

namespace OscaAPI.Controllers
{
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        
        [HttpGet]
        public JsonResult SetStatePedido(int idPedido, int StatusPedido)
        {
            Parametros X = new Parametros();
            X.Descrição = idPedido.ToString();
            X.valor = StatusPedido.ToString();
         
             
            return Json(X);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
    }
}
