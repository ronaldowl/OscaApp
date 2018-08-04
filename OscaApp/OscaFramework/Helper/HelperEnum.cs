using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OscaFramework.Helper
{
    /// <summary>
    /// Class especializada em Formatar Enum
    /// </summary>
   public static  class HelperEnum
    {
       
        public static string FormatEnumTipoAtividade(int entrada)
        {
            string retorno = "";

            switch (entrada)
            {
                case 1: retorno = "Tarefa"; break;
                case 2: retorno = "Telefonema"; break;
                case 3: retorno = "Serviço"; break;
                case 4: retorno = "Cobrança"; break;

                default:
                    retorno = "UNKNOW";
                    break;
            }

            return retorno;
        }

        public static string FormatEnumPeriodo(CustomEnum.Periodo entrada)
        {
            string retorno = "";

            switch (Convert.ToInt32(entrada))
            {
                case 0: retorno = "Manhã"; break;
                case 1: retorno = "Tarde"; break;
                case 2: retorno = "Noite"; break;
             

                default:
                    retorno = "UNKNOW";
                    break;
            }

            return retorno;
        }

    }
}


