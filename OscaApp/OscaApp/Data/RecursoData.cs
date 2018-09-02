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
        public void Delete(Recurso modelo)
        {           
                db.Recursos.Remove(modelo);
                db.SaveChanges(); 
        }
        public void Add(Recurso modelo)
        {          
                db.Recursos.Add(modelo);
                db.SaveChanges();
        }
        public void Update(Recurso modelo)
        {          
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                     = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();

        }
        public Recurso Get(Guid id)
        {
            List<Recurso> retorno = new List<Recurso>();       

            retorno = (from A in db.Recursos where A.id.Equals(id) select A).ToList();


            return retorno[0];
        }

        public List<Recurso> GetAll(Guid idOrg)
        {
            List<Recurso> retorno = new List<Recurso>();
            retorno = (from A in db.Recursos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }
    }
}