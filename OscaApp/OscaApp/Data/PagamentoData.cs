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
            try
            {
                db.Pagamentos.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Add(Pagamento modelo)
        {
            try
            {
                db.Pagamentos.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(Pagamento modelo)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }

        }
        public Pagamento Get(Guid id)
        {
            List<Pagamento> retorno = new List<Pagamento>();
            try
            {
                retorno = db.Pagamentos.FromSql("SELECT * FROM Pagamento where  id = '" + id.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<Pagamento> GetAllByContasReceber(Guid id)
        {
            List<Pagamento> retorno = new List<Pagamento>();
            retorno = db.Pagamentos.FromSql("SELECT * FROM Pagamento where  idContasReceber = '" + id.ToString() + "'").ToList();
            return retorno;

        }
        
    }
}
