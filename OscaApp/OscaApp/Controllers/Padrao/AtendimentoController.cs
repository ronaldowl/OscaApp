using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaApp.ViewModels;
using OscaApp.Data;
using Microsoft.AspNetCore.Http;
using OscaApp.RulesServices;
using OscaApp.framework;
using OscaFramework.MicroServices;
using X.PagedList;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Controllers
{
    public class AtendimentoController : Controller
    {
        private ContextPage contexto;
        public AtendimentoData atendimentoData;
        private readonly SqlGenericData sqlData;

        public AtendimentoController(ContexDataService db, IHttpContextAccessor httpContext, SqlGenericData _sqlData)
        {
            this.atendimentoData = new AtendimentoData(db);
            this.sqlData = _sqlData;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public ViewResult FormCreateAtendimento(string idCliente)
        {
            SqlGeneric sqlServices = new SqlGeneric();
            SqlGenericData sqlData = new SqlGenericData();
            
            AtendimentoViewModel modelo = new AtendimentoViewModel();
            modelo.contexto = contexto;
            modelo.atendimento = new Atendimento();
            modelo.atendimento.status = CustomEnumStatus.Status.Ativo;

            modelo.atendimento.criadoEm = DateTime.Now;
            modelo.atendimento.criadoPorName = contexto.nomeUsuario;

            //Se passar o id carrega o cliente
            if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = sqlData.RetornaRelacaoCliente(new Guid(idCliente));


            try
            {
                modelo.profissional = sqlData.RetornaRelacaoProfissional(new Guid(sqlServices.RetornaidProfissionalPorIdUsuario(contexto.idUsuario.ToString())));
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateAtendimento-get", ex.Message);

            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult FormCreateAtendimento(AtendimentoViewModel entrada)
        {
            Atendimento modelo = new Atendimento();

            try
            {
                if (entrada.atendimento != null)
                {

                    if (AtendimentoRules.AtendimentoCreate(entrada, out modelo, this.contexto))
                    {
                        //Se retorna true grava no banco
                        atendimentoData.Add(modelo);

                        return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateAtendimento-post", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateAtendimento(string id)
        {
            AtendimentoViewModel modelo = new AtendimentoViewModel();

            try
            {
                Atendimento retorno = new Atendimento();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = atendimentoData.Get(new Guid(id));

                    modelo.cliente = sqlData.RetornaRelacaoCliente(retorno.idCliente);
                    modelo.profissional = sqlData.RetornaRelacaoProfissional(retorno.idProfissional);

                    modelo.servico = sqlData.RetornaRelacaoServico(retorno.idServico);

                    if (!String.IsNullOrEmpty(sqlData.RetornaRelacaoAgendamentoByIdReferencia(retorno.id).codigo))
                    {
                        modelo.idAgendamento = sqlData.RetornaRelacaoAgendamentoByIdReferencia(retorno.id).id.ToString();
                    }
             
                    if (retorno != null)
                    {
                        modelo.atendimento = retorno;
                        //apresenta mensagem de registro atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateAtendimento-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateAtendimento(AtendimentoViewModel entrada)
        {
            Atendimento modelo = new Atendimento();
            entrada.contexto = this.contexto;
            try
            {
                if (AtendimentoRules.AtendimentoUpdate(entrada, out modelo))
                {
                    atendimentoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateAtendimento-post", ex.Message);
            }

            return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
        }

        public ViewResult GridAtendimento(string filtro, int Page, string idCliente, int view)
        {
            IEnumerable<AtendimentoGridViewModel> retorno;

            SqlGeneric sqlServices = new SqlGeneric();
            string idProfissional = sqlServices.RetornaidProfissionalPorIdUsuario(contexto.idUsuario.ToString());


            if (String.IsNullOrEmpty(idCliente))
            {
                //Se tiver filtro, busca em todas as linhas
                if (!String.IsNullOrEmpty(filtro)) view = 4;

                retorno = atendimentoData.GetAllGridViewModel(contexto.idOrganizacao, view, idProfissional);
            }
            else
            {

                retorno = atendimentoData.GetAllGridViewModelByCliente(new Guid(idCliente));
            }

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                            (u.atendimento.titulo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))
                            || (u.atendimento.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))
                          select u;
            }
            retorno = retorno.OrderByDescending(A => A.atendimento.dataAgendada);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<AtendimentoGridViewModel>(Page, 10));
        }


        [HttpGet]
        public ViewResult FormStatusAtendimento(string id)
        {
            AtendimentoViewModel modelo = new AtendimentoViewModel();
            modelo.contexto = this.contexto;

            try
            {
                Atendimento retorno = new Atendimento();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = atendimentoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.atendimento = retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusAtendimento-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormStatusAtendimento(AtendimentoViewModel entrada)
        {
            Atendimento modelo = new Atendimento();
            entrada.contexto = this.contexto;

            try
            {
                if (AtendimentoRules.AtendimentoUpdateStatus(entrada, out modelo))
                {
                    atendimentoData.UpdateStatus(modelo);

                    return RedirectToAction("FormUpdateAtendimento", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusAtendimento-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridAtendimentoDia()
        {
            SqlGeneric sqlServices = new SqlGeneric();
            string idProfissional = sqlServices.RetornaidProfissionalPorIdUsuario(contexto.idUsuario.ToString());

            IEnumerable<AtendimentoGridViewModel> retorno = atendimentoData.GetAllGridViewModelDia(new Guid(idProfissional));

            return View(retorno.ToPagedList<AtendimentoGridViewModel>(1, 100));
        }

    }
}
