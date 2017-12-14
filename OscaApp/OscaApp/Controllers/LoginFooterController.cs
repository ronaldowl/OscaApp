using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OscaApp.Controllers
{
    public class LoginFooterController : Controller
    {      
       
        public IActionResult LoginFooter()
        {           
            return View();
        }
    }
}
