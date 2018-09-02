using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class EnderecoData : IEnderecoData
    {
        private ContexDataService db;     

        public EnderecoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Delete(Endereco end)
        {
                db.Enderecos.Remove(end);
                db.SaveChanges(); 
        }
        public void Add(Endereco end)
        { 
                db.Enderecos.Add(end);                
                db.SaveChanges();
           
        }
        public void Update(Endereco end)
        {
         
                db.Attach(end);
        
                db.Entry(end).Property("logradouro").IsModified = true;
                db.Entry(end).Property("numero").IsModified = true;
                db.Entry(end).Property("cep").IsModified = true;
                db.Entry(end).Property("estado").IsModified = true;
                db.Entry(end).Property("cidade").IsModified = true;
                db.Entry(end).Property("complemento").IsModified = true;
                db.Entry(end).Property("anotacao").IsModified = true;
                db.Entry(end).Property("bairro").IsModified = true;

                db.SaveChanges(); 
  
        }
        public Endereco Get(Guid id)
        {
            List<Endereco> retorno = new List<Endereco>();          
            retorno = (from A in db.Enderecos where A.id.Equals(id) select A).ToList();
            return retorno[0];
        }
        public List<Endereco> GetAll(Guid idOrg)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = (from A in db.Enderecos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }

        public List<Endereco> GetAllByIdClinte(Guid idCliente)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = (from A in db.Enderecos where A.idCliente.Equals(idCliente) select A).ToList();
            return retorno;

        }

        public List<Endereco> GetByCliente(Guid idCliente)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = (from A in db.Enderecos where A.idCliente.Equals(idCliente) select A).ToList();
            return retorno;
        }
    }
}
