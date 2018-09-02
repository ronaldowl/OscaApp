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
                db.Atendimentos.Remove(modelo);
                db.SaveChanges();     
        }

        public void Add(Atendimento modelo)
        {     
                db.Atendimentos.Add(modelo);                
                db.SaveChanges();           
        }
        public void Update(Atendimento modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("dataAgendada").IsModified             = true;
                db.Entry(modelo).Property("idCliente").IsModified        = true;                    
                db.Entry(modelo).Property("problema").IsModified = true;
                db.Entry(modelo).Property("diagnostico").IsModified = true;               
                db.Entry(modelo).Property("observacao").IsModified = true;           
                db.Entry(modelo).Property("statusAtendimento").IsModified = true;
                db.Entry(modelo).Property("valor").IsModified = true;     
                db.Entry(modelo).Property("idServico").IsModified = true;
                db.Entry(modelo).Property("idProfissional").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;
                db.Entry(modelo).Property("CondicaoPagamento").IsModified = true;

                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;
                        
                db.SaveChanges(); 
            
        }
        public void UpdateStatus(Atendimento modelo)
        {
      
                db.Attach(modelo);
                db.Entry(modelo).Property("statusAtendimento").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;              
                
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                
                db.SaveChanges();
            
        }
        public Atendimento Get(Guid id)
        {
            List<Atendimento> retorno = new List<Atendimento>();
 
            retorno = (from A in db.Atendimentos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Atendimento> GetAll(Guid idOrg)
        {
            List<Atendimento> retorno = new List<Atendimento>();
            retorno = (from A in db.Atendimentos where A.idOrganizacao.Equals(idOrg) select A).ToList();
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
                itens = (from A in db.Atendimentos where A.idProfissional.Equals(idProfissional) & A.statusAtendimento == CustomEnumStatus.StatusAtendimento.agendado select A).ToList();
            }

            //Meus Atendimentos Fechados
            if (view == 1)
            {
                itens = (from A in db.Atendimentos where A.idProfissional.Equals(idProfissional) & (A.statusAtendimento == CustomEnumStatus.StatusAtendimento.cancelado || A.statusAtendimento == CustomEnumStatus.StatusAtendimento.atendido) select A).ToList();
            }

            //Todos Fechados
            if (view == 2)
            {
                itens = (from A in db.Atendimentos where A.idOrganizacao.Equals(idOrg) & (A.statusAtendimento == CustomEnumStatus.StatusAtendimento.cancelado || A.statusAtendimento == CustomEnumStatus.StatusAtendimento.atendido) select A).ToList();
            }

            //Todos  Abertos
            if (view == 3)
            {
                itens = (from A in db.Atendimentos where A.idOrganizacao.Equals(idOrg) & A.statusAtendimento == CustomEnumStatus.StatusAtendimento.agendado select A).ToList();
            }

            //Todos   
            if (view == 4)
            {
                itens = (from A in db.Atendimentos where A.idOrganizacao.Equals(idOrg)   select A).ToList();
            }

           
            return HelperAssociate.ConvertToGridAtendimento(itens);
        }          
        public List<AtendimentoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        {
            List<Atendimento> itens = new List<Atendimento>();

            itens = (from A in db.Atendimentos where A.idCliente.Equals(idCliente) select A).ToList();

            return HelperAssociate.ConvertToGridAtendimento(itens);
       
        }
        public List<AtendimentoGridViewModel> GetAllGridViewModelDia(Guid idProfissional)
        {
            List<Atendimento> itens = new List<Atendimento>();

            itens = (from A in db.Atendimentos where A.idProfissional.Equals(idProfissional) & A.dataAgendada == DateTime.Now.Date select A).ToList();

            return HelperAssociate.ConvertToGridAtendimento(itens);
        }
        public List<Atendimento> GetAllByIdCliente(Guid idCliente)
        {
            List<Atendimento> retorno = new List<Atendimento>();
            retorno = (from A in db.Atendimentos where A.idCliente.Equals(idCliente) select A).ToList();
            return retorno;

        }
    }
}
