using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class AgendamentoRules
    {
        public static bool AgendamentoCreate(AgendamentoViewModel entrada,out Agendamento modelo, ContextPage contexto )
        {
            modelo = new Agendamento();
            modelo = entrada.agendamento;
            modelo.status = CustomEnumStatus.Status.Ativo;
            modelo.codigo = AutoNumber.GeraCodigo(24, contexto.idOrganizacao);
            modelo.idCliente = entrada.cliente.id;
                        
         

            if (entrada.agendamento.tipoReferencia == CustomEnum.TipoReferencia.OrdemServico) modelo.idReferencia = entrada.os.id;

            if (entrada.agendamento.tipoReferencia == CustomEnum.TipoReferencia.Pedido) modelo.idReferencia = entrada.pedido.id;

            if (entrada.agendamento.tipoReferencia == CustomEnum.TipoReferencia.Atendimento) modelo.idReferencia = entrada.atendimento.id;

            if (entrada.profissional != null) modelo.idProfissional = entrada.profissional.id;

            modelo.horaInicio = Convert.ToInt32(entrada.horaInicio.horaDia);
            modelo.horaFim = Convert.ToInt32(entrada.horaFim.horaDia);


            if (modelo.codigo != null)
            {
                //************ Objetos de controle de acesso ******************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool AtegendamentoUpdate(AgendamentoViewModel entrada,out Agendamento modelo)
        {
            modelo = new Agendamento();
            modelo = entrada.agendamento;

            if (entrada.cliente != null) modelo.idCliente = entrada.cliente.id;

            if (entrada.servico != null) modelo.idReferencia = entrada.servico.id;

            if (entrada.os != null) modelo.idReferencia = entrada.os.id;

            if (entrada.pedido != null) modelo.idReferencia = entrada.pedido.id;

            if (entrada.atendimento != null) modelo.idReferencia = entrada.atendimento.id;

            if (entrada.profissional != null) modelo.idProfissional = entrada.profissional.id;

            modelo.horaInicio = Convert.ToInt32(entrada.horaInicio.horaDia);
            modelo.horaFim = Convert.ToInt32(entrada.horaFim.horaDia);

            if (entrada.agendamento.statusAgendamento == CustomEnumStatus.StatusAgendamento.concluido || entrada.agendamento.statusAgendamento == CustomEnumStatus.StatusAgendamento.cancelado)
            {
                modelo.dataFechamento = DateTime.Now;
            }     
            
            //************ Objetos de controle de acesso *******************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }
        public static bool AtendimentoUpdateStatus(AgendamentoViewModel entrada, out Agendamento modelo)
        {
            modelo = new Agendamento();
            modelo = entrada.agendamento;                

            if (entrada.agendamento.statusAgendamento == CustomEnumStatus.StatusAgendamento.agendado)
            {
                modelo.dataFechamento = new DateTime ();
            }

            if (entrada.agendamento.statusAgendamento == CustomEnumStatus.StatusAgendamento.concluido || entrada.agendamento.statusAgendamento == CustomEnumStatus.StatusAgendamento.cancelado)
            {
                modelo.dataFechamento = DateTime.Now;
            }

            //************ Objetos de controle de acesso *******************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }
    }
}
