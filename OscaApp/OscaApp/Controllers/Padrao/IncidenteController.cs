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

namespace OscaApp.Controllers.Padrao
{
    public class IncidenteController : Controller
    {
        private readonly IIncidenteData modeloData;
        private ContextPage contexto;

        public IncidenteController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.modeloData = new IncidenteData(db);
        } // end of ctor

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
                        modeloData.Add(modelo);
                        return RedirectToAction("FormUpdateIncidente", new { id = modelo.id.ToString() });
                    } // end of if 2
                } // end of if 1
            } // end of try
            catch (Exception ex)
            {
                //LogOsca log = new LogOsca();
                //log.GravaLog(1, 22, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateRecurso-post", ex.Message);
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

            Incidente retorno = new Incidente();

            if (!String.IsNullOrEmpty(id))
            {
                retorno = modeloData.Get(modelo.Incidente.id, contexto.idOrganizacao);
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
                    modeloData.Update(modelo);
                    return RedirectToAction("FormUpdateIncidente", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                } // end of if
            } // end of try
            catch (Exception ex)
            {
                throw ex;
            } // end of catch
            return RedirectToAction("FormUpdateIncidente", new { id = modelo.id.ToString() });
        } // end of method FormUpdateIncidente

    } // end of class IncidenteController
} // end of namespace OscaApp.Controllers.Padrao
