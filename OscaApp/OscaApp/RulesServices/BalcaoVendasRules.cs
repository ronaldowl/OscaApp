﻿using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class BalcaoVendasRules
    {
        public static bool MontaBalcaoVendasCreate(BalcaoVendasViewModel entrada, out BalcaoVendas modelo, ContextPage contexto)
        {
            modelo = new BalcaoVendas();
            modelo = entrada.balcaoVendas;
            modelo.codigo = AutoNumber.GeraCodigo(31, contexto.idOrganizacao);


            if (modelo.codigo != null)
            {              
                               

                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************
                
                return true;
            }

            return false;
        }
        //public static bool MontaClienteUpdate(ClienteViewModel entrada,out Cliente modelo, ContextPage contexto)
        //{
        //    modelo = new Cliente();
        //    modelo = entrada.cliente;
        //    modelo.idContato = entrada.contato.id;
        //    ////************ Objetos de controle de acesso ***************
        //    modelo.modificadoEm         = DateTime.Now;
        //    modelo.modificadoPor        = contexto.idUsuario;
        //    modelo.modificadoPorName    = contexto.nomeUsuario;
        //    ////************ FIM Objetos de controle de acesso ***************

        //    return true;           
        //}
        //public static bool ConsultaProduto(string filtro, Guid idListaPreco, SqlGenericRules sqlServices)
        //{

        //    if(sqlServices.ConsultaProduto(filtro, idListaPreco.ToString()))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
        public static bool FechaVenda(int valor, string idCliente, ClienteData clienteData, ContextPage contexto)
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
