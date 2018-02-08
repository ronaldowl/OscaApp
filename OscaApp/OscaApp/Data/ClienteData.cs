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
    public class ClienteData : IClienteData
    {
        private ContexDataService db;
        //public DbSet<Cliente> Books { get; set; }

        public  ClienteData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(Cliente cliente)
        {
            // try { 
           // List<Cliente> retorno =  new List<Cliente>(); ;
          //  retorno = db.Fornecedor.FromSql("SELECT * FROM Cliente where  cnpj_cpf = '" + cliente.cnpj_cpf.ToString() + "'").ToList();
         //   if (retorno == null) {
                try
                {
                    db.Fornecedor.Add(cliente);                
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
         //  }
        //  catch(Exception ex)
       //  {
      //       throw;
     //   }
    //  }
        public void Update(Cliente modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nomeCliente").IsModified        = true;
                db.Entry(modelo).Property("telefone").IsModified           = true;       
                db.Entry(modelo).Property("celular").IsModified            = true;               
                db.Entry(modelo).Property("cnpj_cpf").IsModified           = true;
                db.Entry(modelo).Property("email").IsModified              = true;
                db.Entry(modelo).Property("anotacao").IsModified           = true;
                db.Entry(modelo).Property("razaoSocial").IsModified        = true;
                db.Entry(modelo).Property("sexo").IsModified               = true;
                db.Entry(modelo).Property("tipoPessoa").IsModified         = true;
                db.Entry(modelo).Property("modificadoPor").IsModified      = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified  = true;
                db.Entry(modelo).Property("modificadoEm").IsModified       = true;
            
            
                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public Cliente Get(Guid id, Guid idOrg)
        {
            List<Cliente> retorno = new List<Cliente>();
            try
            {
               
                retorno = db.Fornecedor.FromSql("SELECT * FROM Cliente where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<Cliente> GetAll(Guid idOrg)
        {
            List<Cliente> retorno = new List<Cliente>();
            retorno = db.Fornecedor.FromSql("SELECT * FROM Cliente where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
