using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OscaFramework.MicroServices;
using OscaFramework.Models;
using OscaFramework.Data;

namespace OscaAPI.Controllers
{
   
    public class ValuesController : Controller
    {
        private readonly SqlGenericData sqlData;
        private readonly SqlGeneric sqlServices;

         


        public ValuesController(SqlGenericData _sqlData, SqlGeneric _sqlServices)
        {
            this.sqlData = _sqlData;
            this.sqlServices = _sqlServices;

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Método Genérico para configurar o Status de todas as Entidades
        /// </summary>
        /// <param name="value"></param>
        [Route("api/SetStatus")]
        [HttpPost]
        public JsonResult SetStatus([FromBody]Relacao relacao)
        {
            Relacao x = new Relacao();

            //if (sqlServices.SetStates(x))  return Json(true);

            return Json(false);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
