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
            try
            {
                db.ContasR.Remove(contasReceber);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void Add(ContasReceber contasReceber)
        {
            try
            {
                db.ContasR.Add(contasReceber);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(ContasReceber modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("titulo").IsModified = true;
                db.Entry(modelo).Property("valor").IsModified = true;
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


                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void UpdateStatus(ContasReceber modelo)
        {
            try
            {
                db.Attach(modelo);


                db.Entry(modelo).Property("dataRecebimento").IsModified = true;
                db.Entry(modelo).Property("statusContaReceber").IsModified = true;
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
        public ContasReceber Get(Guid id)
        {
            List<ContasReceber> retorno = new List<ContasReceber>();
            try
            {
                retorno = db.ContasR.FromSql("SELECT * FROM ContasReceber WHERE  id = '" + id.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<ContasReceberGridViewModel> GetAll(Guid idOrg, int view)
        {
            List<ContasReceber> itens = new List<ContasReceber>();


            //Contas em Aberto
            if (view == 0)
            {
                itens = db.ContasR.FromSql("SELECT * FROM ContasReceber  where statusContaReceber in (0,3) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Contas Fechadas
            if (view == 1)
            {
                itens = db.ContasR.FromSql("SELECT * FROM ContasReceber  where statusContaReceber in (1,2) and  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos Contas   
            if (view == 2)
            {
                itens = db.ContasR.FromSql("SELECT * FROM ContasReceber  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            }

            //Todos Contas  a receber hoje 
            if (view == 3)
            {
                itens = db.ContasR.FromSql("SELECT * FROM ContasReceber  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

                IEnumerable<ContasReceber> retorno = itens;

                retorno = from u in retorno where (u.dataPagamento.Date == DateTime.Now.Date) & (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select u;

                itens = retorno.ToList();
            }

            //Todos Contas  a receber em Atraso
            if (view == 4)
            {
                itens = db.ContasR.FromSql("SELECT * FROM ContasReceber  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

                IEnumerable<ContasReceber> retorno = itens;

                retorno = from u in retorno where (u.dataPagamento.Date < DateTime.Now.Date) & (u.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || u.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado) select u;

                itens = retorno.ToList();
            }

            return HelperAssociate.ConvertToGridContasReceber(itens.ToList());
        }

        public List<ContasReceber> GetAllByIdCliente(Guid idCliente)
        {
            List<ContasReceber> retorno = new List<ContasReceber>();
            retorno = db.ContasR.FromSql("SELECT * FROM ContasReceber WHERE  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }
        public List<ContasReceber> GetAllDia(Guid idOrg)
        {
            List<ContasReceber> itens = (from bl in db.ContasR where (bl.criadoEm.Date == DateTime.Now.Date) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();

            return itens;
        }
    }
}