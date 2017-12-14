using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ListaPrecoController : Controller
    {
        [HttpGet]
        public IActionResult FormCreateListaPreco(){
            return View();
        }

        [HttpPost]
        public IActionResult FormCreateListaPreco( int x){
            return RedirectToAction("FormUpdateListaPreco", new {});
        }
        public IActionResult FormUpdateListaPreco()
        {
            return View();
        }
    }
}
