using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;
using OscaApp.framework;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Data
{
    public class ContasReceberData : IContasReceberData
    {
        private ContexDataService db;

        public ContasReceberData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Delete(ContasReceber contasReceber)
        {
          
                db.ContasR.Remove(contasReceber);
                db.SaveChanges();
        
        }

        public void Add(ContasReceber contasReceber)
        {
          
                db.ContasR.Add(contasReceber);
                db.SaveChanges();
        
        }
        public void Update(ContasReceber modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("titulo").IsModified = true;
                db.Entry(modelo).Property("valor").IsModified = true;
                db.Entry(modelo).Property("valorRestante").IsModified = true;
                db.Entry(modelo).Property("valorPago").IsModified = true;
                db.Entry(modelo).Property("anotacao").IsModified = true;
                db.Entry(modelo).Property("tipoLancamento").IsModified = true;
                db.Entry(modelo).Property("origemContaReceber").IsModified = true;
                db.Entry(modelo).Property("dataPagamento").IsModified = true;
                db.Entry(modelo).Property("statusContaReceber").IsModified = true;
                db.Entry(modelo).Property("numeroReferencia").IsModified = true;
                db.Entry(modelo).Property("idCliente").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                db.Entry(modelo).Property("dataRecebimento").IsModified = true;
                db.Entry(modelo).Property("valorRestante").IsModified = true;
                
                db.SaveChanges();
          

        }
        public void UpdateStatus(ContasReceber modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("valorRestante").IsModified = true;
                db.Entry(modelo).Property("valorPago").IsModified = true;
                db.Entry(modelo).Property("dataRecebimento").IsModified = true;
                db.Entry(modelo).Property("statusContaReceber").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;              

                db.SaveChanges();       

        }
        public ContasReceber Get(Guid id)
        {
            List<ContasReceber> retorno = new List<ContasReceber>();
            retorno = (from A in db.ContasR where A.id.Equals(id) select A).ToList();
        
            return retorno[0];
        }
        public List<ContasReceberGridViewModel> GetAll(Guid idOrg, int view, ClienteData clienteData)
        {
            List<ContasReceber> itens = new List<ContasReceber>();


            //Contas em Aberto
            if (view == 0)
            {
                itens = (from A in db.ContasR where A.idOrganizacao.Equals(idOrg) & (A.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || A.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select A).ToList();
            }

            //Contas Fechadas
            if (view == 1)
            {
                itens = (from A in db.ContasR where A.idOrganizacao.Equals(idOrg) & (A.statusContaReceber == CustomEnumStatus.StatusContaReceber.cancelado || A.statusContaReceber == CustomEnumStatus.StatusContaReceber.recebido) select A ).ToList();
            }

            //Todos Contas a receber  
            if (view == 2)
            {               
                itens = (from A in db.ContasR where A.idOrganizacao.Equals(idOrg) select A).ToList();
            }

            //Todos Contas  a receber hoje 
            if (view == 3)
            {  
                IEnumerable <ContasReceber> retorno = itens;                
                retorno = from u in db.ContasR where (u.dataPagamento.Date == DateTime.Now.Date & u.idOrganizacao.Equals(idOrg)) &  (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select u;
                itens = retorno.ToList();
            }

            //Todos Contas  a receber em Atraso
            if (view == 4)
            {                
                IEnumerable<ContasReceber> retorno = itens;
                retorno = from u in db.ContasR where (u.dataPagamento.Date < DateTime.Now.Date & u.idOrganizacao.Equals(idOrg)) & (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select u;
                itens = retorno.ToList();
            }

            return HelperAssociate.ConvertToGridContasReceber(itens.ToList(), clienteData);
        }
        public List<ContasReceber> GetAllByIdCliente(Guid idCliente, int view)
        {
            List<ContasReceber> itens = new List<ContasReceber>();


            //Contas em Aberto
            if (view == 0)
            {
                itens = (from A in db.ContasR where A.idCliente.Equals(idCliente) & (A.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || A.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select A).ToList();
            }

            //Contas Fechadas
            if (view == 1)
            {
                itens = (from A in db.ContasR where A.idCliente.Equals(idCliente) & (A.statusContaReceber == CustomEnumStatus.StatusContaReceber.cancelado || A.statusContaReceber == CustomEnumStatus.StatusContaReceber.recebido) select A).ToList();
            }

            //Todos Contas a receber  
            if (view == 2)
            {
                itens = (from A in db.ContasR where A.idCliente.Equals(idCliente) select A).ToList();
            }

            //Todos Contas  a receber hoje 
            if (view == 3)
            {
                IEnumerable<ContasReceber> retorno = itens;
                retorno = from u in db.ContasR where (u.dataPagamento.Date == DateTime.Now.Date & u.idCliente.Equals(idCliente)) & (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select u;
                itens = retorno.ToList();
            }

            //Todos Contas  a receber em Atraso
            if (view == 4)
            {
                IEnumerable<ContasReceber> retorno = itens;
                retorno = from u in db.ContasR where (u.dataPagamento.Date < DateTime.Now.Date & u.idCliente.Equals(idCliente)) & (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select u;
                itens = retorno.ToList();
            }

            return itens;

        }
        public List<ContasReceber> GetAllDia(Guid idOrg)
        {
            List<ContasReceber> itens = (from bl in db.ContasR where (bl.criadoEm.Date == DateTime.Now.Date) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();

            return itens;
        }
    }
}