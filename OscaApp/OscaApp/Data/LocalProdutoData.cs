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
                db.LocalProdutos.Add(modelo);
                db.SaveChanges();     
        }
        public void Update(LocalProduto modelo)
        {          
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                     = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();      

        }
        public LocalProduto Get(Guid id)
        {
            List<LocalProduto> retorno = new List<LocalProduto>();

            retorno = (from A in db.LocalProdutos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }

        public List<LocalProduto> GetAll(Guid idOrg)
        {
            List<LocalProduto> retorno = new List<LocalProduto>();
            retorno = (from A in db.LocalProdutos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }
    }
}