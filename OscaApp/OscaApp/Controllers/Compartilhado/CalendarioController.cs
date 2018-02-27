using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.RulesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.Data;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly SqlGeneric sqlServices;

        public CalendarioController(SqlGeneric _sqlServices)
        {
            sqlServices = _sqlServices;

        }

        [HttpGet]
        public ViewResult Mes(int Mes, int Ano)
        {
            Calendario calen = new Calendario();

            List<Atendimento> lista = sqlServices.RetornaAtendimentosMes(Mes.ToString(), Ano.ToString());

            if (Mes > 0)
            {

               calen = CalendarioRules.PreencheMes(Mes, Ano);
            }
            else{

                calen = CalendarioRules.PreencheMes(DateTime.Now.Month, DateTime.Now.Year );
            }          

            return View(calen);
        }

        [HttpPost]
        public ViewResult Mes(Calendario entrada)
        {
            Calendario calen = new Calendario();
         
            calen = CalendarioRules.PreencheMes(entrada.mes, entrada.ano);          
           
            return View(calen);
        }
    }
}
