using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OscaApp.Data
{
    public class IncidenteData : IIncidenteData
    {
        // FIELDS
        private ContexDataService db;

        // CTOR
        public IncidenteData(ContexDataService dbContext)
        {
            this.db = dbContext;
        } // end of ctor

        public void Add(Incidente modelo)
        {
            try
            {
                db.Incidente.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } // end of method Add

        public Incidente Get(Guid id, Guid idOrganizacao)
        {
            List<Incidente> retorno = new List<Incidente>();
            try
            {
                retorno = db.Incidente.FromSql("SELECT * FROM Incidente WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrganizacao.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }

        public List<Incidente> GetAll(Guid idOrg)
        {
            List<Incidente> retorno = new List<Incidente>();
            retorno = db.Incidente.FromSql("SELECT * FROM Incidente WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }

        public void Update(Incidente modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;

                db.Entry(modelo).Property("descricao").IsModified = true;
                db.Entry(modelo).Property("titulo").IsModified = true;

                db.SaveChanges();
            } // end of try
            catch (Exception ex)
            {

                throw ex;
            } // end of catch

        } // end of method Update

    } // end of 
}
