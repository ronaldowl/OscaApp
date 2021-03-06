﻿using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class ClienteRules
    {
        public static Guid CreateClienteResumo(Cliente modelo,  ContextPage contexto, ClienteData clienteData)
        {

            modelo.idOrganizacao = contexto.idOrganizacao;

            if (modelo.idOrganizacao != null)
            {

                modelo.codigo = AutoNumber.GeraCodigo(1, contexto.idOrganizacao);

                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                //Cria Cliente 
                clienteData.Add(modelo);
            }

            return modelo.id;
        }

        public static bool MontaClienteCreate(ClienteViewModel entrada,out Cliente modelo, ContextPage contexto )
        {
            modelo = new Cliente ();                        
           
            if (entrada.contexto.idOrganizacao != null)
            {
                modelo = entrada.cliente;
                modelo.idContato = entrada.contato.id;
                modelo.codigo = AutoNumber.GeraCodigo(1, contexto.idOrganizacao);

                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm         = DateTime.Now;
                modelo.criadoPor        = contexto.idUsuario;
                modelo.criadoPorName    = contexto.nomeUsuario;
                modelo.modificadoEm     = DateTime.Now;
                modelo.modificadoPor    = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************



                return true;
            }           
            
            return false;
        }
        public static bool MontaClienteUpdate(ClienteViewModel entrada,out Cliente modelo, ContextPage contexto)
        {
            modelo = new Cliente();
            modelo = entrada.cliente;
            modelo.idContato = entrada.contato.id;
            ////************ Objetos de controle de acesso ***************
            modelo.modificadoEm         = DateTime.Now;
            modelo.modificadoPor        = contexto.idUsuario;
            modelo.modificadoPorName    = contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************

            return true;           
        }
        public static bool Cnpj_CfpExistente(string valor, Guid idOrganizacao, SqlGenericRules sqlServices, string id)
        {

            if(sqlServices.ConsultaCnpj_CpfDuplicado(valor, idOrganizacao.ToString(), id))
            {
                return true;
            }

            return false;
        }
        public static bool SetStatus(int valor, string idCliente, ClienteData clienteData, ContextPage contexto)
        {

            Cliente  modelo = new Cliente();
            modelo.id = new Guid(idCliente);
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = contexto.idUsuario;
            modelo.modificadoPorName = contexto.nomeUsuario;
            modelo.status = (CustomEnumStatus.Status)valor;
            clienteData.SetStatus(modelo);

            return true;            

        }
    }
}
