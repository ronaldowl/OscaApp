using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.RulesServices
{
    public static class PedidoRules
    {
        public static bool PedidoCreate(PedidoViewModel entrada,out Pedido modelo, ContextPage contexto )
        {
            modelo = new Pedido ();
            modelo = entrada.pedido;                               
                     
            if (modelo.idCliente  != null)
            {
                //Gera código do Pedido
                SqlGenericService sqlservice = new SqlGenericService();

                //Associa Cliente
                modelo.idCliente = entrada.cliente.id;

                modelo.codigoPedido = sqlservice.RetornaNovaPosicao(4, contexto.idOrganizacao);                              

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
        public static bool PedidoUpdate(PedidoViewModel entrada,out Pedido modelo)
        {
            modelo = new Pedido();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.pedido;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }


      
    }
}
