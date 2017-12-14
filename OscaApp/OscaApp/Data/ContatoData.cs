using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace OscaApp.Data
{
    public class ContatoData : IContatoData
    {
        private ContexDataService db;       

        public  ContatoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(Contato contato)
        {
            try
            {
                db.Contatos.Add(contato);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public void Update(Contato modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                = true;
                db.Entry(modelo).Property("telefone").IsModified            = true;       
                db.Entry(modelo).Property("celular").IsModified             = true;               
                db.Entry(modelo).Property("cpf").IsModified                 = true;
                db.Entry(modelo).Property("email").IsModified               = true;
                db.Entry(modelo).Property("numero").IsModified              = true;
                db.Entry(modelo).Property("logradouro").IsModified          = true;
                db.Entry(modelo).Property("sexo").IsModified                = true;
                db.Entry(modelo).Property("cidade").IsModified              = true;
                db.Entry(modelo).Property("estado").IsModified              = true;
                db.Entry(modelo).Property("bairro").IsModified              = true;
                db.Entry(modelo).Property("cep").IsModified                 = true;
                db.Entry(modelo).Property("complemento").IsModified         = true;
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
        public Contato Get(Guid id, Guid idOrg)
        {
            List<Contato> retorno = new List<Contato>();
            try
            {
               
                retorno = db.Contatos.FromSql("SELECT * FROM Contato where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<Contato> GetAll(Guid idOrg)
        {
            List<Contato> retorno = new List<Contato>();
            retorno = db.Contatos.FromSql("SELECT * FROM Contato where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
