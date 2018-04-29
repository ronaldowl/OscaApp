using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class ClientePotencialRules
    {
        public static bool MontaClientePotencialCreate(ClientePotencialViewModel entrada,out ClientePotencial modelo, ContextPage contexto )
        {
            modelo = new ClientePotencial();                        
           
            if (entrada.contexto.idOrganizacao != null)
            {
                modelo = entrada.clientePotencial;
             
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
        public static bool MontaClientePotencialUpdate(ClientePotencialViewModel entrada,out ClientePotencial modelo, ContextPage contexto)
        {
            modelo = new ClientePotencial();
            modelo = entrada.clientePotencial;
            
            ////************ Objetos de controle de acesso ***************
            modelo.modificadoEm         = DateTime.Now;
            modelo.modificadoPor        = contexto.idUsuario;
            modelo.modificadoPorName    = contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************

            return true;           
        }
        public static bool Cnpj_CfpExistente(string valor, Guid idOrganizacao, SqlGenericRules sqlServices)
        {

            if(sqlServices.ConsultaCnpj_CpfDuplicado(valor, idOrganizacao.ToString()))
            {
                return true;
            }

            return false;
        }
        public static bool SetStatus(int valor, string idCliente, ClientePotencialData clientePotencialData, ClienteData clienteData, ContextPage contexto)
        {

            ClientePotencial modelo = new ClientePotencial();
            modelo.id = new Guid(idCliente);
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = contexto.idUsuario;
            modelo.modificadoPorName = contexto.nomeUsuario;           
            modelo.statusLead = (CustomEnumStatus.StatusLead)valor;

            clientePotencialData.SetStatus(modelo);

            //Cria o Objeto Cliente
            if(valor == 2)
            {

                Cliente cliente = new Cliente();
                ClientePotencial clientePotencial = new ClientePotencial();
                clientePotencial = clientePotencialData.Get(new Guid(idCliente));

                cliente.nomeCliente = clientePotencial.nomeCliente;
                cliente.sexo = clientePotencial.sexo;

                if (!String.IsNullOrEmpty(clientePotencial.email)) cliente.email = clientePotencial.email;

                if (!String.IsNullOrEmpty(clientePotencial.telefone)) cliente.telefone = clientePotencial.telefone;

                if (!String.IsNullOrEmpty(clientePotencial.celular)) cliente.celular = clientePotencial.celular;

                if (!String.IsNullOrEmpty(clientePotencial.anotacao)) cliente.anotacao = clientePotencial.anotacao;

                cliente.codigo = AutoNumber.GeraCodigo(1, contexto.idOrganizacao);

                ////************ Objetos de controle de acesso ***************
                cliente.criadoEm = DateTime.Now;
                cliente.criadoPor = contexto.idUsuario;
                cliente.criadoPorName = contexto.nomeUsuario;
                cliente.modificadoEm = DateTime.Now;
                cliente.modificadoPor = contexto.idUsuario;
                cliente.modificadoPorName = contexto.nomeUsuario;
                cliente.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                //Cria o novo Cliente
                clienteData.Add(cliente);


            }

            return true;            

        }

   
    }
}
