using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaApp.framework; 
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class FornecedorRules
    {
        public static bool FornecedorCreate(FornecedorViewModel entrada,out Fornecedor fornecedor, ContextPage contexto )
        {
            fornecedor = new Fornecedor();
            fornecedor = entrada.Fornecedor;
         
            if (fornecedor.nomeFornecedor != null)
            {
                fornecedor.codigo =  AutoNumber.GeraCodigo(14,contexto.idOrganizacao);

                //************ Objetos de controle de acesso ******************
                fornecedor.criadoEm = DateTime.Now;
                fornecedor.criadoPor = contexto.idUsuario;
                fornecedor.criadoPorName = contexto.nomeUsuario;
                fornecedor.modificadoEm = DateTime.Now;
                fornecedor.modificadoPor = contexto.idUsuario;
                fornecedor.modificadoPorName = contexto.nomeUsuario;
                fornecedor.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool FornecedorUpdate(FornecedorViewModel entrada,out Fornecedor fornecedor)
        {
            fornecedor = new Fornecedor();

            //************ Objetos de controle de acesso *******************
            fornecedor = entrada.Fornecedor;
            fornecedor.modificadoEm = DateTime.Now;
            fornecedor.modificadoPor = entrada.Contexto.idUsuario;
            fornecedor.modificadoPorName = entrada.Contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
