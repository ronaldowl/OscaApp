using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ServicoData : IServicoData
    {
        private ContexDataService db;

          public ServicoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(Servico servico)
        {
            try
            {
                db.Servicos.Add(servico);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(Servico modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("codigo").IsModified                   = true;
                db.Entry(modelo).Property("nomeServico").IsModified              = true;
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
        public Servico Get(Guid id, Guid idOrg)
        {
            List<Servico> retorno = new List<Servico>();
            try
            {

                retorno = db.Servicos.FromSql("SELECT * FROM servico where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<Servico> GetAll(Guid idOrg)
        {
            List<Servico> retorno = new List<Servico>();
            retorno = db.Servicos.FromSql("SELECT * FROM servico where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
