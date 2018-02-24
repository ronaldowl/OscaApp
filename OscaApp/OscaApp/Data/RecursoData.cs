using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class RecursoData : IRecursoData
    {
        private ContexDataService db;

          public RecursoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(Recurso modelo)
        {
            try
            {
                db.Recursos.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(Recurso modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                     = true;
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
        public Recurso Get(Guid id, Guid idOrg)
        {
            List<Recurso> retorno = new List<Recurso>();
            try
            {
                retorno = db.Recursos.FromSql("SELECT * FROM Recurso WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }

        public List<Recurso> GetAll(Guid idOrg)
        {
            List<Recurso> retorno = new List<Recurso>();
            retorno = db.Recursos.FromSql("SELECT * FROM Recurso WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }
    }
}