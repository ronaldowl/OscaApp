using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaFramework.Models;

using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ProfissionalController : Controller
    {
        private readonly IProfissionalData profissionalData;
        private ContextPage contexto;
        private readonly SqlGenericData sqlData;

        public ProfissionalController(ContexDataService db, IHttpContextAccessor httpContext, SqlGenericData _sqlData)
        {
            this.profissionalData = new ProfissionalData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.sqlData = _sqlData;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateProfissional()
        {
            ProfissionalViewModel modelo = new ProfissionalViewModel();
            modelo.profissional = new Profissional();
            modelo.contexto = contexto;
            modelo.profissional.criadoEm = DateTime.Now;
            modelo.profissional.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateProfissional(ProfissionalViewModel entrada)
        {
            Profissional modelo = new Profissional();

            try
            {
                if (entrada.profissional !=null)
                {
                  if  (ProfissionalRules.ProfissionalCreate(entrada, out modelo, contexto)) {
                        profissionalData.Add(modelo);
                        return RedirectToAction("FormUpdateProfissional", new { id = modelo.id.ToString() });
                  }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 17, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateProfissional-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateProfissional(string id)
        {
            ProfissionalViewModel modelo = new ProfissionalViewModel();
            modelo.profissional = new Profissional();
            modelo.profissional.id = new Guid(id);

            Profissional retorno = new Profissional();
       
            if (!String.IsNullOrEmpty(id))
            {
                retorno = profissionalData.Get(modelo.profissional.id, contexto.idOrganizacao);

                modelo.banco = sqlData.RetornaRelacaoBanco(retorno.idBanco);

                if (retorno.idUsuario != null) modelo.usuario = sqlData.RetornaRelacaoUsuario(retorno.idUsuario);

                if (retorno != null)
                {
                    modelo.profissional = retorno;
                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateProfissional(ProfissionalViewModel entrada)
        {
            Profissional modelo = new Profissional();
            entrada.contexto = this.contexto;
        
            try
            {
                if (ProfissionalRules.ProfissionalUpdate(entrada, out modelo))
                {
                    profissionalData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateProfissional", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 17, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateProfissional-post", ex.Message);
            }

            return RedirectToAction("FormUpdateProfissional", new { id = modelo.id.ToString() });
        }

        public ViewResult GridProfissional(string filtro, int Page)
        {
            IEnumerable<Profissional> retorno = profissionalData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.nomeProfissional.ToLower().Contains(filtro.ToLower()))
                          select u;
            }

            retorno = retorno.OrderBy(x => x.nomeProfissional);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Profissional>(Page, 10));
        }

        public ViewResult LookupProfissional( )
        {
            IEnumerable<Profissional> retorno = profissionalData.GetAll(contexto.idOrganizacao);

            retorno = retorno.OrderBy(x => x.nomeProfissional);           

            return View(retorno.ToPagedList<Profissional>(1, 10));
        }

        public ViewResult GridLookupProfissional(string filtro, int Page)
        {
            IEnumerable<Profissional> retorno = profissionalData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                (u.nomeProfissional.ToLower().Contains(filtro.ToLower()))
                          select u;
            }

            retorno = retorno.OrderBy(x => x.nomeProfissional);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Profissional>(Page, 10));
        }
    }
}