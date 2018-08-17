using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class PagamentoRules
    {
        public static bool PagamentoCreate(PagamentoViewModel entrada,out Pagamento pagamento, ContextPage contexto )
        {
            pagamento = new Pagamento ();
            pagamento = entrada.pagamento;

         
            if (entrada.contasReceber != null)
            {

                pagamento = entrada.pagamento;
                pagamento.idContasReceber = entrada.contasReceber.id;
                pagamento.dataPagamento = DateTime.Now;

                //************ Objetos de controle de acesso ******************
                pagamento.criadoEm = DateTime.Now;
                pagamento.criadoPor = contexto.idUsuario;
                pagamento.criadoPorName = contexto.nomeUsuario;
                pagamento.modificadoEm = DateTime.Now;
                pagamento.modificadoPor = contexto.idUsuario;
                pagamento.modificadoPorName = contexto.nomeUsuario;
                pagamento.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool PagamentoUpdate(PagamentoViewModel entrada,out Pagamento pagamento)
        {
            pagamento = new Pagamento();

            //************ Objetos de controle de acesso *******************
           
            pagamento.dataPagamento = DateTime.Now;
            pagamento.modificadoEm = DateTime.Now;
            pagamento.modificadoPor = entrada.contexto.idUsuario;
            pagamento.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }
     
    }
}
