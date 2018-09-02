using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class PagamentoData : IPagamentoData
    {
        private ContexDataService db;

          public PagamentoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Delete(Pagamento modelo)
        {         
                db.Pagamentos.Remove(modelo);
                db.SaveChanges();
        }

        public void Add(Pagamento modelo)
        {          
                db.Pagamentos.Add(modelo);
                db.SaveChanges();   
        }
        public void Update(Pagamento modelo)
        {         
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified                    = true;
                db.Entry(modelo).Property("dataPagamento").IsModified            = true;
                db.Entry(modelo).Property("tipoPagamento").IsModified            = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();

        }
        public Pagamento Get(Guid id)
        {
            List<Pagamento> retorno = new List<Pagamento>();
   
            retorno = (from A in db.Pagamentos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Pagamento> GetAllByContasReceber(Guid id)
        {
            List<Pagamento> retorno = new List<Pagamento>();          
            retorno = (from A in db.Pagamentos where A.idContasReceber.Equals(id) select A).ToList();
            return retorno;

        }
        
    }
}
