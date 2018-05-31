using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.framework;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace OscaApp.Controllers.Padrao
{
    public class PerfilAcessoController : Controller
    {
        private readonly PerfilAcessoData perfilAcessoData;
        private ContextPage contexto;

        public PerfilAcessoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.perfilAcessoData = new PerfilAcessoData(db);
        } // end of ctor

        [TempData]
        public string StatusMessage { get; set; }

        // Form create get
        [HttpGet]
        public ViewResult FormCreatePerfilAcesso()
        {
            PerfilAcessoViewModel modelo = new PerfilAcessoViewModel();
            modelo.perfilAcesso = new PerfilAcesso();
            modelo.Contexto = contexto;
            modelo.perfilAcesso.criadoEm = DateTime.Now;
            modelo.perfilAcesso.criadoPorName = contexto.nomeUsuario;
            return View(modelo);
        }  

        [HttpPost]
        public IActionResult FormCreatePerfilAcesso(PerfilAcessoViewModel entrada)
        {
            PerfilAcesso modelo = new PerfilAcesso();
            try
            {
                if (entrada.perfilAcesso != null)
                {
                    if (PerfilAcessoRules.PerfilAcessoCreate(entrada, out modelo, contexto))
                    {
                        perfilAcessoData.Add(modelo);
                        return RedirectToAction("FormUpdatePerfilAcesso", new { id = modelo.id.ToString() }); 
                    } // end of if 2
                } // end of if 1
            } // end of try
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 11, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreatePerfilAcesso-post", ex.Message);
                throw ex;
            } // end of catch
            return View();
        }  


        [HttpGet]
        public ViewResult FormUpdatePerfilAcesso(string id)
        {
            PerfilAcessoViewModel modelo = new PerfilAcessoViewModel();
            modelo.perfilAcesso = new PerfilAcesso();
            modelo.perfilAcesso.id = new Guid(id);
            modelo.perfilAcesso.modificadoPorName = contexto.nomeUsuario;
            modelo.perfilAcesso.modificadoEm = DateTime.Now;

            PerfilAcesso retorno = new PerfilAcesso();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = perfilAcessoData.Get(modelo.perfilAcesso.id);
                modelo.perfilAcesso = retorno;
                //apresenta mensagem de registro atualizado com sucesso
                modelo.StatusMessage = StatusMessage;
            } // end of if
            return View(modelo);
        } 

        [HttpPost]
        public IActionResult FormUpdatePerfilAcesso(PerfilAcessoViewModel entrada)
        {
            PerfilAcesso modelo = new PerfilAcesso();
            entrada.Contexto = this.contexto;
            try
            {
                if (PerfilAcessoRules.PerfilAcessoUpdate(entrada, out modelo))
                {
                    perfilAcessoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdatePerfilAcesso", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                } // end of if
            } // end of try
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 11, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdatePerfilAcesso-post", ex.Message);

            } // end of catch
            return RedirectToAction("FormUpdatePerfilAcesso", new { id = modelo.id.ToString() });
        }  

        public ViewResult GridPerfilAcesso(string filtro, int Page)
        {
            IEnumerable<PerfilAcesso> retorno = perfilAcessoData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                            (u.nome.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))                       
                          select u;
            }
            retorno = retorno.OrderByDescending(x => x.nome);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<PerfilAcesso>(Page, 20));
        }

    }  
} // end of namespace OscaApp.Controllers.Padrao
