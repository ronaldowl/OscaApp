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
    public class ProdutoFornecedorData : IProdutoFornecedorData
    {
        private ContexDataService db;       

        public ProdutoFornecedorData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(ProdutoFornecedor modelo)
        {
            try
            {
                db.ProdutosFornecedor.Add(modelo);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }
           
        }
        public void Update(ProdutoFornecedor modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("valorCompra").IsModified               = true;
                db.Entry(modelo).Property("codigoProdutoFornecedor").IsModified = true;                       
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
        public void Delete(ProdutoFornecedor modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public ProdutoFornecedor Get(Guid id )
        {
            List<ProdutoFornecedor> retorno = new List<ProdutoFornecedor>();
            try
            {
               
                retorno = db.ProdutosFornecedor.FromSql("SELECT * FROM ProdutoFornecedor where  id = '" + id.ToString() +  "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<ProdutoFornecedor> GetAll(Guid idOrg)
        {
            List<ProdutoFornecedor> retorno = new List<ProdutoFornecedor>();
            retorno = db.ProdutosFornecedor.FromSql("SELECT * FROM ProdutoFornecedor  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
        //public List<ProdutoFornecedorGridViewModel> GetAllByProduto(Guid idProduto)
        //{
        //    List<ProdutoFornecedor> itens = new List<ProdutoFornecedor>();

        //    itens = db.ItemListaPrecos.FromSql("SELECT * FROM itemlistapreco  where  idProduto = '" + idProduto.ToString() + "'").ToList();

        //    return itens;
        //}
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ProdutoFornecedor> retorno = new List<ProdutoFornecedor>();

            retorno = db.ProdutosFornecedor.FromSql("SELECT * FROM ProdutoFornecedor  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
        //public List<LookupItemLista> List<Fornecedor> GetAllByProduto(Guid idProduto)(Guid idLista)
        //{            
        //    List<ItemListaPreco> itens = new List<ItemListaPreco>();

        //    itens =  db.ItemListaPrecos.FromSql("SELECT * FROM itemlistapreco  where  idListaPreco = '" + idLista.ToString() + "'").ToList();

        //    return HelperAssociate.ConvertToLookupItemLista( itens);
        //}
        public List<ProdutoFornecedorGridViewModel> GetAllByProduto(Guid idProduto)
        {
            List<ProdutoFornecedor> itens = new List<ProdutoFornecedor>();

            itens = db.ProdutosFornecedor.FromSql("SELECT * FROM ProdutoFornecedor  where  idProduto= '" + idProduto.ToString() + "'").ToList();

            return HelperAssociate.ConvertToGridProdutoFornecedor(itens);
        }

    }
}
