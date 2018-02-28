using System;
using System.Collections.Generic;
using System.Text;

namespace OscaFramework.Helper
{
   public static  class HelperCalendario
    {
        public static string RetornaHoraFormatda(int hora)
        {
            string horaFormatada = "";               

            switch (hora)
            {
                case 1: horaFormatada = "01:00"; break;
                case 2: horaFormatada = "01:30"; break;
                case 3: horaFormatada = "02:00"; break;
                case 4: horaFormatada = "02:30"; break;
                case 5: horaFormatada = "03:00"; break;
                case 6: horaFormatada = "03:30"; break;
                case 7: horaFormatada = "04:00"; break;
                case 8: horaFormatada = "04:30"; break;
                case 9: horaFormatada = "05:00"; break;
                case 10: horaFormatada = "05:30"; break;
                case 11: horaFormatada = "06:00"; break;
                case 12: horaFormatada = "06:30"; break;
                case 13: horaFormatada = "07:00"; break;
                case 14: horaFormatada = "07:30"; break;
                case 15: horaFormatada = "08:00"; break;
                case 16: horaFormatada = "08:30"; break;
                case 17: horaFormatada = "09:00"; break;
                case 18: horaFormatada = "09:30"; break;
                case 19: horaFormatada = "10:00"; break;
                case 20: horaFormatada = "10:30"; break;
                case 21: horaFormatada = "11:00"; break;
                case 22: horaFormatada = "11:30"; break;
                case 23: horaFormatada = "12:00"; break;
                case 24: horaFormatada = "12:30"; break;
                case 25: horaFormatada = "13:00"; break;
                case 26: horaFormatada = "13:30"; break;
                case 27: horaFormatada = "14:00"; break;
                case 28: horaFormatada = "14:30"; break;
                case 29: horaFormatada = "15:00"; break;
                case 30: horaFormatada = "15:30"; break;
                case 31: horaFormatada = "16:00"; break;
                case 32: horaFormatada = "16:30"; break;
                case 33: horaFormatada = "17:00"; break;
                case 34: horaFormatada = "17:30"; break;
                case 35: horaFormatada = "18:00"; break;
                case 36: horaFormatada = "18:30"; break;
                case 37: horaFormatada = "19:00"; break;
                case 38: horaFormatada = "19:30"; break;
                case 39: horaFormatada = "20:00"; break;
                case 40: horaFormatada = "20:30"; break;
                case 41: horaFormatada = "21:00"; break;
                case 42: horaFormatada = "21:30"; break;
                case 43: horaFormatada = "22:00"; break;
                case 44: horaFormatada = "22:30"; break;
                case 45: horaFormatada = "23:00"; break;
                case 46: horaFormatada = "23:30"; break;
                case 47: horaFormatada = "00:00"; break;               
                    
                default: horaFormatada = "UNKNOW";
                    break;
            }

            return horaFormatada;
        }

    }
}
