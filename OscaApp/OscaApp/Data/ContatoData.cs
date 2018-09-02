using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContatoData : IContatoData
    {
        private ContexDataService db;       

        public  ContatoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Delete(Contato modelo)
        { 
                db.Contatos.Remove(modelo);
                db.SaveChanges(); 
        }


        public void Add(Contato contato)
        {      
                db.Contatos.Add(contato);                
                db.SaveChanges();
        }
        public void Update(Contato modelo)
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
        public Contato Get(Guid id)
        {
            List<Contato> retorno = new List<Contato>();

            retorno = (from A in db.Contatos where A.id.Equals(id) select A).ToList();
            return retorno[0];
        }
        public List<Contato> GetAll(Guid idOrg)
        {
            List<Contato> retorno = new List<Contato>();
            retorno = (from A in db.Contatos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }
    }
}
