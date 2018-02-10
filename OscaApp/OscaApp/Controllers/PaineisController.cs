using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
    [Authorize]
    public class PaineisController :Controller
    {
        private ContextPage contexto;

        public PaineisController(  IHttpContextAccessor httpContext)
        {
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        public ViewResult PainelHome()
        {
            return View(this.contexto);
        }

        public ViewResult PainelOperacional()
        {          
            return View();
        }
        public ViewResult PainelGerenciamento()
        {          
            return View();
        }

        public ViewResult PainelConfiguracoes()
        {          
            return View();
        }
        public ViewResult PainelCadastro()
        {       
            return View();
        }
        public ViewResult PainelVendas()
        {           
            return View();
        }
        public ViewResult PainelServico()
        {       
            return View();
        }
        public ViewResult PainelFinanceiro()
        {           
            return View();
        }

        public ViewResult PainelSuporte()
        {
            return View();
        }

        public ViewResult PainelAll()
        {
            return View();
        }



    }
}
