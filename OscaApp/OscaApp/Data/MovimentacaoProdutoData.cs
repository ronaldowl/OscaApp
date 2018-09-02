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
                db.MovimentacaoProdutos.Add(modelo);
                db.SaveChanges();        
        }
        public void Update(MovimentacaoProduto modelo)
        {
      
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;

                db.SaveChanges();

        }
        public MovimentacaoProduto Get(Guid id)
        {
            List<MovimentacaoProduto> retorno = new List<MovimentacaoProduto>();
        
            retorno = (from A in db.MovimentacaoProdutos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }

        public List<MovimentacaoProduto> GetAll(Guid idOrg)
        {
            List<MovimentacaoProduto> retorno = new List<MovimentacaoProduto>();

            retorno = (from A in db.MovimentacaoProdutos where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return retorno;
        }
    }
}