using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class OrdemServicoData : IOrdemServicoData
    {
        private ContexDataService db;

          public OrdemServicoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(OrdemServico ordemServico)
        {
            try
            {
                db.OrdensServico.Add(ordemServico);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(OrdemServico modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("codigo").IsModified                   = true;
                db.Entry(modelo).Property("dataAgendada").IsModified              = true;


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
        public OrdemServico Get(Guid id, Guid idOrg)
        {
            List<OrdemServico> retorno = new List<OrdemServico>();
            try
            {
                retorno = db.OrdensServico.FromSql("SELECT * FROM OrdemServico WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<OrdemServico> GetAll(Guid idOrg)
        {
            List<OrdemServico> retorno = new List<OrdemServico>();
            retorno = db.OrdensServico.FromSql("SELECT * FROM OrdemServico WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
