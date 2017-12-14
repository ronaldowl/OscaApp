using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using OSCA.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
    [Authorize]
    public class PaineisController :Controller
    {

        public ViewResult PainelOperacional()
        {
            PainelOperacionalViewModel model = new PainelOperacionalViewModel();
            //ContextPage contexto = new ContextPage(HttpContext.Session.GetString("email"), HttpContext.Session.GetString("organizacao"));
            //model.contextPage = contexto;
            return View(model);
        }
        public ViewResult PainelGerenciamento()
        {
           // PainelGerenciamentoViewModel model = new PainelGerenciamentoViewModel();
 
            return View();
        }

        public ViewResult PainelConfiguracoes()
        {
            // PainelGerenciamentoViewModel model = new PainelGerenciamentoViewModel();

            return View();
        }
        public ViewResult PainelCadastro()
        {
            // PainelGerenciamentoViewModel model = new PainelGerenciamentoViewModel();

            return View();
        }
        public ViewResult PainelVendas()
        {
            // PainelGerenciamentoViewModel model = new PainelGerenciamentoViewModel();

            return View();
        }
        public ViewResult PainelServico()
        {
            // PainelGerenciamentoViewModel model = new PainelGerenciamentoViewModel();

            return View();
        }
        public ViewResult PainelFinanceiro()
        {
            // PainelGerenciamentoViewModel model = new PainelGerenciamentoViewModel();

            return View();
        }


    }
}
