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

        public List<AtividadeGridViewModel> GetAllGridViewModel(Guid idOrg)
        {
            List<Atividade> itens = new List<Atividade>();

            itens = db.Atividades.FromSql("SELECT * FROM Atividade  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return HelperAssociate.ConvertToGridAtividade(itens);
        }

    }
}
