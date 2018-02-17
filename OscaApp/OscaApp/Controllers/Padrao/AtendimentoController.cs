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

namespace OscaApp.Controllers
{
    public class AtendimentoController : Controller
    {
        private ContextPage contexto;
        public AtendimentoData atendimentoData;

        public AtendimentoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.atendimentoData = new AtendimentoData(db);
            this.contexto = new ContextPage().ExtractContext(httpContext);
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

        [HttpPost]
        public ViewResult FormCreateAtendimento(AtendimentoViewModel entrada)
        {
            Atendimento  modelo = new Atendimento ();

            if(entrada != null)
            {

               if( AtendimentoRules.AtendimentoCreate(entrada,out modelo, this.contexto))
                {
                    //Se retorna true grava no banco
                    atendimentoData.Add(modelo);
                }
            }                

            return View(modelo);
        }

    }
}
