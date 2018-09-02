using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ComunicadoData : IComunicadoData
    {
        private ContexDataService db;

          public ComunicadoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Delete(Comunicado modelo)
        {   
                db.Comunicados.Remove(modelo);
                db.SaveChanges();
        }

        public void Add(Comunicado comunicado)
        { 
                db.Comunicados.Add(comunicado);
                db.SaveChanges();
        }
        public void Update(Comunicado modelo)
        {

                db.Attach(modelo);
                db.Entry(modelo).Property("titulo").IsModified                   = true;
                db.Entry(modelo).Property("dataInicio").IsModified               = true;
                db.Entry(modelo).Property("dataFim").IsModified                  = true;
                db.Entry(modelo).Property("mensagem").IsModified                 = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();
        }
        public Comunicado Get(Guid id)
        {
            List<Comunicado> retorno = new List<Comunicado>();
 
            retorno = (from A in db.Comunicados where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Comunicado> GetAll(Guid idOrg)
        {
            List<Comunicado> retorno = new List<Comunicado>();
            retorno = (from A in db.Comunicados where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
        
    }
}
