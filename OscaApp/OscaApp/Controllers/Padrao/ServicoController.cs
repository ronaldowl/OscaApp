using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using OscaFramework.Models;



namespace OscaApp.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        private readonly IServicoData servicoData;
        private ContextPage contexto;



        public ServicoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.servicoData = new ServicoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [HttpGet]
        public ViewResult FormCreateServico()
        {
            ServicoViewModel modelo = new ServicoViewModel();
            modelo.servico = new Servico();
            modelo.contexto = contexto;
            modelo.servico.criadoEm = DateTime.Now;
            modelo.servico.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateServico(ServicoViewModel entrada)
        {
            Servico modelo = new Servico();

            try
            {
                if (entrada.servico != null)
                {
                    if  (ServicoRules.ServicoCreate(entrada, out modelo, contexto)) {                    
                        servicoData.Add(modelo);
                        return RedirectToAction("FormUpdateServico", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 6, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateServico-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateServico(string id)
        {
            ServicoViewModel modelo = new ServicoViewModel();
            modelo.servico = new Servico();
            modelo.servico.id = new Guid(id);

            Servico retorno = new Servico();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = servicoData.Get(modelo.servico.id, contexto.idOrganizacao);

                //TODO Formata campos

                if (retorno != null)
                {
                    modelo.servico = retorno;
                }
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateServico(ServicoViewModel entrada)
        {
            Servico modelo = new Servico();
            entrada.contexto = this.contexto;
            try
            {
                if (ServicoRules.ServicoUpdate(entrada, out modelo))
                {
                    servicoData.Update(modelo);
                    return RedirectToAction("FormUpdateServico", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 6, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateServico-post", ex.Message);
            }

            return RedirectToAction("FormUpdateServico", new { id = modelo.id.ToString() });
        }

        public ViewResult GridServico(string filtro, int Page)
        {
            IEnumerable<Servico> retorno = servicoData.GetAll(contexto.idOrganizacao);

            // realiza busca por Nome, Código 
            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.codigo.Equals(filtro, StringComparison.InvariantCultureIgnoreCase) ||
                                                         A.nomeServico.Equals(filtro, StringComparison.InvariantCultureIgnoreCase) ) select A;
            
            retorno = retorno.OrderBy(x => x.nomeServico);
             

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Servico>(Page, 10));
        }

        public ViewResult LookupServico(string filtro, int Page)
        {          

            return View( );
        }

        public ViewResult GridLookupServico(string filtro, int Page)
        {
            IEnumerable<Servico> retorno = servicoData.GetAll(contexto.idOrganizacao);

            // realiza busca por Nome, Código 
            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.codigo == filtro || A.nomeServico == filtro) select A;

            retorno = retorno.OrderBy(x => x.nomeServico);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Servico>(Page, 10));
        }
    }
}