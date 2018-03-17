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
    public class FornecedorController : Controller
    {
        private readonly IFornecedorData fornecedorData;
        private ContextPage contexto;



        public FornecedorController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.fornecedorData = new FornecedorData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [HttpGet]
        public ViewResult FormCreateFornecedor()
        {
            FornecedorViewModel modelo = new FornecedorViewModel();
            modelo.Fornecedor = new Fornecedor();
            modelo.Contexto = contexto;
            modelo.Fornecedor.criadoEm = DateTime.Now;
            modelo.Fornecedor.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateFornecedor(FornecedorViewModel entrada)
        {
            Fornecedor modelo = new Fornecedor();

            try
            {
                if (entrada.Fornecedor != null)
                {
                    if (FornecedorRules.FornecedorCreate(entrada, out modelo, contexto))
                    {
                        fornecedorData.Add(modelo);
                        return RedirectToAction("FormUpdateFornecedor", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 14, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateFornecedor-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateFornecedor(string id)
        {
            FornecedorViewModel modelo = new FornecedorViewModel();
            modelo.Fornecedor = new Fornecedor();
            modelo.Fornecedor.id = new Guid(id);

            Fornecedor retorno = new Fornecedor();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = fornecedorData.Get(modelo.Fornecedor.id, contexto.idOrganizacao);            

                if (retorno != null)
                {
                    modelo.Fornecedor = retorno;
                }
            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateFornecedor(FornecedorViewModel entrada)
        {
            Fornecedor modelo = new Fornecedor();
            entrada.Contexto = this.contexto;
            try
            {
                if (FornecedorRules.FornecedorUpdate(entrada, out modelo))
                {
                    fornecedorData.Update(modelo);
                    return RedirectToAction("FormUpdateFornecedor", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 14, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-post", ex.Message);
            }

            return RedirectToAction("FormUpdateFornecedor", new { id = modelo.id.ToString() });
        }

        public ViewResult GridFornecedor(string filtro, int Page)
        {
            IEnumerable<Fornecedor> retorno = fornecedorData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno where (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))||
                          (u.nomeFornecedor.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))select u;
            }

            retorno = retorno.OrderBy(x => x.nomeFornecedor);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Fornecedor>(Page, 10));
        }
    }
}
