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
            retorno.nomeMes = dataFormat.MonthNames[Mes -1];
            retorno.qtdDias = qtdDiasMes;

            List<Dia> diasMEs = new List<Dia>();
            List<Atendimento> Atendimentos = sqlServices.RetornaAtendimentosMes(Mes.ToString(), Ano.ToString(), idProfissional, contexto.idOrganizacao.ToString());

            //Preenche todos o dias do Mes
            for (int i = 1; i <= qtdDiasMes; i++)
            {

                DateTime dataRef = new DateTime(Ano, Mes, i);
                Dia dia = new Dia();
                dia.dia = i;
                dia.nome = dataFormat.GetDayName(dataRef.DayOfWeek);
                dia.itensCalendario = PreencheItemCalendario(i, Atendimentos);
                diasMEs.Add(dia);
            }
            retorno.dias = diasMEs;

            SqlGenericData sqlData = new SqlGenericData();
            retorno.profissionais = HelperAttributes.PreencheDropDownList(sqlData.RetornaRelacaoProfissional( contexto.idOrganizacao));

            return retorno;
        }      
               

        public static List<ItemCalendario> PreencheItemCalendario(int dia, List<Atendimento>  atendimentos)
        {
            List<ItemCalendario> retorno = new List<ItemCalendario>();

            foreach (var item in atendimentos)
            {
                if(item.dataAgendada.Day == dia)
                {
                    ItemCalendario atendimento = new ItemCalendario();
                    atendimento.id = item.id.ToString();
                    atendimento.titulo = item.codigo;
                    atendimento.statusAtendimento = item.statusAtendimento;
                    
                    atendimento.inicio = new ItemHoraDia();
                    atendimento.inicio.horaDia = (CustomEnum.itemHora)item.horaInicio;
                    atendimento.inicio.HoraFormatada = HelperCalendario.RetornaHoraFormatda(item.horaInicio);

                    atendimento.fim = new ItemHoraDia();
                    atendimento.fim.horaDia = (CustomEnum.itemHora)item.horaFim;
                    atendimento.fim.HoraFormatada = HelperCalendario.RetornaHoraFormatda(item.horaFim);

                    retorno.Add(atendimento);
                }
            }

            return retorno;
        }
    }
}
