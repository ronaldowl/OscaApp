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
