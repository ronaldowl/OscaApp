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
    public class AgendamentoData : IAgendamentoData
    {
        private ContexDataService db;       

        public AgendamentoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }

        public void Delete(Agendamento modelo)
        {
            try
            {
                db.Agendamentos.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void Add(Agendamento modelo)
        {
            try
            {
                db.Agendamentos.Add(modelo);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public void Update(Agendamento modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("dataAgendada").IsModified             = true;
                db.Entry(modelo).Property("idCliente").IsModified        = true;                    
                     
                db.Entry(modelo).Property("statusAgendamento").IsModified = true;
           
                      
                db.Entry(modelo).Property("idReferencia").IsModified = true;
                db.Entry(modelo).Property("tipoReferencia").IsModified = true;

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
        public void UpdateStatus(Agendamento modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("statusAgendamento").IsModified = true;
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
        public Agendamento Get(Guid id)
        {
            List<Agendamento> retorno = new List<Agendamento>();
            try
            {
               
                retorno = db.Agendamentos.FromSql("SELECT * FROM Agendamento where  id = '" + id.ToString() + "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<Agendamento> GetAll(Guid idOrg)
        {
            List<Agendamento> retorno = new List<Agendamento>();
            retorno = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
           List<Agendamento> retorno = new List<Agendamento>();
            //List<Relacao> lista = new List<Relacao>();

            retorno = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);
            

        }
        public List<AgendamentoGridViewModel> GetAllGridViewModel(Guid idOrg,int view,string idProfissional)
        {
            List<Agendamento> itens = new List<Agendamento>();

            //Meus Atendimentos abertos
            if (view == 0)
            {
                itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where statusAgendamento = 0 and idProfissional = '" + idProfissional + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Meus Atendimentos Fechados
            if (view == 1)
            {
                itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where statusAgendamento in (1,2) and idProfissional = '" + idProfissional + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos Fechados
            if (view == 2)
            {
                itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where statusAgendamento in (1,2) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos  Abertos
            if (view == 3)
            {
                itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where statusAgendamento in (0) and  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos   
            if (view == 4)
            {
                itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

           
            return HelperAssociate.ConvertToGridAgendamento(itens);
        }          
        public List<AgendamentoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        {
            List<Agendamento> itens = new List<Agendamento>();

            itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where  idCliente = '" + idCliente.ToString() + "'").ToList();

            return HelperAssociate.ConvertToGridAgendamento(itens);
       
        }
        public List<AgendamentoGridViewModel> GetAllGridViewModelDia(Guid idProfissional)
        {
            List<Agendamento> itens = new List<Agendamento>();

            itens = db.Agendamentos.FromSql("SELECT * FROM Agendamento  where  idProfissional = '" + idProfissional.ToString()  + "' and Cast(dataAgendada as date) = Cast(getdate() as date) order by criadoEm desc").ToList();

            return HelperAssociate.ConvertToGridAgendamento(itens);
        }
        public List<Agendamento> GetAllByIdCliente(Guid idCliente)
        {
            List<Agendamento> retorno = new List<Agendamento>();
            retorno = db.Agendamentos.FromSql("SELECT * FROM Agendamento where  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }
    }
}
