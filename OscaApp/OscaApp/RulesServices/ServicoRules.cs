using OscaApp.ViewModels;
using OscaApp.Data;
using OscaApp.Models;
using System;

namespace OscaApp.RulesServices
{
    public static class ServicoRules
    {
        public static bool ServicoCreate(ServicoViewModel entrada,out Servico servico, ContextPage contexto )
        {
            servico = new Servico();
            servico = entrada.servico;
            servico.codigo = AutoNumber.GeraCodigo(6, contexto.idOrganizacao);


            if (servico.nomeServico != null)
            {
                //************ Objetos de controle de acesso ******************
                servico.criadoEm = DateTime.Now;
                servico.criadoPor = contexto.idUsuario;
                servico.criadoPorName = contexto.nomeUsuario;
                servico.modificadoEm = DateTime.Now;
                servico.modificadoPor = contexto.idUsuario;
                servico.modificadoPorName = contexto.nomeUsuario;
                servico.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool ServicoUpdate(ServicoViewModel entrada,out Servico servico)
        {
            servico = new Servico();

            //************ Objetos de controle de acesso *******************
            servico = entrada.servico;
            servico.modificadoEm = DateTime.Now;
            servico.modificadoPor = entrada.contexto.idUsuario;
            servico.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
