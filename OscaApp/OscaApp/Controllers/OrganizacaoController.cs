using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OscaApp.Controllers
{
    [Authorize]
    public class OrganizacaoController :Controller
    {

        public ViewResult FormUpdateOrganizacao()
        {
            OrganizacaoViewModel model = new OrganizacaoViewModel();
            ContextPage contexto = new ContextPage(HttpContext.Session.GetString("email"), HttpContext.Session.GetString("organizacao"));
            model.contexto = contexto;
            return View(model);
        }
      
    }
}
