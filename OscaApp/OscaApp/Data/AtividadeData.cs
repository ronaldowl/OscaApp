using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using OscaApp.framework.Models;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class AtividadeData : IAtividadeData
    {
        private ContexDataService db;  

        public AtividadeData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(Atividade atividade)
        {
            try
            {
                db.Atividades.Add(atividade);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public void Delete(Atividade atividade)
        {
            try
            {
                db.Atividades.Remove(atividade);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Update(Atividade modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("descricao").IsModified           = true;
                db.Entry(modelo).Property("tipo").IsModified                = true;
                db.Entry(modelo).Property("idProfissional").IsModified      = true;
                db.Entry(modelo).Property("dataAlvo").IsModified            = true;
                db.Entry(modelo).Property("assunto").IsModified             = true;
                db.Entry(modelo).Property("statusAtividade").IsModified = true;
                
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
        public void UpdateStatus(Atividade modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("dataFechamento").IsModified = true;              
                db.Entry(modelo).Property("statusAtividade").IsModified = true;
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
        public Atividade Get(Guid id)
        {
            List<Atividade> retorno = new List<Atividade>();
            retorno = db.Atividades.FromSql("SELECT * FROM Atividade where  id = '" + id.ToString() +  "'" ).ToList();
            return retorno[0];
        }
        public List<Atividade> GetAll(Guid idOrg)
        {
            List<Atividade> retorno = new List<Atividade>();
            retorno = db.Atividades.FromSql("SELECT * FROM Atividade where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }

        public List<AtividadeGridViewModel> GetAllGridViewModel(Guid idOrg, int view, string idProfissional)
        {
            List<Atividade> itens = new List<Atividade>();

            
            //Minhas Atividades Abertas
            if (view == 0)
            {
                itens = db.Atividades.FromSql("SELECT * FROM Atividade  where StatusAtividade = 0 and idProfissional = '" + idProfissional + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Minhas Atividades Fechadas
            if (view == 1)
            {
                itens = db.Atividades.FromSql("SELECT * FROM Atividade  where StatusAtividade in (1,2) and idProfissional = '" + idProfissional + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Atividades Fechadas
            if (view == 2)
            {
                itens = db.Atividades.FromSql("SELECT * FROM Atividade  where StatusAtividade in (1,2) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todas Atividades
            if (view == 3)
            {
                itens = db.Atividades.FromSql("SELECT * FROM Atividade  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            return HelperAssociate.ConvertToGridAtividade(itens);
        }
        public List<AtividadeGridViewModel> GetAllGridDia( string idProfissional)
        {
            List<Atividade> itens = new List<Atividade>();
  
                itens = db.Atividades.FromSql("SELECT * FROM Atividade  where StatusAtividade = 0 and idProfissional = '" + idProfissional + "'").ToList();
        

            return HelperAssociate.ConvertToGridAtividade(itens);
        }


    }
}
