using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class ProdutoOrdemData : IProdutoOrdemData
    {
        private ContexDataService db;

        public ProdutoOrdemData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Delete(ProdutoOrdem modelo)
        {            
                db.ProdutosOrdem.Remove(modelo);
                db.SaveChanges();      

        }
        public void Add(ProdutoOrdem modelo)
        {         
                db.ProdutosOrdem.Add(modelo);
                db.SaveChanges(); 

        }
        public void Update(ProdutoOrdem modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified = true;
                db.Entry(modelo).Property("valorDescontoMoney").IsModified = true;
                db.Entry(modelo).Property("valorDescontoPercentual").IsModified = true;
                db.Entry(modelo).Property("tipoDesconto").IsModified = true;
                db.Entry(modelo).Property("quantidade").IsModified = true;
                db.Entry(modelo).Property("valorDesconto").IsModified = true;
                db.Entry(modelo).Property("total").IsModified = true;
                db.Entry(modelo).Property("totalGeral").IsModified = true;
                db.Entry(modelo).Property("idProduto").IsModified = true;
                db.Entry(modelo).Property("idListaPreco").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
            
                db.SaveChanges();   

        }
        public ProdutoOrdem Get(Guid id)
        {
            List<ProdutoOrdem> retorno = new List<ProdutoOrdem>();
                     
            retorno = (from A in db.ProdutosOrdem where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<ProdutoOrdem> GetAll(Guid idOrg)
        {
            List<ProdutoOrdem> retorno = new List<ProdutoOrdem>();
            retorno = (from A in db.ProdutosOrdem where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
        public List<ProdutoOrdem> GetByProdutoOrdemId(Guid idOrdemServico)
        {
            List<ProdutoOrdem> retorno = new List<ProdutoOrdem>();
            retorno = (from A in db.ProdutosOrdem where A.idOrdemServico.Equals(idOrdemServico) select A).ToList();

            return retorno;

        }
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ProdutoOrdem> retorno = new List<ProdutoOrdem>();
            List<Relacao> lista = new List<Relacao>();

            retorno = (from A in db.ProdutosOrdem where A.idOrganizacao.Equals(idOrg) select A).ToList();
            
            return Relacao.ConvertToRelacao(retorno);

        }
        public List<ProdutoOrdemGridViewModel> GetAllGridViewModel(Guid idOrdem)
        {
            List<ProdutoOrdem> itens = new List<ProdutoOrdem>();
                  
            itens = (from A in db.ProdutosOrdem where A.idOrdemServico.Equals(idOrdem) select A).ToList();

            return HelperAssociate.ConvertToGridProdutoOrdem(itens);
        }
    }
}
