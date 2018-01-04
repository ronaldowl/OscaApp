﻿using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class ItemListaPrecoData : IItemListaPrecoData
    {
        private ContexDataService db;       

        public ItemListaPrecoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(ItemListaPreco modelo)
        {
            try
            {
                db.ItemListaPrecos.Add(modelo);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }
           
        }
        public void Update(ItemListaPreco modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified               = true;
                db.Entry(modelo).Property("idListaPreco").IsModified        = true;             
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
        public ItemListaPreco Get(Guid id )
        {
            List<ItemListaPreco> retorno = new List<ItemListaPreco>();
            try
            {
               
                retorno = db.ItemListaPrecos.FromSql("SELECT * FROM itemlistapreco where  id = '" + id.ToString() +  "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<ItemListaPreco> GetAll(Guid idOrg)
        {
            List<ItemListaPreco> retorno = new List<ItemListaPreco>();
            retorno = db.ItemListaPrecos.FromSql("SELECT * FROM itemlistapreco  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ItemListaPreco> retorno = new List<ItemListaPreco>();
            List<Relacao> lista = new List<Relacao>();

            retorno = db.ItemListaPrecos.FromSql("SELECT * FROM itemlistapreco  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
    }
}