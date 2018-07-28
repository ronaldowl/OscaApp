using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class MovimentacaoProdutoRules
    {
        public static bool MovimentacaoProdutoCreate(MovimentacaoProdutoViewModel entrada,out MovimentacaoProduto modelo, ContextPage contexto )
        {
            modelo = new MovimentacaoProduto();
            modelo = entrada.movimentacaoProduto;

            SqlGeneric sqlServic = new SqlGeneric();
            //modelo.codigo = sqlServic.RetornaNovaPosicao(22,contexto.idOrganizacao);
            
 
            if (modelo.idOrganizacao!= null)
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

        public static bool MovimentacaoProdutoUpdate(MovimentacaoProdutoViewModel entrada, out MovimentacaoProduto modelo)
        {
            modelo = new MovimentacaoProduto();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.movimentacaoProduto;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
