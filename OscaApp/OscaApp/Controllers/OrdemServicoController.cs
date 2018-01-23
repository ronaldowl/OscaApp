using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;


namespace OscaApp.Controllers
{
    [Authorize]
    public class OrdemServicoController : Controller
    {
        private readonly IOrdemServicoData ordemServicoData;
        private ContextPage contexto;

        public OrdemServicoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.ordemServicoData = new OrdemServicoData(db);
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
        }

        [HttpGet]
        public ViewResult FormCreateOrdemServico()
        {
            OrdemServicoViewModel modelo = new OrdemServicoViewModel();
            modelo.ordemServico = new OrdemServico();
            modelo.contexto = contexto;
            modelo.ordemServico.criadoEm = DateTime.Now;
            modelo.ordemServico.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateOrdemServico(OrdemServicoViewModel entrada)
        {
            OrdemServico modelo = new OrdemServico();

            try
            {
                if (entrada.ordemServico.codigo!=null)
                {
                  if  (OrdemServicoRules.OrdemServicoCreate(entrada, out modelo, contexto)) {
                        ordemServicoData.Add(modelo);
                        return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateOrdemServico(string id)
        {
            OrdemServicoViewModel modelo = new OrdemServicoViewModel();
            modelo.ordemServico = new OrdemServico();
            modelo.ordemServico.id = new Guid(id);

            OrdemServico retorno = new OrdemServico();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = ordemServicoData.Get(modelo.ordemServico.id, contexto.idOrganizacao);

                if (retorno != null)
                {
                    modelo.ordemServico = retorno;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateOrdemServico(OrdemServicoViewModel entrada)
        {
            OrdemServico modelo = new OrdemServico();
            entrada.contexto = this.contexto;
            try
            {
                if (OrdemServicoRules.OrdemServicoUpdate(entrada, out modelo))
                {
                    ordemServicoData.Update(modelo);
                    return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }

            return RedirectToAction("FormUpdateOrdemServico", new { id = modelo.id.ToString() });
        }

        public ViewResult GridOrdemServico(string filtro, int Page)
        {
            IEnumerable<OrdemServico> retorno = ordemServicoData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.dataEntrada);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<OrdemServico>(Page, 10));
        }
    }
}
