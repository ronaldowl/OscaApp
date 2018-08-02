using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContasPagarData : IContasPagarData
    {
        private ContexDataService db;

          public ContasPagarData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Delete(ContasPagar modelo)
        {
            try
            {
                db.ContasP.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void Add(ContasPagar contasPagar)
        {
            try
            {
                db.ContasP.Add(contasPagar);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Update(ContasPagar modelo)
        {
            try
            {
                db.Attach(modelo);
           
                db.Entry(modelo).Property("titulo").IsModified                   = true;
                db.Entry(modelo).Property("valor").IsModified                    = true;
                db.Entry(modelo).Property("anotacao").IsModified                 = true;
                db.Entry(modelo).Property("tipoLancamento").IsModified           = true;
                db.Entry(modelo).Property("origemContaPagar").IsModified         = true;
                db.Entry(modelo).Property("dataPagamento").IsModified            = true;
                db.Entry(modelo).Property("statusContaPagar").IsModified         = true;
                db.Entry(modelo).Property("numeroReferencia").IsModified         = true;                                
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
        public void UpdateStatus(ContasPagar modelo)
        {
            try
            {
                db.Attach(modelo);
           
                db.Entry(modelo).Property("statusContaPagar").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public ContasPagar Get(Guid id)
        {
            List<ContasPagar> retorno = new List<ContasPagar>();
            try
            {
                retorno = db.ContasP.FromSql("SELECT * FROM ContasPagar WHERE  id = '" + id.ToString() +  "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<ContasPagar> GetAll(Guid idOrg, int view)
        {
            List<ContasPagar> itens = new List<ContasPagar>();

            //Contas em Aberto
            if (view == 0)
            {
                itens = db.ContasP.FromSql("SELECT * FROM ContasPagar  where statusContaPagar in (0,3) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Contas Fechadas
            if (view == 1)
            {
                itens = db.ContasP.FromSql("SELECT * FROM ContasPagar  where statusContaPagar in (1,2) and  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos Contas   
            if (view == 2)
            {
                itens = db.ContasP.FromSql("SELECT * FROM ContasPagar  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            }

            //Todos Contas  a Pagar hoje 
            if (view == 3)
            {
                itens = db.ContasP.FromSql("SELECT * FROM ContasPagar  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

                IEnumerable<ContasPagar> retorno = itens;

                retorno = from u in retorno where (u.dataPagamento.Date == DateTime.Now.Date) & (u.statusContaPagar == CustomEnumStatus.StatusContaPagar.agendado || u.statusContaPagar == CustomEnumStatus.StatusContaPagar.atrasado) select u;

                itens = retorno.ToList();
            }

            return itens;

        }
    }
}
