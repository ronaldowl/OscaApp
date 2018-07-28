using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class LocalProdutoData : ILocalProdutoData
    {
        private ContexDataService db;

          public LocalProdutoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Add(LocalProduto modelo)
        {
            try
            {
                db.LocalProdutos.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(LocalProduto modelo)
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
        public LocalProduto Get(Guid id, Guid idOrg)
        {
            List<LocalProduto> retorno = new List<LocalProduto>();
            try
            {
                retorno = db.LocalProdutos.FromSql("SELECT * FROM LocalProduto WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }

        public List<LocalProduto> GetAll(Guid idOrg)
        {
            List<LocalProduto> retorno = new List<LocalProduto>();
            retorno = db.LocalProdutos.FromSql("SELECT * FROM LocalProduto WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }
    }
}