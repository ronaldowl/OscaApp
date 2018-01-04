﻿using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace OscaApp.Data
{
    public class OrganizacaoData : IOrganizacaoData
    {
        private ContexDataManager db;  

        public OrganizacaoData(ContexDataManager dbContext)
        {
            this.db = dbContext;        
        }
    
        public void Update(Organizacao organizacao)
        {
            try
            {
                db.Attach(organizacao);
                db.Entry(organizacao).Property("nomeLogin").IsModified = true;
                db.Entry(organizacao).Property("nomeAmigavel").IsModified = true;
                db.Entry(organizacao).Property("cpf_cnpj").IsModified = true;                
                db.Entry(organizacao).Property("tipoPessoa").IsModified = true;
                db.Entry(organizacao).Property("quantidadeUsuario").IsModified = true;
                db.Entry(organizacao).Property("nomeAdministrador").IsModified = true;
                db.Entry(organizacao).Property("emailAdministrador").IsModified = true;
                db.Entry(organizacao).Property("telefoneAdministrador").IsModified = true;
                db.Entry(organizacao).Property("celularAdministrador").IsModified = true;
                db.Entry(organizacao).Property("cargo").IsModified = true; 
                db.Entry(organizacao).Property("logradouro").IsModified = true;
                db.Entry(organizacao).Property("estado").IsModified = true;
                db.Entry(organizacao).Property("cidade").IsModified = true;
                db.Entry(organizacao).Property("pais").IsModified = true;
                db.Entry(organizacao).Property("cep").IsModified = true;
                db.Entry(organizacao).Property("numero").IsModified = true;
                db.Entry(organizacao).Property("bairro").IsModified = true;
                db.Entry(organizacao).Property("complemento").IsModified = true;
                db.Entry(organizacao).Property("anotacao").IsModified = true;
                db.Entry(organizacao).Property("modificadoPor").IsModified = true;
                db.Entry(organizacao).Property("modificadoPorName").IsModified = true;
                db.Entry(organizacao).Property("modificadoEm").IsModified = true;
            

                db.SaveChanges(); 
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Organizacao Get(Guid idOrg)
        {
            List<Organizacao> retorno = new List<Organizacao>();
            retorno = db.Organizacao.FromSql("SELECT * FROM Organizacao where  id = '" + idOrg.ToString() + "'").ToList();
            return retorno[0];
        }
     
    }
}