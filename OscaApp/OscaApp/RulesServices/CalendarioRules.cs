using OscaFramework.MicroServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using OscaFramework.Models;
using OscaFramework.Helper;
using OscaApp.framework;
using OscaApp.Data;

namespace OscaApp.RulesServices
{
    public static class CalendarioRules
    {

        public static Calendario PreencheMes(int Mes, int Ano, SqlGeneric sqlServices, ContextPage contexto, string idProfissional)
        {


            Calendario retorno = new Calendario();
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dataFormat = culture.DateTimeFormat;

            int qtdDiasMes = DateTime.DaysInMonth(Ano, Mes);
            retorno.ano = Ano;
            retorno.mes = Mes;
            retorno.nomeMes = dataFormat.MonthNames[Mes - 1];
            retorno.qtdDias = qtdDiasMes;

            List<Dia> diasMEs = new List<Dia>();
            List<Agendamento> Agendamentos = sqlServices.RetornaAgendamentosMes(Mes.ToString(), Ano.ToString(), idProfissional, contexto.idOrganizacao.ToString());

            //Preenche todos o dias do Mes
            for (int i = 1; i <= qtdDiasMes; i++)
            {

                DateTime dataRef = new DateTime(Ano, Mes, i);
                Dia dia = new Dia();
                dia.dia = i;
                dia.nomeDia = dataFormat.GetDayName(dataRef.DayOfWeek);
                dia.itensCalendario = PreencheItemCalendario(i, Agendamentos);
                diasMEs.Add(dia);
            }
            retorno.dias = diasMEs;

            SqlGenericData sqlData = new SqlGenericData();
            retorno.profissionais = HelperAttributes.PreencheDropDownList(sqlData.RetornaTodasRelacaoProfissional(contexto.idOrganizacao));

            return retorno;
        }

        public static List<ItemCalendario> PreencheItemCalendario(int dia, List<Agendamento> agendamentos)
        {
            List<ItemCalendario> retorno = new List<ItemCalendario>();

            foreach (var item in agendamentos)
            {
                if (item.dataAgendada.Day == dia)
                {
                    ItemCalendario agendamento = new ItemCalendario();
                    agendamento.id = item.id.ToString();
                    agendamento.titulo = item.codigo;
                    agendamento.statusAgendamento = item.statusAgendamento;

                    agendamento.inicio = new ItemHoraDia();
                    agendamento.inicio.horaDia = (CustomEnum.itemHora)item.horaInicio;
                    agendamento.inicio.HoraFormatada = HelperCalendario.RetornaHoraFormatada(item.horaInicio);

                    agendamento.fim = new ItemHoraDia();
                    agendamento.fim.horaDia = (CustomEnum.itemHora)item.horaFim;
                    agendamento.fim.HoraFormatada = HelperCalendario.RetornaHoraFormatada(item.horaFim);

                    retorno.Add(agendamento);
                }
            }

            return retorno;
        }

        public static Dia PreencheDia( int Ano,  int Mes, int dia, SqlGeneric sqlServices, ContextPage contexto, string idProfissional)
        {
            string data = Ano + "-" + Mes + "- " + dia;

            Dia retorno = new Dia();
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTime dataRef = new DateTime(Ano, Mes, dia);
            DateTimeFormatInfo dataFormat = culture.DateTimeFormat;
            retorno.nomeDia = dataFormat.GetDayName(dataRef.DayOfWeek);
            retorno.dia = dia;
            retorno.ano = Ano;
            retorno.nomeMes = dataFormat.MonthNames[Mes - 1];

            SqlGenericData sqldata = new SqlGenericData();
            IEnumerable<Agendamento> Atendimentos = sqlServices.RetornaAgendamentosDia(data, idProfissional, contexto.idOrganizacao.ToString());
           
            List<ItemCalendario> lancamentos = new List<ItemCalendario>();
          

                foreach (var item in Atendimentos)
                {
                  
                        ItemCalendario hoc = new ItemCalendario();
                        hoc.id = item.id.ToString();
                        hoc.codigo = item.codigo;
                        hoc.inicio = new ItemHoraDia();
                        hoc.inicio.horaDia = (CustomEnum.itemHora)item.horaInicio;
                        hoc.fim = new ItemHoraDia();
                        hoc.fim.horaDia = (CustomEnum.itemHora)item.horaFim;
                     
                        hoc.cliente = sqldata.RetornaRelacaoCliente(item.idCliente).idName;
                        hoc.tipo = item.tipoReferencia.ToString();

                        if (item.tipoReferencia == CustomEnum.TipoReferencia.OrdemServico)
                        {
                            hoc.referencia = sqldata.RetornaRelacaoOrdemServico(item.idReferencia).codigo;
                        }
                        if (item.tipoReferencia == CustomEnum.TipoReferencia.Servico)
                        {
                            hoc.referencia = sqldata.RetornaRelacaoServico(item.idReferencia).codigo;
                        } 

                        if (item.tipoReferencia == CustomEnum.TipoReferencia.Atendimento)
                        {
                            hoc.referencia = "";
                        }

                        lancamentos.Add(hoc);                      
             
            }

            retorno.itensCalendario = lancamentos;


            return retorno;
        }
    }
}
