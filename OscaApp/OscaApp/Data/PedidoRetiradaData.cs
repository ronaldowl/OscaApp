﻿using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class PedidoRetiradaData : IPedidoRetiradaData
    {
        private ContexDataService db;

          public PedidoRetiradaData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Delete(PedidoRetirada modelo)
        {
            try
            {
                db.PedidosRetirada.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Add(PedidoRetirada modelo)
        {
            try
            {
                db.PedidosRetirada.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(PedidoRetirada modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("tipoPagamento").IsModified            = true;
                db.Entry(modelo).Property("condicaoPagamento").IsModified        = true;
                db.Entry(modelo).Property("tipoDesconto").IsModified             = true;
                db.Entry(modelo).Property("valorDesconto").IsModified            = true;
                db.Entry(modelo).Property("valorTotal").IsModified               = true;
                db.Entry(modelo).Property("statusPedidoRetirada").IsModified     = true;
                db.Entry(modelo).Property("anotacao").IsModified                 = true;
                db.Entry(modelo).Property("dataFechamento").IsModified           = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public PedidoRetirada Get(Guid id, Guid idOrg)
        {
            List<PedidoRetirada> retorno = new List<PedidoRetirada>();
            try
            {
                retorno = db.PedidosRetirada.FromSql("SELECT * FROM PedidoRetirada WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }

        public List<PedidoRetirada> GetAll(Guid idOrg)
        {
            List<PedidoRetirada> retorno = new List<PedidoRetirada>();
            retorno = db.PedidosRetirada.FromSql("SELECT * FROM PedidoRetirada WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }
    }
}