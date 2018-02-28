using OscaFramework.MicroServices;
using System;

namespace OscaApp.Data
{
    public static class AutoNumber
    {
        /// <summary>
        /// Método utilizado para criar todos os códigos do formulário
        /// </summary>
        /// <param name="Entidade"></param>
        /// <returns></returns>
        public static string GeraCodigo(int Entidade, Guid idOrganizacao)
        {
            //TODO: Implemenar o Gerador de código para sistema OFFline
            SqlGeneric sqlGeneric = new SqlGeneric();
            string codigoRetorno = sqlGeneric.RetornaNovaPosicao(Entidade, idOrganizacao);

            return codigoRetorno;
        }
    }
}
