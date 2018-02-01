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
            Calendario calen = CalendarioRules.PreencheMes(Mes, Ano);

            return View(calen);
        }
    }
}
