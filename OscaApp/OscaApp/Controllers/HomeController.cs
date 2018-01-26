using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
 
    public class HomeController : Controller
    {      

        public IActionResult About()
        {           
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
