using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class AtendimentoData : IAtendimentoData
    {
        private ContexDataService db;       

        public AtendimentoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }

        public void Delete(Atendimento modelo)
        {
            try
            {
                db.Atendimentos.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Add(Atendimento modelo)
        {
            try
            {
                db.Atendimentos.Add(modelo);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public void Update(Atendimento modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("dataAgendada").IsModified             = true;
                db.Entry(modelo).Property("idCliente").IsModified        = true;                    
                db.Entry(modelo).Property("problema").IsModified = true;
                db.Entry(modelo).Property("diagnostico").IsModified = true;               
                db.Entry(modelo).Property("observacao").IsModified = true;           
                db.Entry(modelo).Property("statusAtendimento").IsModified = true;
                db.Entry(modelo).Property("valor").IsModified = true;
                db.Entry(modelo).Property("titulo").IsModified = true;              
                db.Entry(modelo).Property("idServico").IsModified = true;
                db.Entry(modelo).Property("idProfissional").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;

                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;
            
            
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void UpdateStatus(Atendimento modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("statusAtendimento").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;               
                
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
        public Atendimento Get(Guid id)
        {
            List<Atendimento> retorno = new List<Atendimento>();
            try
            {
               
                retorno = db.Atendimentos.FromSql("SELECT * FROM Atendimento where  id = '" + id.ToString() + "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<Atendimento> GetAll(Guid idOrg)
        {
            List<Atendimento> retorno = new List<Atendimento>();
            retorno = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
           List<Relacao> retorno = new List<Relacao>();
            //List<Relacao> lista = new List<Relacao>();

            //retorno = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            //return Relacao.ConvertToRelacao(retorno);
            return retorno;

        }
        public List<AtendimentoGridViewModel> GetAllGridViewModel(Guid idOrg,int view,string idProfissional)
        {
            List<Atendimento> itens = new List<Atendimento>();

            //Meus Atendimentos abertos
            if (view == 0)
            {
                itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where statusAtendimento = 0 and idProfissional = '" + idProfissional + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Meus Atendimentos Fechados
            if (view == 1)
            {
                itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where statusAtendimento in (1,2) and idProfissional = '" + idProfissional + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos Fechados
            if (view == 2)
            {
                itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where statusAtendimento in (1,2) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos  Abertos
            if (view == 3)
            {
                itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where statusAtendimento in (0) and  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos   
            if (view == 4)
            {
                itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

           
            return HelperAssociate.ConvertToGridAtendimento(itens);
        }          
        public List<AtendimentoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        {
            List<Atendimento> itens = new List<Atendimento>();

            itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where  idCliente = '" + idCliente.ToString() + "'").ToList();

            return HelperAssociate.ConvertToGridAtendimento(itens);
       
        }
        public List<AtendimentoGridViewModel> GetAllGridViewModelDia(Guid idProfissional)
        {
            List<Atendimento> itens = new List<Atendimento>();

            itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where  idProfissional = '" + idProfissional.ToString()  + "' and Cast(dataAgendada as date) = Cast(getdate() as date) ").ToList();

            return HelperAssociate.ConvertToGridAtendimento(itens);
        }
        public List<Atendimento> GetAllByIdCliente(Guid idCliente)
        {
            List<Atendimento> retorno = new List<Atendimento>();
            retorno = db.Atendimentos.FromSql("SELECT * FROM Atendimento where  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }
    }
}
