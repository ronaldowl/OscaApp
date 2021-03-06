﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public class AgendamentoController : Controller
    {
        private ContextPage contexto;
        public AgendamentoData agendamentoData;
        private readonly SqlGenericData sqlData;

        public AgendamentoController(ContexDataService db, IHttpContextAccessor httpContext, SqlGenericData _sqlData)
        {
            this.agendamentoData = new AgendamentoData(db);
            this.sqlData = _sqlData;
            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public int tempTipoReferencia { get; set; }

        [HttpGet]
        public ViewResult FormCreateAgendamento(string idCliente, int tipoReferencia, string idReferencia, string idProfissional)
        {
            AgendamentoViewModel modelo = new AgendamentoViewModel();
            try
            {
                SqlGeneric sqlServices = new SqlGeneric();
                SqlGenericData sqlData = new SqlGenericData();

                tempTipoReferencia = tipoReferencia;
                modelo.contexto = contexto;
                modelo.agendamento = new Agendamento();
                modelo.agendamento.status = CustomEnumStatus.Status.Ativo;

                modelo.agendamento.criadoEm = DateTime.Now;
                modelo.agendamento.criadoPorName = contexto.nomeUsuario;

                //Se passar o id carrega o cliente
                if (!String.IsNullOrEmpty(idCliente)) modelo.cliente = sqlData.RetornaRelacaoCliente(new Guid(idCliente));

                if (!String.IsNullOrEmpty(idReferencia))
                {
                    if (tipoReferencia == (int)CustomEnum.TipoReferencia.Atendimento)
                    {
                        modelo.agendamento.tipoReferencia = CustomEnum.TipoReferencia.Atendimento;
                        modelo.atendimento = sqlData.RetornaRelacaoAtendimento(new Guid(idReferencia));
                    }

                    if (tipoReferencia == (int)CustomEnum.TipoReferencia.OrdemServico)
                    {
                        modelo.agendamento.tipoReferencia = CustomEnum.TipoReferencia.OrdemServico;
                        modelo.os = sqlData.RetornaRelacaoOrdemServico(new Guid(idReferencia));
                    }

                    if (tipoReferencia == (int)CustomEnum.TipoReferencia.Pedido)
                    {
                        modelo.agendamento.tipoReferencia = CustomEnum.TipoReferencia.Pedido;
                        modelo.pedido = sqlData.RetornaRelacaoPedido(new Guid(idReferencia));
                    }
                }


                modelo.profissional = sqlData.RetornaRelacaoProfissional(new Guid( idProfissional));
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateAgendamento-get", ex.Message);

            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult FormCreateAgendamento(AgendamentoViewModel entrada)
        {
            Agendamento modelo = new Agendamento();

            try
            {
                if (entrada.agendamento != null)
                {
                    entrada.agendamento.tipoReferencia = (CustomEnum.TipoReferencia)tempTipoReferencia;

                    if (AgendamentoRules.AgendamentoCreate(entrada, out modelo, this.contexto))
                    {
                        //Se retorna true grava no banco
                        agendamentoData.Add(modelo);

                        return RedirectToAction("FormUpdateAgendamento", new { id = modelo.id.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormCreateAgendamento-post", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ViewResult FormUpdateAgendamento(string id)
        {
            AgendamentoViewModel modelo = new AgendamentoViewModel();

            try
            {
                Agendamento retorno = new Agendamento();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = agendamentoData.Get(new Guid(id));

                    modelo.cliente = sqlData.RetornaRelacaoCliente(retorno.idCliente);
                    modelo.profissional = sqlData.RetornaRelacaoProfissional(retorno.idProfissional);


                    modelo.horaInicio = new ItemHoraDia();
                    modelo.horaInicio.horaDia = (CustomEnum.itemHora)retorno.horaInicio;
                    modelo.horaFim = new ItemHoraDia();
                    modelo.horaFim.horaDia = (CustomEnum.itemHora)retorno.horaFim;

                    if (retorno != null)
                    {
                        modelo.agendamento = retorno;
                        //modelo.agendamento.tipoReferencia = new CustomEnum.TipoReferencia();

                        if (retorno.tipoReferencia == CustomEnum.TipoReferencia.Atendimento)
                        {
                            modelo.atendimento = sqlData.RetornaRelacaoAtendimento(modelo.agendamento.idReferencia);
                        }

                        if (retorno.tipoReferencia == CustomEnum.TipoReferencia.OrdemServico)
                        {
                            modelo.os = sqlData.RetornaRelacaoOrdemServico(modelo.agendamento.idReferencia);
                        }

                        if (retorno.tipoReferencia == CustomEnum.TipoReferencia.Pedido)
                        {
                            modelo.pedido = sqlData.RetornaRelacaoPedido(modelo.agendamento.idReferencia);
                        }
                        //apresenta mensagem de registro atualizado com sucesso
                        modelo.StatusMessage = StatusMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateAgendamento-get", ex.Message);

            }

            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormUpdateAgendamento(AgendamentoViewModel entrada)
        {
            Agendamento modelo = new Agendamento();
            entrada.contexto = this.contexto;
            try
            {
                if (AgendamentoRules.AtegendamentoUpdate(entrada, out modelo))
                {
                    agendamentoData.Update(modelo);
                    StatusMessage = "Registro Atualizado com Sucesso!";

                    return RedirectToAction("FormUpdateAgendamento", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormUpdateAgendamento-post", ex.Message);
            }

            return RedirectToAction("FormUpdateAgendamento", new { id = modelo.id.ToString() });
        }

        public ViewResult GridAgendamento(string filtro, int Page, string idCliente, int view)
        {
            IEnumerable<AgendamentoGridViewModel> retorno;

            ViewBag.viewContexto = view;

            SqlGeneric sqlServices = new SqlGeneric();
            string idProfissional = sqlServices.RetornaidProfissionalPorIdUsuario(contexto.idUsuario.ToString());


            if (String.IsNullOrEmpty(idCliente))
            {

                retorno = agendamentoData.GetAllGridViewModel(contexto.idOrganizacao, view, idProfissional);
            }
            else
            {

                retorno = agendamentoData.GetAllGridViewModelByCliente(new Guid(idCliente));
            }

            if (!String.IsNullOrEmpty(filtro))
            {
                retorno = from u in retorno
                          where
                            (u.agendamento.codigo.StartsWith(filtro, StringComparison.InvariantCultureIgnoreCase))
                          select u;
            }
            retorno = retorno.OrderByDescending(A => A.agendamento.dataAgendada);

            //Se não passar a número da página, caregar a primeira
            if (Page == 0) Page = 1;

            return View(retorno.ToPagedList<AgendamentoGridViewModel>(Page, 20));
        }


        [HttpGet]
        public ViewResult FormStatusAgendamento(string id)
        {
            AgendamentoViewModel modelo = new AgendamentoViewModel();
            modelo.contexto = this.contexto;

            try
            {
                Agendamento retorno = new Agendamento();

                if (!String.IsNullOrEmpty(id))
                {
                    //campo que sempre contém valor
                    retorno = agendamentoData.Get(new Guid(id));

                    if (retorno != null)
                    {
                        modelo.agendamento = retorno;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusAgendamento-get", ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public IActionResult FormStatusAgendamento(AgendamentoViewModel entrada)
        {
            Agendamento modelo = new Agendamento();
            entrada.contexto = this.contexto;

            try
            {
                if (AgendamentoRules.AtendimentoUpdateStatus(entrada, out modelo))
                {
                    agendamentoData.UpdateStatus(modelo);

                    return RedirectToAction("FormUpdateAgendamento", new { id = modelo.id.ToString() });
                }
            }
            catch (Exception ex)
            {
                LogOsca log = new LogOsca();
                log.GravaLog(1, 3, this.contexto.idUsuario, this.contexto.idOrganizacao, "FormStatusAgendamento-post", ex.Message);
            }
            return View();
        }

        public ViewResult GridAgendamentoDia()
        {
            SqlGeneric sqlServices = new SqlGeneric();
            string idProfissional = sqlServices.RetornaidProfissionalPorIdUsuario(contexto.idUsuario.ToString());

            IEnumerable<AgendamentoGridViewModel> retorno = agendamentoData.GetAllGridViewModelDia(new Guid(idProfissional));

            retorno = from u in retorno
                      where
                        (u.agendamento.dataAgendada.Date == DateTime.Now.Date)

                      select u;

            retorno = retorno.OrderByDescending(x => x.agendamento.dataAgendada);

            return View(retorno.ToPagedList<AgendamentoGridViewModel>(1, 10));
        }

    }
}
