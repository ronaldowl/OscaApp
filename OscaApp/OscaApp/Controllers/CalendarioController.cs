using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.RulesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Controllers
{
    public class CalendarioController : Controller
    {

        public ViewResult Mes(int Mes, int Ano)
        {
            Calendario calen = new Calendario();

            if (Mes > 0)
            {
                calen = CalendarioRules.PreencheMes(Mes, Ano);
            }
            else{

                calen = CalendarioRules.PreencheMes(DateTime.Now.Month, DateTime.Now.Year );
            }          

            return View(calen);
        }
    }
}
