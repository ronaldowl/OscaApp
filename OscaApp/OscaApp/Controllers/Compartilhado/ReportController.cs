using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
    public class ReportController : Controller
    {
        public ViewResult ReportNativo()
        {
            return View();
        }
    }
}
