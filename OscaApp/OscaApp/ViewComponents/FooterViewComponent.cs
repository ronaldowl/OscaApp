using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OscaApp.framework.Models.CustomEnum;

namespace OscaApp.ViewComponents
{

    [ViewComponent(Name = "Footer")]
    public class FooterViewComponent : ViewComponent
    {
 
        private ContextPage contexto;

        public FooterViewComponent( IHttpContextAccessor httpContext)
        {      

            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            Footer modelo = new Footer();       
            
            Organizacao org = new Organizacao(contexto.idOrganizacao);

            modelo.nomeOrganizacao = org.nomeAmigavel;
            modelo.statusOrg = org.statusOrg;

            //consulta se a Organização esta ativa
            if (org.statusOrg == StatusOrg.EmAvaliacao)
            {
                modelo.msgAvaliacao = " - Falta 20 dias de Avaliação";
            }           


            return View("LoginFooter", modelo);

        }

    }
}
