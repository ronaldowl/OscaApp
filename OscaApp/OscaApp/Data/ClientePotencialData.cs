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
    public class ClientePotencialData : IClientePotencialData
    {
        private ContexDataService db;
       

        public ClientePotencialData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(ClientePotencial cliente)
        {                
                    db.ClientePotencial.Add(cliente);                
                    db.SaveChanges();
        }     
        public void Update(ClientePotencial modelo)
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
        public void SetStatus(ClientePotencial modelo)
        {
 
                db.Attach(modelo);
                db.Entry(modelo).Property("status").IsModified = true;                
                db.Entry(modelo).Property("statusLead").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                
                db.SaveChanges();             

        }
        public ClientePotencial Get(Guid id )
        {
            List<ClientePotencial> retorno = new List<ClientePotencial>();
      
            retorno = (from A in db.ClientePotencial where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<ClientePotencial> GetAll(Guid idOrg,int view)
        {
            List<ClientePotencial> retorno = new List<ClientePotencial>();

            //Cliente Ativo
            if (view == 0)
            {              
                retorno = (from A in db.ClientePotencial where A.idOrganizacao.Equals(idOrg) & A.statusLead == CustomEnumStatus.StatusLead.Ativo select A).ToList();

            }

            //Cliente Inativo
            if (view == 1)
            {             
                retorno = (from A in db.ClientePotencial where A.idOrganizacao.Equals(idOrg) & A.statusLead == CustomEnumStatus.StatusLead.Inativo select A).ToList();

            }

            //Qualificados  
            if (view == 2)
            {             
                retorno = (from A in db.ClientePotencial where A.idOrganizacao.Equals(idOrg) & A.statusLead == CustomEnumStatus.StatusLead.Qualificado select A).ToList();

            }

            return retorno;
        }
    }
}
