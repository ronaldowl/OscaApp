﻿using OscaApp.Data;
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
            modelo.idCliente = entrada.cliente.id;
            modelo.idProfissional = entrada.profissional.id;
            modelo.idEndereco = entrada.endereco.id;
            modelo.idEndereco2 = entrada.endereco2.id;


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
            modelo = entrada.pedidoRetirada;

            if (entrada.endereco != null)
            {
                modelo.idEndereco = entrada.endereco.id;
            }

            if (entrada.endereco2 != null)
            {
                modelo.idEndereco2 = entrada.endereco2.id;
            }

            if (entrada.profissional != null)
            {
                modelo.idProfissional = entrada.profissional.id;
            }

            if (entrada.pedidoRetirada.statusPedidoRetirada == CustomEnumStatus.StatusPedidoRetirada.Cancelado || entrada.pedidoRetirada.statusPedidoRetirada == CustomEnumStatus.StatusPedidoRetirada.Fechado)
            {
                modelo.dataFechamento = DateTime.Now;
            }

            //************ Objetos de controle de acesso *******************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }

        public static bool PedidoRetiradaUpdateStatus(PedidoRetiradaViewModel entrada, out PedidoRetirada modelo)
        {
            modelo = new PedidoRetirada();
            modelo = entrada.pedidoRetirada;
            modelo.id = entrada.pedidoRetirada.id;


            if (entrada.pedidoRetirada.statusPedidoRetirada == CustomEnumStatus.StatusPedidoRetirada.Cancelado || entrada.pedidoRetirada.statusPedidoRetirada == CustomEnumStatus.StatusPedidoRetirada.Fechado)
            {
                modelo.dataFechamento = DateTime.Now;
            }

            if (entrada.pedidoRetirada.statusPedidoRetirada == CustomEnumStatus.StatusPedidoRetirada.EmAndamento)
            {
                modelo.dataFechamento = new DateTime();
            }
    

            //************ Objetos de controle de acesso *******************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }


    }
}
