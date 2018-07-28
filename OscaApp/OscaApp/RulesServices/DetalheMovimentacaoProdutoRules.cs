using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class DetalheMovimentacaoProdutoRules
    {
        public static bool DetalheMovimentacaoProdutoCreate(DetalheMovimentacaoProdutoViewModel entrada,out DetalheMovimentacaoProduto modelo, ContextPage contexto )
        {
            modelo = new DetalheMovimentacaoProduto();
            modelo = entrada.detalheMovimentacaoProduto;

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

        public static bool DetalheMovimentacaoProdutoUpdate(DetalheMovimentacaoProdutoViewModel entrada, out DetalheMovimentacaoProduto modelo)
        {
            modelo = new DetalheMovimentacaoProduto();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.detalheMovimentacaoProduto;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
