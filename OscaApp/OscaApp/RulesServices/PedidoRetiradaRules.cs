using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class PedidoRetiradaRules
    {
        public static bool PedidoRetiradaCreate(PedidoRetiradaViewModel entrada,out PedidoRetirada modelo, ContextPage contexto )
        {
            modelo = new PedidoRetirada();
            modelo = entrada.pedidoRetirada;

            SqlGeneric sqlServic = new SqlGeneric();
            modelo.codigo = sqlServic.RetornaNovaPosicao(33,contexto.idOrganizacao);
                                 
 
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

        public static bool PedidoRetiradaUpdate(PedidoRetiradaViewModel entrada, out PedidoRetirada modelo)
        {
            modelo = new PedidoRetirada();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.pedidoRetirada;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
