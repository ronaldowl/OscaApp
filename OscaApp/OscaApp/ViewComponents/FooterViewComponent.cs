using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using OscaFramework.Models;

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
            if (org.statusOrg == CustomEnumStatus.StatusOrg.EmAvaliacao)
            {          
                modelo.msgAvaliacao =  " - Avaliação: expira em " + org.dataExpiracao.ToShortDateString();
            }

            return View("LoginFooter", modelo);

        }

    }
}
