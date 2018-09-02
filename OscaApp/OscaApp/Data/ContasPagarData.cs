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
                db.ContasP.Remove(modelo);
                db.SaveChanges();
        }


        public void Add(ContasPagar contasPagar)
        {          
                db.ContasP.Add(contasPagar);
                db.SaveChanges();
        }
        public void Update(ContasPagar modelo)
        {
       
                db.Attach(modelo);
           
                db.Entry(modelo).Property("titulo").IsModified                   = true;
                db.Entry(modelo).Property("valor").IsModified                    = true;
                db.Entry(modelo).Property("anotacao").IsModified                 = true;
                db.Entry(modelo).Property("tipoLancamento").IsModified           = true;
                db.Entry(modelo).Property("origemContaPagar").IsModified         = true;
                db.Entry(modelo).Property("dataPagamento").IsModified            = true;
                db.Entry(modelo).Property("dataFechamento").IsModified           = true;
                db.Entry(modelo).Property("statusContaPagar").IsModified         = true;
                db.Entry(modelo).Property("numeroReferencia").IsModified         = true;                                
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;                               

                db.SaveChanges();
        }
        public void UpdateStatus(ContasPagar modelo)
        {
    
                db.Attach(modelo);
           
                db.Entry(modelo).Property("statusContaPagar").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                
                db.SaveChanges();
        }
        public ContasPagar Get(Guid id)
        {
            List<ContasPagar> retorno = new List<ContasPagar>();
           
            retorno = (from A in db.ContasP where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<ContasPagar> GetAll(Guid idOrg, int view)
        {
            List<ContasPagar> itens = new List<ContasPagar>();

            //Contas em Aberto
            if (view == 0)
            {              
                itens = (from A in db.ContasP where A.idOrganizacao.Equals(idOrg) & (A.statusContaPagar == CustomEnumStatus.StatusContaPagar.agendado || A.statusContaPagar == CustomEnumStatus.StatusContaPagar.atrasado) select A).ToList();
            }

            //Contas Fechadas
            if (view == 1)
            {                
                itens = (from A in db.ContasP where A.idOrganizacao.Equals(idOrg) & (A.statusContaPagar == CustomEnumStatus.StatusContaPagar.cancelado || A.statusContaPagar == CustomEnumStatus.StatusContaPagar.pago) select A).ToList();
            }

            //Todos Contas   
            if (view == 2)
            {                
                itens = (from A in db.ContasP where A.idOrganizacao.Equals(idOrg)  select A).ToList();
            }

            //Todos Contas  a Pagar hoje 
            if (view == 3)
            {
                itens = (from A in db.ContasP where A.idOrganizacao.Equals(idOrg) & (A.dataPagamento.Date == DateTime.Now.Date) & (A.statusContaPagar == CustomEnumStatus.StatusContaPagar.agendado || A.statusContaPagar == CustomEnumStatus.StatusContaPagar.atrasado) select A).ToList();
                                 
            }

            return itens;

        }
    }
}
