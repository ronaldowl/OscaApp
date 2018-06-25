using System;
using System.Collections.Generic;
using System.Text;

namespace OscaFramework.Helper
{
    public static class HelperString
    {

        public static string RemoveAcentos(string texto)

        {

            string ComAcentos = "!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";

            string SemAcentos = "_________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";

            for (int i = 0; i < ComAcentos.Length; i++)
            {

                texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();

            }
            return texto;



        }
    }
}
