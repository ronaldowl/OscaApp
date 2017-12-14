using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {      

        public IActionResult About()
        {
            ViewData["Message"] = "Desenvolvido por - Trine Assessoria em Informática";

            return View();
        }

        public IActionResult Contact()
        {
           

            Cliente cl = new Models.Cliente();

            ViewData["Message"] = cl.id;
            return View();
        }

        public IActionResult Error()
        {

           

            return View();
        }
    }
}
