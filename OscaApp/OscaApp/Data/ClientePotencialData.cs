﻿using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ClientePotencialData : IClientePotencialData
    {
        private ContexDataService db;
       

        public ClientePotencialData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(ClientePotencial cliente)
        {        
                try
                {
                    db.ClientePotencial.Add(cliente);                
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }     
        public void Update(ClientePotencial modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nomeCliente").IsModified        = true;
                db.Entry(modelo).Property("telefone").IsModified           = true;       
                db.Entry(modelo).Property("celular").IsModified            = true;            
         
                db.Entry(modelo).Property("email").IsModified              = true;
                db.Entry(modelo).Property("anotacao").IsModified           = true;    
                db.Entry(modelo).Property("sexo").IsModified               = true;   
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
        public void SetStatus(ClientePotencial modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("status").IsModified = true;                
                db.Entry(modelo).Property("statusLead").IsModified = true;
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
        public ClientePotencial Get(Guid id )
        {
            List<ClientePotencial> retorno = new List<ClientePotencial>();
            try
            {
               
                retorno = db.ClientePotencial.FromSql("SELECT * FROM ClientePotencial where  id = '" + id.ToString() +  "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<ClientePotencial> GetAll(Guid idOrg,int view)
        {
            List<ClientePotencial> retorno = new List<ClientePotencial>();


            //Cliente Ativo
            if (view == 0)
            {
                retorno = db.ClientePotencial.FromSql("SELECT * FROM ClientePotencial where statusLead = 0 and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Cliente Inativo
            if (view == 1)
            {
                retorno = db.ClientePotencial.FromSql("SELECT * FROM ClientePotencial where statusLead = 1 and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Qualificados  
            if (view == 2)
            {
                retorno = db.ClientePotencial.FromSql("SELECT * FROM ClientePotencial where  statusLead = 2 and   idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            return retorno;

        }
    }
}
