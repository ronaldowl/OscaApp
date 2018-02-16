using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class AtendimentoRules
    {
        public static bool AtendimentoCreate(AtendimentoViewModel entrada,out Atendimento modelo, ContextPage contexto )
        {
            modelo = new Atendimento();
            modelo = entrada.atendimento;
            modelo.status = CustomEnumStatus.Status.Ativo;
            modelo.codigo = AutoNumber.GeraCodigo(3, contexto.idOrganizacao);

         
            if (modelo.codigo != null)
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
        ////public static bool AtendimentoUpdate(AtendimentoViewModel entrada,out Atendimento atendimento)
        //{
        //    listapreco = new ListaPreco();

        //    //************ Objetos de controle de acesso *******************
        //    listapreco = entrada.listaPreco;
        //    listapreco.modificadoEm = DateTime.Now;
        //    listapreco.modificadoPor = entrada.contexto.idUsuario;
        //    listapreco.modificadoPorName = entrada.contexto.nomeUsuario;
        //    //************ FIM Objetos de controle de acesso ***************

        //    return true;
        //}       
    }
}
