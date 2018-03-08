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
            List<Atendimento> Atendimentos = sqlServices.RetornaAtendimentosMes(Mes.ToString(), Ano.ToString(), idProfissional, contexto.idOrganizacao.ToString());

            //Preenche todos o dias do Mes
            for (int i = 1; i <= qtdDiasMes; i++)
            {

                DateTime dataRef = new DateTime(Ano, Mes, i);
                Dia dia = new Dia();
                dia.dia = i;
                dia.nomeDia = dataFormat.GetDayName(dataRef.DayOfWeek);
                dia.itensCalendario = PreencheItemCalendario(i, Atendimentos);
                diasMEs.Add(dia);
            }
            retorno.dias = diasMEs;

            SqlGenericData sqlData = new SqlGenericData();
            retorno.profissionais = HelperAttributes.PreencheDropDownList(sqlData.RetornaTodasRelacaoProfissional(contexto.idOrganizacao));

            return retorno;
        }


        public static List<ItemCalendario> PreencheItemCalendario(int dia, List<Atendimento> atendimentos)
        {
            List<ItemCalendario> retorno = new List<ItemCalendario>();

            foreach (var item in atendimentos)
            {
                if (item.dataAgendada.Day == dia)
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

        public static Dia PreencheDia(int dia, int Mes, int Ano, SqlGeneric sqlServices, ContextPage contexto, string idProfissional)
        {
            string data = Ano + "-" + Mes + "- " + dia;

            Dia retorno = new Dia();
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dataFormat = culture.DateTimeFormat;
            //retorno.nomeDia = dataFormat.AbbreviatedDayNames[dia - 1];            


            IEnumerable<Atendimento> Atendimentos = sqlServices.RetornaAtendimentosDia(data, idProfissional, contexto.idOrganizacao.ToString());
           
            List<ItemCalendario> lancamentos = new List<ItemCalendario>();
            int horarios = 15; 

            ////Preenche horarios vazio
            //while ( horarios < 48 )
            //{
                foreach (var item in Atendimentos)
                {
                    //if (item.horaInicio == horarios)
                    //{
                        ItemCalendario hoc = new ItemCalendario();
                        hoc.id = item.id.ToString();
                        hoc.inicio = new ItemHoraDia();
                        hoc.inicio.horaDia = (CustomEnum.itemHora)item.horaInicio;
                        hoc.fim = new ItemHoraDia();
                        hoc.fim.horaDia = (CustomEnum.itemHora)item.horaFim;
                        hoc.titulo = item.titulo;
                        lancamentos.Add(hoc);

                        horarios = Convert.ToInt32(hoc.fim.horaDia) + 1;
                       // break;
                //    }
                //    else
                //    {
                       
                //            ItemCalendario hoc = new ItemCalendario();

                //            hoc.inicio = new ItemHoraDia();
                //            hoc.inicio.horaDia = (CustomEnum.itemHora)horarios;
                //            hoc.fim = new ItemHoraDia();

                //            hoc.titulo = "LIVRE";
                //            lancamentos.Add(hoc);
                //        break;
                        
                //    }
                //}
                //horarios++;
            }

            retorno.itensCalendario = lancamentos;


            return retorno;
        }
    }
}
