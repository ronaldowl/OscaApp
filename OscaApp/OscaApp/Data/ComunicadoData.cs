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
        public void Add(Comunicado comunicado)
        {
            try
            {
                db.Comunicados.Add(comunicado);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(Comunicado modelo)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }

        }
        public Comunicado Get(Guid id, Guid idOrg)
        {
            List<Comunicado> retorno = new List<Comunicado>();
            try
            {
                retorno = db.Comunicados.FromSql("SELECT * FROM comunicado where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<Comunicado> GetAll(Guid idOrg)
        {
            List<Comunicado> retorno = new List<Comunicado>();
            retorno = db.Comunicados.FromSql("SELECT * FROM comunicado where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
        
    }
}
