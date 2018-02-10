using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ContasPagarRules
    {
        public static bool ContasPagarCreate(ContasPagarViewModel entrada,out ContasPagar contasPagar, ContextPage contexto )
        {
            contasPagar = new ContasPagar();
            contasPagar = entrada.contasPagar;
            contasPagar.codigo = AutoNumber.GeraCodigo(20, contexto.idOrganizacao);
         
            if (contasPagar.codigo != null)
            {
                //************ Objetos de controle de acesso ******************
                contasPagar.criadoEm = DateTime.Now;
                contasPagar.criadoPor = contexto.idUsuario;
                contasPagar.criadoPorName = contexto.nomeUsuario;
                contasPagar.modificadoEm = DateTime.Now;
                contasPagar.modificadoPor = contexto.idUsuario;
                contasPagar.modificadoPorName = contexto.nomeUsuario;
                contasPagar.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool ContasPagarUpdate(ContasPagarViewModel entrada, out ContasPagar contasPagar)
        {
            contasPagar = new ContasPagar();

            //************ Objetos de controle de acesso *******************
            contasPagar = entrada.contasPagar;
            contasPagar.modificadoEm = DateTime.Now;
            contasPagar.modificadoPor = entrada.contexto.idUsuario;
            contasPagar.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
