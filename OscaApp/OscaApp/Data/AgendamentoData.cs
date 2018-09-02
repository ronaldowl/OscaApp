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
                db.Agendamentos.Remove(modelo);
                db.SaveChanges();        
        }

        public void Add(Agendamento modelo)
        {
                db.Agendamentos.Add(modelo);                
                db.SaveChanges();       
           
        }
        public void Update(Agendamento modelo)
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
        public void UpdateStatus(Agendamento modelo)
        {
          
                db.Attach(modelo);
                db.Entry(modelo).Property("statusAgendamento").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;             
                
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
            
                db.SaveChanges();
        

        }
        public Agendamento Get(Guid id)
        {
            List<Agendamento> retorno = new List<Agendamento>();

            retorno = (from A in db.Agendamentos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Agendamento> GetAll(Guid idOrg)
        {
            List<Agendamento> retorno = new List<Agendamento>();
            retorno = (from A in db.Agendamentos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
           List<Agendamento> retorno = new List<Agendamento>();

            retorno = (from A in db.Agendamentos where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return Relacao.ConvertToRelacao(retorno);
            

        }
        public List<AgendamentoGridViewModel> GetAllGridViewModel(Guid idOrg,int view,string idProfissional)
        {
            List<Agendamento> itens = new List<Agendamento>();

            //Meus Agendamentos Abertos
            if (view == 0)
            {        
                itens = (from A in db.Agendamentos where A.idProfissional.Equals(idProfissional) & A.statusAgendamento == CustomEnumStatus.StatusAgendamento.agendado select A).ToList();
            }

            //Meus Agendamentos Fechados
            if (view == 1)
            {                
                itens = (from A in db.Agendamentos where A.idProfissional.Equals(idProfissional) & (A.statusAgendamento == CustomEnumStatus.StatusAgendamento.cancelado  || A.statusAgendamento == CustomEnumStatus.StatusAgendamento.concluido) select A).ToList();
            }

            //Todos Agendamentos Fechados
            if (view == 2)
            {
                itens = (from A in db.Agendamentos where A.idOrganizacao.Equals(idOrg) & (A.statusAgendamento == CustomEnumStatus.StatusAgendamento.cancelado || A.statusAgendamento == CustomEnumStatus.StatusAgendamento.concluido) select A).ToList();
            }

            //Todos Agendamentos Abertos
            if (view == 3)
            {            
                itens = (from A in db.Agendamentos where A.idOrganizacao.Equals(idOrg) & A.statusAgendamento == CustomEnumStatus.StatusAgendamento.agendado select A).ToList();
            }

            //Todos Agendamentos  
            if (view == 4)
            {
                itens = (from A in db.Agendamentos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            }

           
            return HelperAssociate.ConvertToGridAgendamento(itens);
        }          
        public List<AgendamentoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        {
            List<Agendamento> itens = new List<Agendamento>();

            itens = (from A in db.Agendamentos where A.idCliente.Equals(idCliente) select A).ToList();

            return HelperAssociate.ConvertToGridAgendamento(itens);
       
        }
        public List<AgendamentoGridViewModel> GetAllGridViewModelDia(Guid idProfissional)
        {
            List<Agendamento> itens = new List<Agendamento>();
            
            itens = (from A in db.Agendamentos where A.idProfissional.Equals(idProfissional) & A.dataAgendada == DateTime.Now.Date select A).ToList();
                      
            return HelperAssociate.ConvertToGridAgendamento(itens);
        }
        public List<Agendamento> GetAllByIdCliente(Guid idCliente)
        {
            List<Agendamento> retorno = new List<Agendamento>();

            retorno = (from A in db.Agendamentos where A.idCliente.Equals(idCliente) select A).ToList();
            return retorno;

        }
    }
}
