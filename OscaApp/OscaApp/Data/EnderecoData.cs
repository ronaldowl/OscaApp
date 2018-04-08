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
            try
            {
                db.Enderecos.Remove(end);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Add(Endereco end)
        {
            try
            {
                db.Enderecos.Add(end);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public void Update(Endereco end)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }

        }
        public Endereco Get(Guid id)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = db.Enderecos.FromSql("SELECT * FROM Endereco where  id = '" + id.ToString()  + "'" ).ToList();
            return retorno[0];
        }
        public List<Endereco> GetAll(Guid idOrg)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = db.Enderecos.FromSql("SELECT * FROM Endereco where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }

        public List<Endereco> GetAllByIdClinte(Guid idCliente)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = db.Enderecos.FromSql("SELECT * FROM Endereco where  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }

        public List<Endereco> GetByCliente(Guid idCliente)
        {
            List<Endereco> retorno = new List<Endereco>();
            retorno = db.Enderecos.FromSql("SELECT * FROM Endereco where  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }
    }
}
