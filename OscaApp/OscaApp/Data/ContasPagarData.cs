using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContasPagarData : IContasPagarData
    {
        private ContexDataService db;

          public ContasPagarData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(ContasPagar contasPagar)
        {
            try
            {
                db.ContasP.Add(contasPagar);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(ContasPagar modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("codigo").IsModified                   = true;
                db.Entry(modelo).Property("titulo").IsModified                   = true;
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
        public ContasPagar Get(Guid id, Guid idOrg)
        {
            List<ContasPagar> retorno = new List<ContasPagar>();
            try
            {
                retorno = db.ContasP.FromSql("SELECT * FROM ContasPagar WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<ContasPagar> GetAll(Guid idOrg)
        {
            List<ContasPagar> retorno = new List<ContasPagar>();
            retorno = db.ContasP.FromSql("SELECT * FROM ContasPagar WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
