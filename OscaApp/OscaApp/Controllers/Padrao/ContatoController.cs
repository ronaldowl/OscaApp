﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaApp.Models;
using OscaApp.Data;
using X.PagedList;
using Microsoft.AspNetCore.Http;
using OscaApp.ViewModels;
using OscaApp.RulesServices;
using Microsoft.AspNetCore.Authorization;
using OscaApp.framework;
using OscaFramework.Models;
using OscaApp.Services;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ContatoController: Controller
    {
        private readonly ContatoData contatoData;   

        private readonly ContextPage contexto;
        

        public ContatoController(ContexDataService db, IHttpContextAccessor httpContext)
        {            
            this.contatoData = new ContatoData(db);           
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }
  

        [HttpGet]
        public ViewResult FormCreateContato()
        {
            ContatoViewModel modelo = new ContatoViewModel();
            modelo.contato = new Contato();
            modelo.contexto = contexto;
            modelo.contato.criadoEm = DateTime.Now;
            modelo.contato.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateContato(ContatoViewModel entrada)
        {

            Contato modelo = new Contato();
            entrada.contexto = contexto;         

            try
            {
                if (entrada.contato != null)
                {
                    if (ContatoRules.MontaContatoCreate(entrada, out modelo, contexto))
                    {
                        contatoData.Add(modelo);

                        return RedirectToAction("FormUpdateContato", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 2, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateContato-post", ex.Message);
            }
            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateContato(string id)
        {
            ContatoViewModel modelo = new ContatoViewModel();

            Contato retorno = new Contato();
            //Formulario com os dados do cliente
            if (!String.IsNullOrEmpty(id))
            {
                //campo que sempre contém valor
                retorno = contatoData.Get(new Guid(id));

                if (retorno != null)
                {
                    modelo.contato = retorno;

                    //apresenta mensagem de registro atualizado com sucesso
                    modelo.StatusMessage = StatusMessage;
                }
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateContato(ContatoViewModel entrada)
        {
            Contato modelo = new Contato();
            entrada.contexto = contexto;


            try
            {
                if (ContatoRules.MontaContatoUpdate(entrada, out modelo))
                {
                    contatoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateContato", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 2, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateContato-post", ex.Message);
            }

            return RedirectToAction("FormUpdateContato", new { id = modelo.id.ToString() });
        }

        public ViewResult GridContato(string filtro, int Page)
        {
            IEnumerable<Contato> retorno = contatoData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where (u.nome.ToLower().Contains(filtro.ToLower()) || u.email.ToLower().Contains(filtro.ToLower()))
                          select u;
            }
            retorno = retorno.OrderBy(x => x.nome);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Contato>(Page, 20));
        }

        public ActionResult GridLookupContato(string filtro, int Page)
        {
            IEnumerable<Contato> retorno = contatoData.GetAll(contexto.idOrganizacao);

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where   (u.nome.ToLower().Contains(filtro.ToLower()) || u.email.ToLower().Contains(filtro.ToLower()))                            
                          select u;
            }
            retorno = retorno.OrderBy(x => x.nome);

            if (Page == 0) Page = 1;

            return PartialView(retorno.ToPagedList<Contato>(Page, 10));
        }
        public ViewResult LookupContato()
        {
            IEnumerable<Contato> retorno = contatoData.GetAll(contexto.idOrganizacao); 

            return View(retorno.ToPagedList<Contato>(1,10));
        }
    }
}
