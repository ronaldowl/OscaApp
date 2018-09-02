using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ProfissionalData : IProfissionalData
    {
        private ContexDataService db;

          public ProfissionalData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(Profissional profissional)
        {           
                db.Profissionais.Add(profissional);
                db.SaveChanges();     
        }
        public void Update(Profissional modelo)
        {
         
                db.Attach(modelo);

                db.Entry(modelo).Property("tipoConta").IsModified = true;
             
                db.Entry(modelo).Property("numeroConta").IsModified              = true;
                db.Entry(modelo).Property("agencia").IsModified                  = true;
                db.Entry(modelo).Property("comissionado").IsModified             = true;
                db.Entry(modelo).Property("idBanco").IsModified                  = true;
                db.Entry(modelo).Property("idUsuario").IsModified = true;
                db.Entry(modelo).Property("nomeProfissional").IsModified = true;


                db.Entry(modelo).Property("percentualComissao").IsModified       = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();

        }
        public Profissional Get(Guid id)
        {
            List<Profissional> retorno = new List<Profissional>();
 
            retorno = (from A in db.Profissionais where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Profissional> GetAll(Guid idOrg)
        {
            List<Profissional> retorno = new List<Profissional>();
            retorno = (from A in db.Profissionais where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }
    }
}