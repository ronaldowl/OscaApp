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

    }
}


