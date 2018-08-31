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
                try
                {
                    db.Clientes.Add(cliente);                
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }     
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
                db.Entry(modelo).Property("idContato").IsModified          = true;
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
        public void SetStatus(Cliente modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("status").IsModified = true;                
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
        public Cliente Get(Guid id)
        {
            List<Cliente> retorno = new List<Cliente>();
          
            retorno = (from A in db.Clientes where A.id.Equals(id) select A).ToList();
        
            return retorno[0];
        }
        public List<Cliente> GetAll(Guid idOrg,int view)
        {
            List<Cliente> retorno = new List<Cliente>();


            //Cliente Inativo
            if (view == 0)
            {                
                retorno = (from A in db.Clientes where A.idOrganizacao.Equals(idOrg) & A.status == CustomEnumStatus.Status.Inativo select A).ToList();
            }

            //Cliente Ativo
            if (view == 1)
            {
                retorno = (from A in db.Clientes where A.idOrganizacao.Equals(idOrg) & A.status == CustomEnumStatus.Status.Ativo select A).ToList();
            }

            //Todos Clientes  
            if (view == 2)
            {
                retorno = (from A in db.Clientes where A.idOrganizacao.Equals(idOrg) select A).ToList();
            }

            return retorno;

        }
    }
}
