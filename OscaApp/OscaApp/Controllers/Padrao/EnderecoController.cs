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
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoData enderecoData;
        private ContextPage contexto;
        private readonly SqlGenericData Sqlservice;

        public EnderecoController(ContexDataService db, IHttpContextAccessor httpContext)
        {
           
            this.enderecoData = new EnderecoData(db);
            this.Sqlservice = new SqlGenericData();
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);

        }

        [TempData]
        public string StatusMessage { get; set; }
        public string idCliente { get; set; }


        [HttpGet]
        public ViewResult FormCreateEndereco(string idCliente, string NomeCliente)
        {
            EnderecoViewModel modelo = new EnderecoViewModel();
            modelo.endereco = new Endereco();
            try
            {
             Relacao cliente = Sqlservice.RetornaRelacaoCliente(new Guid(idCliente));
             modelo.contexto = contexto;          
            modelo.endereco.idCliente = cliente.id;
            modelo.endereco.idClienteName = cliente.idName;      

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
                //apresenta mensagem de registro atualizado com sucesso
                modelo.StatusMessage = StatusMessage;
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

            ViewBag.idcliente = idCliente;
            retorno = retorno.OrderBy(x => x.logradouro);          

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Endereco>(Page, 10));
        }

        public ViewResult LookupEndereco(int Page, string idCliente)
        {
            IEnumerable<Endereco> retorno = enderecoData.GetAllByIdClinte(new Guid(idCliente));

            ViewBag.idcliente = idCliente;
            retorno = retorno.OrderBy(x => x.logradouro);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Endereco>(Page, 10));
        }
        public ViewResult LookupEndereco2(int Page, string idCliente)
        {
            IEnumerable<Endereco> retorno = enderecoData.GetAllByIdClinte(new Guid(idCliente));

            ViewBag.idcliente = idCliente;
            retorno = retorno.OrderBy(x => x.logradouro);

            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<Endereco>(Page, 10));
        }
    }
}
