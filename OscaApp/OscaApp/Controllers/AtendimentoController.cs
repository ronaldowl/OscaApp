using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using Microsoft.AspNetCore.Http;

namespace OscaApp.Controllers
{
    public class AtendimentoController : Controller
    {
        private ContextPage contexto;

        public AtendimentoController(ContexDataService db, IHttpContextAccessor httpContext)
        {        
            contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
        }

        [HttpGet]
        public ViewResult FormCreateAtendimento()
        {
            AtendimentoViewModel modelo = new AtendimentoViewModel();
            modelo.contexto = contexto;
            modelo.atendimento = new Atendimento();
            modelo.atendimento.status = CustomEnumStatus.Status.Ativo;

            modelo.atendimento.criadoEm = DateTime.Now;
            modelo.atendimento.criadoPorName = contexto.nomeUsuario;       
            
            return View(modelo);
        }

        //[HttpPost]
        //public ViewResult FormCreateAtendimento()
        //{
        //    AtendimentoViewModel modelo = new AtendimentoViewModel();
        //    modelo.contexto = contexto;
        //    modelo.atendimento = new Atendimento();
        //    modelo.atendimento.status = CustomEnumStatus.Status.Ativo;

        //    modelo.atendimento.criadoEm = DateTime.Now;
        //    modelo.atendimento.criadoPorName = contexto.nomeUsuario;

        //    return View(modelo);
        //}

    }
}
