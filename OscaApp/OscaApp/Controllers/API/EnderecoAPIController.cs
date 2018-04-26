using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;
using Correios;

namespace OscaAPI.Controllers
{
    
    public class EnderecoAPIController : Controller
    {
        private readonly IEnderecoData serviceData;
        

        public EnderecoAPIController(ContexDataService db)
        {
            this.serviceData = new EnderecoData(db);          
        }    

        [Route("api/[controller]/Delete")]
        [HttpGet("{id}")]
        public JsonResult Delete(string id)
        {
            ResultService retorno = new ResultService();
            try
            {
                Endereco modelo = new Endereco();
                modelo.id = new Guid(id);

                serviceData.Delete(modelo);
                retorno.statusOperation = true;
               return Json(retorno);
            }
            catch (System.Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }
        [Route("api/[controller]/ConsultaCEP")]
        [HttpGet("{cep}")]
        public JsonResult ConsultaCEP(string cep)
        {
            ResultService retorno = new ResultService();
           

            var service = new CorreiosApi();
            var dados = service.consultaCEP("cep");
            retorno.statusMensagem = dados.bairro;
               


                retorno.statusOperation = true;
                return Json(dados);
         

        
        }
    }
}
