using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ListaPrecoData : IListaPrecoData
    {
        private ContexDataService db;       

        public ListaPrecoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(ListaPreco listapreco)
        {
            try
            {
                db.ListaPrecos.Add(listapreco);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public void Update(ListaPreco modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                = true;
                db.Entry(modelo).Property("descricao").IsModified           = true;
                db.Entry(modelo).Property("dataValidade").IsModified        = true;
                db.Entry(modelo).Property("fimValidade").IsModified = true;
                
                db.Entry(modelo).Property("padrao").IsModified              = true;
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
        public ListaPreco Get(Guid id, Guid idOrg)
        {
            List<ListaPreco> retorno = new List<ListaPreco>();
            try
            {
               
                retorno = db.ListaPrecos.FromSql("SELECT * FROM listapreco where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<ListaPreco> GetAll(Guid idOrg)
        {
            List<ListaPreco> retorno = new List<ListaPreco>();
            retorno = db.ListaPrecos.FromSql("SELECT * FROM listapreco  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ListaPreco> retorno = new List<ListaPreco>();
            List<Relacao> lista = new List<Relacao>();

            retorno = db.ListaPrecos.FromSql("SELECT * FROM listapreco  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
    }
}
