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
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly ClienteData clienteData;
        private readonly EnderecoData enderecoData;
        private readonly OrdemServicoData ordemServicoData;
        private readonly PedidoData pedidoData;
        private readonly AtendimentoData atendimentoData;

        private readonly SqlGenericData sqlData;
        private ContextPage contexto;
       
        public ClienteController(ContexDataService db, IHttpContextAccessor httpContext, SqlGenericData _sqlData)
        {
            this.clienteData = new ClienteData(db);
            this.enderecoData = new EnderecoData(db);
            this.pedidoData = new PedidoData(db);
            this.atendimentoData = new AtendimentoData(db);

            this.ordemServicoData = new OrdemServicoData(db);
            this.contexto = new ContextPage().ExtractContext(httpContext);
            this.sqlData = _sqlData;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public bool StatusRetorno { get; set; }

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

        [HttpPost]
        public IActionResult FormCreateCliente(ClienteViewModel entrada)
        {

            Cliente modelo = new Cliente();
            entrada.contexto = contexto;

            try
            {
                if (entrada.cliente != null)
                {
                    if (ClienteRules.MontaClienteCreate(entrada, out modelo, contexto))
                    {
                        clienteData.Add(modelo);

                        return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString() });
                    }
                }
                else
                {

                    //Apresenta mensagem para o usuário
                    return RedirectToAction("ContexError", "CustomError", new { entityType = 1 });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateCliente-post", ex.Message);

            }
            return View();
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

                    modelo.contato = sqlData.RetornaRelacaoContato(retorno.idContato);
                    if (retorno != null)
                    {
                        modelo.cliente = retorno;

                        //Preenche informações do grid de Endereço
                        modelo.enderecos = enderecoData.GetByCliente(new Guid(id));
                        //Preenche informações do grid de Ordem de Servico
                        modelo.ordensServico = ordemServicoData.GetAllByIdCliente(new Guid(id));

                        //Preenche informações do grid de Pedido
                        modelo.pedidos = pedidoData.GetAllByIdCliente(new Guid(id));

                        //Preenche informações do grid de Atendiemento
                        modelo.atendimentos = atendimentoData.GetAllByIdCliente(new Guid(id));

                        //apresenta mensagem de cliente atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateCliente(ClienteViewModel entrada)
        {
            Cliente modelo = new Cliente();
            try
            {
                if (ClienteRules.MontaClienteUpdate(entrada, out modelo, this.contexto))
                {
                    clienteData.Update(modelo);                                        
                    StatusMessage = "Cliente Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString(), idOrg = contexto.idOrganizacao });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateCliente-post", ex.Message);
            }

            return RedirectToAction("FormUpdateCliente", new { id = modelo.id.ToString() });
        }

        public ViewResult GridCliente(string filtro, int Page)
        {
            try
            {
                IEnumerable<Cliente> retorno = clienteData.GetAll(contexto.idOrganizacao);

                if (!String.IsNullOrEmpty(filtro))
                {
                    retorno = from u in retorno
                              where (u.nomeCliente.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                    (u.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))
                              select u;

                }
                retorno = retorno.OrderBy(x => x.nomeCliente);

                if (Page == 0) Page = 1;

                return View(retorno.ToPagedList<Cliente>(Page, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }

        public ViewResult LookupCliente()
        {
            try
            {
                IEnumerable<Cliente> retorno = clienteData.GetAll(contexto.idOrganizacao);

                return View(retorno.ToPagedList<Cliente>(1, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }

        public ViewResult GridLookupCliente(string filtro, int Page)
        {
            try
            {
                IEnumerable<Cliente> retorno = clienteData.GetAll(contexto.idOrganizacao);

                if (!String.IsNullOrEmpty(filtro))
                {
                    retorno = from u in retorno
                              where (u.nomeCliente.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase)) ||
                                    (u.codigo.StartsWith(filtro,StringComparison.InvariantCultureIgnoreCase))
                              select u;

                }
                retorno = retorno.OrderBy(x => x.nomeCliente);

                //Se não passar a número da página, caregar a primeira
                if (Page == 0) Page = 1;

                return View(retorno.ToPagedList<Cliente>(Page, 10));

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 1, this.contexto.idUsuario, this.contexto.idOrganizacao, "Grid", ex.Message);
            }

            return View();
        }
    }
}
