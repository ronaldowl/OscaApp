using System;
using Microsoft.AspNetCore.Mvc;
using OscaApp.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using OscaApp.RulesServices;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using OscaApp.framework;
using OscaFramework.Models;


namespace OscaApp.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ClienteData clienteData;
        private readonly EnderecoData enderecoData;
        private ContextPage contexto;


        public ClienteController(ContexDataService db, IHttpContextAccessor httpContext)
        {
            this.clienteData = new ClienteData(db);
            this.enderecoData = new EnderecoData(db);
            // this.contexto = new ContextPage(httpContext.HttpContext.Session.GetString("email"), httpContext.HttpContext.Session.GetString("organizacao"));
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        public ViewResult GridCliente(string filtro, int Page)
        {
            try
            {
                IEnumerable<Cliente> retorno = clienteData.GetAll(contexto.idOrganizacao);

                //realiza busca por Nome, Código, Email e CPF
                if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.codigo == filtro || A.nomeCliente == filtro || A.cnpj_cpf == filtro || A.email == filtro) select A;

                retorno = retorno.OrderBy(x => x.nomeCliente);

                //Se não passar a número da página, caregar a primeira
                if (Page == 0) Page = 1;

                return View(retorno.ToPagedList<Cliente>(Page, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,1, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }

        public ViewResult LookupCliente(string filtro, int Page)
        {
            try
            {
                IEnumerable<Cliente> retorno = clienteData.GetAll(contexto.idOrganizacao);

                //realiza busca por Nome, Código, Email e CPF
                if (!String.IsNullOrEmpty(filtro)) retorno = from A in retorno where (A.codigo == filtro || A.nomeCliente == filtro || A.cnpj_cpf == filtro || A.email == filtro) select A;

                retorno = retorno.OrderBy(x => x.nomeCliente);

                //Se não passar a número da página, caregar a primeira
                if (Page == 0) Page = 1;

                return View(retorno.ToPagedList<Cliente>(Page, 5));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }


        [HttpPost]
        public IActionResult FormUpdateCliente(ClienteViewModel entrada)
        {
            Cliente modelo = new Cliente();
            entrada.contexto = this.contexto;
            try
            {
                if (ClienteRules.MontaClienteUpdate(entrada, out modelo))
                {
                    clienteData.Update(modelo);
                    return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-post", ex.Message);
            }

            return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString() });
        }
        [HttpGet]
        public ViewResult FormUpdateCliente(string id)
        {
            ClienteViewModel modelo = new ClienteViewModel();

            try
            {
                Cliente retorno = new Cliente();
                //Formulario com os dados do cliente
                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = clienteData.Get(new Guid(id), contexto.idOrganizacao);

                    if (retorno != null)
                    {
                        modelo.cliente = retorno;

                        //Preenche informações do grid de Endereço
                        modelo.enderecos = enderecoData.GetByCliente(modelo.cliente.id);

                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormCreateCliente(ClienteViewModel entrada)
        {

            Cliente modelo = new Cliente();
            entrada.contexto = contexto;

            try
            {
                if (entrada.cliente.nomeCliente != null)
                {
                    if (ClienteRules.MontaClienteCreate(entrada, out modelo, contexto))
                    {
                        clienteData.Add(modelo);

                        return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1,1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateCliente", ex.Message);

            }
            return View();
        }
        [HttpGet]
        public ViewResult FormCreateCliente()
        {
            ClienteViewModel modelo = new ClienteViewModel();
            modelo.cliente = new Cliente();
            modelo.contexto = contexto;
            modelo.cliente.criadoEm = DateTime.Now;
            modelo.cliente.criadoPorName = contexto.nomeUsuario;

            return View(modelo);
        }
    }
}
