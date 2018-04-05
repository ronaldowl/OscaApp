using Microsoft.AspNetCore.Http;
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
using OscaApp.framework;
using OscaFramework.Models;


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

            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [TempData]
        public string StatusMessage { get; set; }

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

        [HttpPost]
        public IActionResult FormCreateEndereco(EnderecoViewModel entrada, string idCliente, string idClienteName)
        {
            Endereco modelo = new Endereco();

            try
            {
                if (entrada.endereco != null)
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
                LogOsca log = new LogOsca();
                log.GravaLog(1, 9, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateEndereco-post", ex.Message);
            }
            return View();
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
        public IActionResult FormUpdateEndereco(EnderecoViewModel entrada)
        {
            Endereco modelo = new Endereco();
            entrada.contexto = contexto;

            try
            {
                if (EnderecoRules.MontaEnderecoUpdate(entrada, out modelo))
                {

                    enderecoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateEndereco", new { id = modelo.id.ToString() });
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 9, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateEndereco-post", ex.Message);
            }

            return RedirectToAction("FormUpdateEndereco", new { id = modelo.id.ToString() });
        }

        public ViewResult GridEndereco(int Page, string idCliente)
        {
            IEnumerable<Endereco> retorno = enderecoData.GetAllByIdClinte(new Guid(idCliente));

            retorno = retorno.OrderBy(x => x.logradouro);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Endereco>(Page, 10));
        }
    }
}
