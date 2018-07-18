using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ServicoPedidoRetiradaData : IServicoPedidoRetiradaData
    {
        private ContexDataService db;

          public ServicoPedidoRetiradaData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(ServicoPedidoRetirada servicoPedidoRetirada)
        {
            try
            {
                db.ServicosPedidoRetirada.Add(servicoPedidoRetirada);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(ServicoPedidoRetirada modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified                    = true;
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
        public ServicoPedidoRetirada Get(Guid id, Guid idOrg)
        {
            List<ServicoPedidoRetirada> retorno = new List<ServicoPedidoRetirada>();
            try
            {

                retorno = db.ServicosPedidoRetirada.FromSql("SELECT * FROM ServicoPedidoRetirada where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<ServicoPedidoRetirada> GetAll(Guid idOrg)
        {
            List<ServicoPedidoRetirada> retorno = new List<ServicoPedidoRetirada>();
            retorno = db.ServicosPedidoRetirada.FromSql("SELECT * FROM ServicoPedidoRetirada where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
