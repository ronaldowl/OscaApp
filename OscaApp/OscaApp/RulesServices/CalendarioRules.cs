using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OscaApp.RulesServices
{
    public static class CalendarioRules
    {
       
        public static Calendario PreencheMes(int Mes, int Ano)
        {
            Calendario retorno = new Calendario();
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;

            int qtdDiasMes = DateTime.DaysInMonth(2018, 1);
            retorno.ano = Ano;
            retorno.mes = Mes;
            retorno.nomeMes = dtfi.MonthNames[1];                     
           
            List<Dia> diasMEs = new List<Dia>();

            //Preenche todos o dias do Mes
            for (int i = 1; i < qtdDiasMes; i++)
            {
                Dia dia = new Dia();
                dia.dia = i;
                dia.nome = dtfi.DayNames[i];

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
    }
}
