﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OscaApp.framework.Models;
using OscaApp.Data; 
using OscaApp.Models;
using OscaApp.RulesServices;
using OscaApp.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace OscaApp.Controllers
{
    [Authorize]
    public class EnderecoController : Controller
    {
     
        private readonly IEnderecoData enderecoData;
        private ContextPage contexto;

        public EnderecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
           
            this.enderecoData = new EnderecoData(db);
          
            this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));

        }

        public ViewResult GridEndereco(string filtro, int Page)
        {
            IEnumerable<Endereco> retorno = enderecoData.GetAll(contexto.idOrganizacao);

            //realiza busca por Nome, Código, Email e CPF
            if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.idClienteName == filtro || A.logradouro == filtro) select A;

            retorno = retorno.OrderBy(x => x.logradouro);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Endereco>(Page, 10));
        }

        [HttpPost]
        public IActionResult FormUpdateEndereco(EnderecoViewModel entrada)
        {
            Endereco modelo = new Endereco();
            entrada.contexto = contexto;

            try
            {
                if (EnderecoRules.MontaEnderecoUpdate(entrada, out modelo))
                {
                   
                    enderecoData.Update(modelo);
                    return RedirectToAction("FormUpdateEndereco", new { id = modelo.id.ToString() });
                }

            }
            catch (Exception ex)
            {
                //TODO: Gravar exceção no LOG
            }

            return RedirectToAction("FormUpdateEndereco", new { id = modelo.id.ToString() });
        }

        [HttpGet]
        public ViewResult FormUpdateEndereco(string id)
        {
            EnderecoViewModel modelo = new EnderecoViewModel();           


            if (!String.IsNullOrEmpty(id))
            {
                modelo.endereco = enderecoData.Get(new Guid(id));
            }
                return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateEndereco(EnderecoViewModel entrada, string idCliente, string idClienteName)
        {
            Endereco modelo = new Endereco();                
                    
            try
            {
                if (entrada.endereco.logradouro != null)
                {
                    if (EnderecoRules.MontaEnderecoCreate(entrada, out modelo, contexto))
                    {
                        modelo.idCliente = new Guid(idCliente);
                        modelo.idClienteName = idClienteName;

                        enderecoData.Add(modelo);

                        return RedirectToAction("FormUpdateEndereco", new { id = modelo.id.ToString() });
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
        public ViewResult FormCreateEndereco(string idCliente, string NomeCliente)
        {
            EnderecoViewModel modelo = new EnderecoViewModel();
            modelo.endereco = new Endereco();
            try
            {
            
            modelo.contexto = contexto;          
            modelo.endereco.idCliente = new Guid(idCliente);
            modelo.endereco.idClienteName = NomeCliente;      

            }
            catch (Exception)
            {

                throw;
            }

            return View(modelo);
        }
    }
}
