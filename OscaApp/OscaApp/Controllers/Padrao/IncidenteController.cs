using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaFramework.Models;
using OscaApp.ViewModels;
using OscaApp.RulesServices;
using OscaFramework.MicroServices;
using X.PagedList;
using OscaApp.framework;

namespace OscaApp.Controllers.Padrao
{
    public class IncidenteController : Controller
    {
        private readonly IIncidenteData IncidenteData;
        private ContextPage contexto;

        public IncidenteController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.IncidenteData = new IncidenteData(db);
        } // end of ctor

        [TempData]
        public string StatusMessage { get; set; }

        // Form create get
        [HttpGet]
        public ViewResult FormCreateIncidente()
        {
            IncidenteViewModel modelo = new IncidenteViewModel();
            modelo.Incidente = new Incidente();
            modelo.Contexto = contexto;
            modelo.Incidente.criadoEm = DateTime.Now;
            modelo.Incidente.criadoPorName = contexto.nomeUsuario;
            return View(modelo);
        } // end of method FormCreateIncidente

        [HttpPost]
        public IActionResult FormCreateIncidente(IncidenteViewModel entrada)
        {
            Incidente modelo = new Incidente();
            try
            {
                if (entrada.Incidente != null)
                {
                    if (IncidenteRules.IncidenteCreate(entrada, out modelo, contexto))
                    {
                        IncidenteData.Add(modelo);
                        return RedirectToAction("FormUpdateIncidente", new { id = modelo.id.ToString() });
                    } // end of if 2
                } // end of if 1
            } // end of try
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 11, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateIncidente-post", ex.Message);
                throw ex;
            } // end of catch
            return View();
        } // end of method FormCreateIncidente


        [HttpGet]
        public ViewResult FormUpdateIncidente(string id)
        {
            IncidenteViewModel modelo = new IncidenteViewModel();
            modelo.Incidente = new Incidente();
            modelo.Incidente.id = new Guid(id);
            modelo.Incidente.modificadoPorName = contexto.nomeUsuario;
            modelo.Incidente.modificadoEm = DateTime.Now;

            Incidente retorno = new Incidente();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = IncidenteData.Get(modelo.Incidente.id, contexto.idOrganizacao);
                modelo.Incidente = retorno;
                //apresenta mensagem de registro atualizado com sucesso
                modelo.StatusMessage = StatusMessage;
            } // end of if
            return View(modelo);
        } // end of FormUpdateIncidente

        [HttpPost]
        public IActionResult FormUpdateIncidente(IncidenteViewModel entrada)
        {
            Incidente modelo = new Incidente();
            entrada.Contexto = this.contexto;
            try
            {
                if (IncidenteRules.IncidenteUpdate(entrada, out modelo))
                {
                    IncidenteData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateIncidente", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                } // end of if
            } // end of try
            catch (Exception ex)
            {

                LogOsca log = new LogOsca();
                log.GravaLog(1, 11, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateIncidente-post", ex.Message);

            } // end of catch
            return RedirectToAction("FormUpdateIncidente", new { id = modelo.id.ToString() });
        } // end of method FormUpdateIncidente

        public ViewResult GridIncidente(string filtro, int Page)
        {
            IEnumerable<Incidente> retorno = IncidenteData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                            (u.codigo.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))
                            || (u.titulo.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))                       
                          select u;
            }
            retorno = retorno.OrderByDescending(x => x.codigo);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Incidente>(Page, 2));
        }

    } // end of class IncidenteController
} // end of namespace OscaApp.Controllers.Padrao
