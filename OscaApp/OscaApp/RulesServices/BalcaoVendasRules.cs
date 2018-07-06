using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class BalcaoVendasRules
    {
        public static Guid BalcaoVendasCreate(BalcaoVendasViewModel entrada,  ContextPage contexto, IBalcaoVendasData balcaoData)
        {
        
            entrada.balcaoVendas.codigo = AutoNumber.GeraCodigo(31, contexto.idOrganizacao);


            if (entrada.balcaoVendas.codigo != null)
            {
                ////************ Objetos de controle de acesso ***************
                entrada.balcaoVendas.criadoEm = DateTime.Now;
                entrada.balcaoVendas.criadoPor = contexto.idUsuario;
                entrada.balcaoVendas.criadoPorName = contexto.nomeUsuario;
                entrada.balcaoVendas.modificadoEm = DateTime.Now;
                entrada.balcaoVendas.modificadoPor = contexto.idUsuario;
                entrada.balcaoVendas.modificadoPorName = contexto.nomeUsuario;
                entrada.balcaoVendas.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                //Grava objeto na base
                balcaoData.Add(entrada.balcaoVendas);


               
            }

            return entrada.balcaoVendas.id;

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
