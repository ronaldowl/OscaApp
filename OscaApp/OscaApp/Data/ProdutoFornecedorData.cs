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
           
                db.ProdutosFornecedor.Add(modelo);                
                db.SaveChanges();     
           
        }
        public void Update(ProdutoFornecedor modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("valorCompra").IsModified               = true;
                db.Entry(modelo).Property("codigoProdutoFornecedor").IsModified = true;                       
                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;            
            
                db.SaveChanges(); 
  
        }
        public void Delete(ProdutoFornecedor modelo)
        {            
                db.Attach(modelo);
                db.Remove(modelo);
                db.SaveChanges();
        }
        public ProdutoFornecedor Get(Guid id )
        {
            List<ProdutoFornecedor> retorno = new List<ProdutoFornecedor>();                    
        
            retorno = (from A in db.ProdutosFornecedor where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<ProdutoFornecedor> GetAll(Guid idOrg)
        {
            List<ProdutoFornecedor> retorno = new List<ProdutoFornecedor>();
            retorno = (from A in db.ProdutosFornecedor where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
     
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ProdutoFornecedor> retorno = new List<ProdutoFornecedor>();

            retorno = (from A in db.ProdutosFornecedor where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
    
        public List<ProdutoFornecedorGridViewModel> GetAllByProduto(Guid idProduto)
        {
            List<ProdutoFornecedor> itens = new List<ProdutoFornecedor>();

            itens = (from A in db.ProdutosFornecedor where A.idProduto.Equals(idProduto) select A).ToList();

            return HelperAssociate.ConvertToGridProdutoFornecedor(itens);
        }

        public List<ProdutoFornecedorGridViewModel> GetAllByFornecedor(Guid idFornecedor)
        {
            List<ProdutoFornecedor> itens = new List<ProdutoFornecedor>();

            itens = (from A in db.ProdutosFornecedor where A.idFornecedor.Equals(idFornecedor) select A).ToList();

            return HelperAssociate.ConvertToGridProdutoFornecedor(itens);
        }

    }
}
