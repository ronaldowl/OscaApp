﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaFramework.Models;
using OscaApp.Data;

namespace OscaAPI.Controllers
{

    public class OrdemServicoAPIController : Controller
    {
        private readonly IOrdemServicoData serviceData;


        public OrdemServicoAPIController(ContexDataService db)
        {
            this.serviceData = new OrdemServicoData(db);
        }

        [Route("api/[controller]/Delete")]
        [HttpGet("{id}")]
        public JsonResult Delete(string id)
        {
            ResultService retorno = new ResultService();
            try
            {
                OrdemServico modelo = new OrdemServico();
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