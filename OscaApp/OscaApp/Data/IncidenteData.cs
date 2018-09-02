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

            db.Incidente.Add(modelo);
            db.SaveChanges();

        } // end of method Add

        public Incidente Get(Guid id)
        {
            List<Incidente> retorno = new List<Incidente>();

            retorno = (from A in db.Incidente where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }

        public List<Incidente> GetAll(Guid idOrg)
        {
            List<Incidente> retorno = new List<Incidente>();
            retorno = (from A in db.Incidente where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }

        public void Update(Incidente modelo)
        {
            db.Attach(modelo);
            db.Entry(modelo).Property("modificadoPor").IsModified = true;
            db.Entry(modelo).Property("modificadoPorName").IsModified = true;
            db.Entry(modelo).Property("modificadoEm").IsModified = true;
            db.Entry(modelo).Property("descricao").IsModified = true;
            db.Entry(modelo).Property("titulo").IsModified = true;

            db.SaveChanges();

        } // end of method Update

    } // end of 
}
