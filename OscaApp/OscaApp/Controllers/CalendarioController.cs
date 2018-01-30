using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Controllers
{
    public class CalendarioController : Controller
    {

        public ViewResult Mes()
        {
            Calendario calen = new Calendario();

            return View(calen);
        }
    }
}
