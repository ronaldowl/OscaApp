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
           
                db.DetalheMovimentacaoProdutos.Add(modelo);
                db.SaveChanges();
      
        }
        public void Update(DetalheMovimentacaoProduto modelo)
        {
            
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                     = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;
                db.SaveChanges();        

        }
        public DetalheMovimentacaoProduto Get(Guid id)
        {
            List<DetalheMovimentacaoProduto> retorno = new List<DetalheMovimentacaoProduto>();
     
            retorno = (from A in db.DetalheMovimentacaoProdutos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }

        public List<DetalheMovimentacaoProduto> GetAll(Guid idOrg)
        {
            List<DetalheMovimentacaoProduto> retorno = new List<DetalheMovimentacaoProduto>();
            retorno = (from A in db.DetalheMovimentacaoProdutos where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return retorno;
        }
    }
}