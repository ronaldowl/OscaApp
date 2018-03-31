using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using OscaFramework.Models;

namespace OscaApp.Controllers
{
    public class ReportController : Controller
    {
        public ViewResult ReportNativo()
        {
            return View();
        }
        public ViewResult ReportPrint(int tipo, string id)
        {
            Relatorio model = new Relatorio();
            model.idRegistro = id;

            if(tipo == 1) model.nomeReport = "FICHAATENDIMENTO";

            if (tipo == 2) model.nomeReport = "IMPRESSAOPEDIDO";

            if (tipo == 3) model.nomeReport = "IMPRESSAOOS";

            model.url = "http://www.report.oscas.com.br/ReportRenderPrint?tipo=" + tipo + "&id=" + model.idRegistro;



            return View(model);
        }
    }
}
