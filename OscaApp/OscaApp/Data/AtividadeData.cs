using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using OscaApp.framework.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class AtividadeData : IAtividadeData
    {
        private ContexDataService db;
        //public DbSet<Cliente> Books { get; set; }

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
                db.Entry(modelo).Property("idProprietario").IsModified      = true;
                db.Entry(modelo).Property("dataAlvo").IsModified            = true;
                db.Entry(modelo).Property("assunto").IsModified             = true;
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
        public Atividade Get(Guid id, Guid idOrg)
        {
            List<Atividade> retorno = new List<Atividade>();
            retorno = db.Atividades.FromSql("SELECT * FROM Atividade where  id = '" + id.ToString() + "' and idOrganizacao = '"+ idOrg.ToString() + "'" ).ToList();
            return retorno[0];
        }
        public List<Atividade> GetAll(Guid idOrg)
        {
            List<Atividade> retorno = new List<Atividade>();
            retorno = db.Atividades.FromSql("SELECT * FROM Atividade where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;
        }
    }
}
