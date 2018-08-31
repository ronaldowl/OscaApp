using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaApp.framework;
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.Models;

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
           
                db.ItemListaPrecos.Add(modelo);                
                db.SaveChanges();        
           
        }
        public void Update(ItemListaPreco modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified               = true;
                db.Entry(modelo).Property("valorMinimo").IsModified = true;
                db.Entry(modelo).Property("idListaPreco").IsModified        = true;             
                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;
                        
                db.SaveChanges();         

        }
        public ItemListaPreco Get(Guid id )
        {
            List<ItemListaPreco> retorno = new List<ItemListaPreco>();
                          
            retorno = (from bl in db.ItemListaPrecos where bl.id.Equals(id) select bl).ToList();

            return retorno[0];
        }
        public List<ItemListaPreco> GetAll(Guid idOrg)
        {
            List<ItemListaPreco> retorno = new List<ItemListaPreco>();
            retorno = (from bl in db.ItemListaPrecos where bl.idOrganizacao.Equals(idOrg) select bl).ToList();
            return retorno;

        }
        public List<ItemListaPreco> GetAllByProduto(Guid idProduto)
        {
            List<ItemListaPreco> itens = new List<ItemListaPreco>();

            itens = (from bl in db.ItemListaPrecos where bl.idProduto.Equals(idProduto) select bl).ToList();

            return itens;
        }      
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ItemListaPreco> retorno = new List<ItemListaPreco>();

            retorno = (from bl in db.ItemListaPrecos where bl.idOrganizacao.Equals(idOrg) select bl).ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
        public List<LookupItemLista> GetAllByListaPreco(Guid idLista)
        {            
            List<ItemListaPreco> itens = new List<ItemListaPreco>();
                      
            itens = (from bl in db.ItemListaPrecos where bl.idListaPreco.Equals(idLista) select bl).ToList();

            return HelperAssociate.ConvertToLookupItemLista( itens);
        }
        public List<LookupItemLista> GetAllByIdProduto(Guid idProduto)
        {
            List<ItemListaPreco> itens = new List<ItemListaPreco>();

            itens = (from bl in db.ItemListaPrecos where bl.idProduto.Equals(idProduto) select bl).ToList();

            return HelperAssociate.ConvertToLookupItemLista(itens);
        }

    }
}
