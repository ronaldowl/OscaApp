﻿using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ContasReceberRules
    {
        public static bool ContasReceberCreate(ContasReceberViewModel entrada,out ContasReceber contasReceber, ContextPage contexto )
        {
            contasReceber = new ContasReceber();
            contasReceber = entrada.contasReceber;

            if (entrada.cliente != null) contasReceber.idCliente = entrada.cliente.id;

            contasReceber.codigo = AutoNumber.GeraCodigo(21, contexto.idOrganizacao);
         
            if (contasReceber.codigo != null)
            {
                //************ Objetos de controle de acesso ******************
                contasReceber.criadoEm = DateTime.Now;
                contasReceber.criadoPor = contexto.idUsuario;
                contasReceber.criadoPorName = contexto.nomeUsuario;
                contasReceber.modificadoEm = DateTime.Now;
                contasReceber.modificadoPor = contexto.idUsuario;
                contasReceber.modificadoPorName = contexto.nomeUsuario;
                contasReceber.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool ContasReceberUpdate(ContasReceberViewModel entrada, out ContasReceber contasReceber)
        {
            contasReceber = new ContasReceber();
            contasReceber = entrada.contasReceber;
            if (entrada.cliente != null) contasReceber.idCliente = entrada.cliente.id;


            //************ Objetos de controle de acesso *******************
            contasReceber.modificadoEm = DateTime.Now;
            contasReceber.modificadoPor = entrada.contexto.idUsuario;
            contasReceber.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
