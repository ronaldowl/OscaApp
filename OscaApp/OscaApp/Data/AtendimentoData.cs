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
                db.Entry(modelo).Property("laudo").IsModified = true;
                db.Entry(modelo).Property("observacao").IsModified = true;
                db.Entry(modelo).Property("horaInicio").IsModified = true;
                db.Entry(modelo).Property("horaFim").IsModified = true;
                db.Entry(modelo).Property("statusAtendimento").IsModified = true;

                db.Entry(modelo).Property("titulo").IsModified = true;
                db.Entry(modelo).Property("tipoReferencia").IsModified = true;
                db.Entry(modelo).Property("idReferencia").IsModified = true;
                 

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
        public List<AtendimentoGridViewModel> GetAllGridViewModel(Guid idOrg)
        {
            List<Atendimento> itens = new List<Atendimento>();

            itens = db.Atendimentos.FromSql("SELECT * FROM Atendimento  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return HelperAssociate.ConvertToGridAtendimento(itens);
        }
    }
}
