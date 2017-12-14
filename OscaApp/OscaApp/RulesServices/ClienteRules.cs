﻿using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;

namespace OscaApp.RulesServices
{
    public static class ClienteRules
    {
        public static bool MontaClienteCreate(ClienteViewModel entrada,out Cliente modelo, ContextPage contexto )
        {
            modelo = new Cliente ();                        
           
            if (entrada.contexto.idOrganizacao != null)
            {
                modelo = entrada.cliente;
                modelo.codigo = AutoNumber.GeraCodigo(1, contexto.idOrganizacao);

                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm         = DateTime.Now;
                modelo.criadoPor        = contexto.idUsuario;
                modelo.criadoPorName    = contexto.nomeUsuario;
                modelo.modificadoEm     = DateTime.Now;
                modelo.modificadoPor    = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                ////************ FIM Objetos de controle de acesso ***************

                modelo.idOrganizacao    = contexto.idOrganizacao;              

                return true;
            }           
            
            return false;
        }
        public static bool MontaClienteUpdate(ClienteViewModel entrada,out Cliente modelo)
        {
            modelo = new Cliente();

            modelo = entrada.cliente;
            modelo.modificadoEm         = DateTime.Now;
            modelo.modificadoPor        = entrada.contexto.idUsuario;
            modelo.modificadoPorName    = entrada.contexto.nomeUsuario;          
                
            return true;           
        }
        
    }
}
