using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OscaApp.Data
{
    public class ProdutoBalcaoData : IProdutoBalcaoData
    {
        // FIELDS
        private ContexDataService db;

        // CTOR
        public ProdutoBalcaoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        } // end of ctor

        public void Add(ProdutoBalcao modelo)
        {
           
                db.ProdutosBalcao.Add(modelo);
                db.SaveChanges();
         
        } // end of method Add

        public ProdutoBalcao Get(Guid id)
        {
            List<ProdutoBalcao> retorno = new List<ProdutoBalcao>();
            retorno = (from bl in db.ProdutosBalcao where bl.id.Equals(id) select bl).ToList();
          
            return retorno[0];
        }

        public List<ProdutoBalcao> GetAll(Guid idOrg)
        {
            List<ProdutoBalcao> retorno = new List<ProdutoBalcao>();
            retorno = (from bl in db.ProdutosBalcao where bl.idOrganizacao.Equals(idOrg) select bl).ToList();
            return retorno;
        }

        public void Update(ProdutoBalcao modelo)
        {
            
                db.Attach(modelo);
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;

                db.Entry(modelo).Property("total").IsModified = true;
                db.Entry(modelo).Property("quantidade").IsModified = true;
         
                db.SaveChanges();
  

        } // end of method Update

    } // end of 
}
