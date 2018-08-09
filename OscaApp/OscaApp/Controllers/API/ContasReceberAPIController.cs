using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;
using OscaFramework.MicroServices;
using Microsoft.AspNetCore.Http;

namespace OscaAPI.Controllers
{
    
    public class ContasReceberAPIController : Controller
    {
        private readonly IContasReceberData serviceData;
        private readonly SqlGenericRules sqlServices;
        private readonly ContextPage contexto;

        public ContasReceberAPIController(ContexDataService db, SqlGenericRules _sqlRules, IHttpContextAccessor httpContext)
        {
            this.serviceData = new ContasReceberData(db);
            this.sqlServices = _sqlRules;
            this.contexto = new ContextPage().ExtractContext(httpContext);
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

        [Route("api/[controller]/RetornaValorEmAbertoCliente")]
        [HttpGet("{id}")]
        public JsonResult RetornaValorEmAbertoCliente(string id)
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.valor = sqlServices.RetornaValorEmAbertoCliente(id);
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
        [HttpGet()]
        public JsonResult RetornaValorEmAberto()
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.valor = sqlServices.RetornaValorEmAberto(this.contexto.idOrganizacao.ToString());
                retorno.statusOperation = true;

                return Json(retorno);
            }
            catch (Exception ex)
            {
                retorno.statusMensagem = ex.Message;
            }

            return Json(retorno);
        }

        [Route("api/[controller]/RetornaValorRecebidoCliente")]
        [HttpGet("{id}")]
        public JsonResult RetornaValorRecebidoCliente(string id)
        {
            ResultServiceList retorno = new ResultServiceList();
            try
            {
                retorno.valor = sqlServices.RetornaValorRecebidoCliente(id);
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
