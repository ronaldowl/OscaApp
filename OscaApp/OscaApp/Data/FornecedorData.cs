using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class FornecedorData : IFornecedorData
    {
        private ContexDataService db;

          public  FornecedorData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(Fornecedor fornecedor)
        {
            try
            {
                db.Fornecedores.Add(fornecedor);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(Fornecedor modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nomeFornecedor").IsModified           = true;
                db.Entry(modelo).Property("cnpj").IsModified                     = true;
                db.Entry(modelo).Property("nomeVendedor").IsModified             = true;
                db.Entry(modelo).Property("telefone").IsModified                 = true;
                db.Entry(modelo).Property("email").IsModified                    = true;
                db.Entry(modelo).Property("anotacao").IsModified                 = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public Fornecedor Get(Guid id)
        {
            List<Fornecedor> retorno = new List<Fornecedor>();
            try
            {

                retorno = db.Fornecedores.FromSql("SELECT * FROM fornecedor where  id = '" + id.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<Fornecedor> GetAll(Guid idOrg)
        {
            List<Fornecedor> retorno = new List<Fornecedor>();
            retorno = db.Fornecedores.FromSql("SELECT * FROM fornecedor where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
