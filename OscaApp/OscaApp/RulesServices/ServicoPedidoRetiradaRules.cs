using OscaApp.ViewModels;
using OscaApp.Data;
using OscaApp.Models;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ServicoPedidoRetiradaRules
    {
        public static bool ServicoPedidoRetiradaCreate(ServicoPedidoRetiradaViewModel entrada,out ServicoPedidoRetirada servicoPedidoRetirada, ContextPage contexto )
        {
            servicoPedidoRetirada = new ServicoPedidoRetirada();
            servicoPedidoRetirada = entrada.servicoPedidoRetirada;


            if (servicoPedidoRetirada.idOrganizacao != null)
            {
                //************ Objetos de controle de acesso ******************
                servicoPedidoRetirada.criadoEm = DateTime.Now;
                servicoPedidoRetirada.criadoPor = contexto.idUsuario;
                servicoPedidoRetirada.criadoPorName = contexto.nomeUsuario;
                servicoPedidoRetirada.modificadoEm = DateTime.Now;
                servicoPedidoRetirada.modificadoPor = contexto.idUsuario;
                servicoPedidoRetirada.modificadoPorName = contexto.nomeUsuario;
                servicoPedidoRetirada.idOrganizacao = contexto.idOrganizacao;

                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool ServicoPedidoRetiradaUpdate(ServicoPedidoRetiradaViewModel entrada,out ServicoPedidoRetirada servicoPedidoRetirada)
        {
            servicoPedidoRetirada = new ServicoPedidoRetirada();

            //************ Objetos de controle de acesso *******************
            servicoPedidoRetirada = entrada.servicoPedidoRetirada;
            servicoPedidoRetirada.modificadoEm = DateTime.Now;
            servicoPedidoRetirada.modificadoPor = entrada.contexto.idUsuario;
            servicoPedidoRetirada.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
