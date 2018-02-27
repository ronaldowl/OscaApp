using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using OscaFramework.Models;
 


namespace OscaApp.RulesServices
{
    public static class CalendarioRules
    {
       
        public static Calendario PreencheMes(int Mes, int Ano)
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

            //Preenche todos o dias do Mes
            for (int i = 1; i <= qtdDiasMes; i++)
            {

                DateTime dataRef = new DateTime(Ano, Mes, i);
                Dia dia = new Dia();
                dia.dia = i;
                dia.nome = dataFormat.GetDayName(dataRef.DayOfWeek);
                dia.itensCalendario = PreencheItemCalendario(i,Mes,Ano);
                diasMEs.Add(dia);
            }
            retorno.dias = diasMEs;

            return retorno;
        }

        public static Semana PreencheSemana(int semana, int Mes, int Ano)
        {
            Semana retorno = new Semana();
            //DayOfWeek.
            //DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;

            //retorno.domingo = new Dia();
            //retorno.domingo.dia = dtfi.FirstDayOfWeek;


            return retorno;
        }


        public static Dia PreencheDia(int dia, int Mes, int Ano)
        {
            Dia retorno = new Dia();            
            
          return retorno;
        }

        public static List<ItemCalendario> PreencheItemCalendario(int dia, int Mes, int Ano)
        {
            List<ItemCalendario> retorno = new List<ItemCalendario>();

            for (int i = 0; i < 47; i++)
            {
                ItemCalendario item = new ItemCalendario();            

            }

            return retorno;
        }
    }
}
