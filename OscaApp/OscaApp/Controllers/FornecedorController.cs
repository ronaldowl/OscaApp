using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
    [Authorize]
    public class FornecedorController : Controller
    {

        [HttpPost]
        public IActionResult FormCreateFornecedor(string id)
        {
            return RedirectToAction("FormUpdateFornecedor", new { });
        }

        [HttpGet]
        public IActionResult FormCreateFornecedor()
        {
            return View();
        }

        public IActionResult FormUpdateFornecedor()
        {
            return View();
        }
    }
}