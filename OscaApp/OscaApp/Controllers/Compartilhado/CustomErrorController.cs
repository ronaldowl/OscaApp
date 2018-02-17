using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using Microsoft.AspNetCore.Http;
using OscaApp.RulesServices;
using OscaFramework.Log;

namespace OscaApp.Controllers
{
    public class CustomErrorController : Controller
    {  
        [HttpGet]
        public ViewResult ContextError()
        {
            CustomError   modelo = new CustomError();                        
            
            return View(modelo);
        }
        

    }
}
