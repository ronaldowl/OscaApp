using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContasReceberData : IContasReceberData
    {
        private ContexDataService db;

          public ContasReceberData (ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(ContasReceber contasReceber)
        {
            try
            {
                db.ContasR.Add(contasReceber);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(ContasReceber modelo)
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
        public ContasReceber Get(Guid id, Guid idOrg)
        {
            List<ContasReceber> retorno = new List<ContasReceber>();
            try
            {
                retorno = db.ContasR.FromSql("SELECT * FROM ContasReceber WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<ContasReceber> GetAll(Guid idOrg)
        {
            List<ContasReceber> retorno = new List<ContasReceber>();
            retorno = db.ContasR.FromSql("SELECT * FROM ContasReceber WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
