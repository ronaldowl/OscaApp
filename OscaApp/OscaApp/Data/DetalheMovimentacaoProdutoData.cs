using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class DetalheMovimentacaoProdutoData : IDetalheMovimentacaoProdutoData
    {
        private ContexDataService db;

          public DetalheMovimentacaoProdutoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Add(DetalheMovimentacaoProduto modelo)
        {
            try
            {
                db.DetalheMovimentacaoProdutos.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(DetalheMovimentacaoProduto modelo)
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
        public DetalheMovimentacaoProduto Get(Guid id, Guid idOrg)
        {
            List<DetalheMovimentacaoProduto> retorno = new List<DetalheMovimentacaoProduto>();
            try
            {
                retorno = db.DetalheMovimentacaoProdutos.FromSql("SELECT * FROM DetalheMovimentacaoProduto WHERE  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }

        public List<DetalheMovimentacaoProduto> GetAll(Guid idOrg)
        {
            List<DetalheMovimentacaoProduto> retorno = new List<DetalheMovimentacaoProduto>();
            retorno = db.DetalheMovimentacaoProdutos.FromSql("SELECT * FROM DetalheMovimentacaoProduto WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }
    }
}