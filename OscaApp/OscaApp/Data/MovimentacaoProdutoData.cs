using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class MovimentacaoProdutoData : IMovimentacaoProdutoData
    {
        private ContexDataService db;

          public MovimentacaoProdutoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Add(MovimentacaoProduto modelo)
        {
            try
            {
                db.MovimentacaoProdutos.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(MovimentacaoProduto modelo)
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
        public MovimentacaoProduto Get(Guid id, Guid idOrg)
        {
            List<MovimentacaoProduto> retorno = new List<MovimentacaoProduto>();
            try
            {
                retorno = db.MovimentacaoProdutos.FromSql("SELECT * FROM MovimentacaoProduto WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }

        public List<MovimentacaoProduto> GetAll(Guid idOrg)
        {
            List<MovimentacaoProduto> retorno = new List<MovimentacaoProduto>();
            retorno = db.MovimentacaoProdutos.FromSql("SELECT * FROM MovimentacaoProduto WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }
    }
}