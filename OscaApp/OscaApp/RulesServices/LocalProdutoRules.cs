using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class LocalProdutoRules
    {
        public static bool LocalProdutoCreate(LocalProdutoViewModel entrada,out LocalProduto modelo, ContextPage contexto )
        {
            modelo = new LocalProduto();
            modelo = entrada.localProduto;

            SqlGeneric sqlServic = new SqlGeneric();
            //modelo.codigo = sqlServic.RetornaNovaPosicao(22,contexto.idOrganizacao);
            
 
            if (modelo.idOrganizacao != null)
            {
                //************ Objetos de controle de acesso ******************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }

        public static bool LocalProdutoUpdate(LocalProdutoViewModel entrada, out LocalProduto modelo)
        {
            modelo = new LocalProduto();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.localProduto;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
