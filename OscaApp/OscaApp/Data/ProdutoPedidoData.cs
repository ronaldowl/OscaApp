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
    public class ProdutoPedidoData : IProdutoPedidoData
    {
        private ContexDataService db;

        public ProdutoPedidoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Delete(ProdutoPedido modelo)
        {            
                db.ProdutosPedido.Remove(modelo);
                db.SaveChanges();
        }
        public void Add(ProdutoPedido modelo)
        {           
                db.ProdutosPedido.Add(modelo);
                db.SaveChanges();
        }
        public void Update(ProdutoPedido modelo)
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
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
            
                db.SaveChanges();

        }
        public ProdutoPedido Get(Guid id)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            
            retorno = (from A in db.ProdutosPedido where A.id.Equals(id) select A).ToList();
        
            return retorno[0];
        }
        public List<ProdutoPedido> GetAll(Guid idOrg)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
        
            retorno = (from A in db.ProdutosPedido where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return retorno;

        }
        public List<ProdutoPedido> GetByPedidoId(Guid idPedido)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            retorno = (from A in db.ProdutosPedido where A.idPedido.Equals(idPedido) select A).ToList();
            return retorno;

        }
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            List<Relacao> lista = new List<Relacao>();

            retorno = (from A in db.ProdutosPedido where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
        public List<ProdutoPedidoGridViewModel> GetAllGridViewModel(Guid idPedido)
        {
            List<ProdutoPedido> itens = new List<ProdutoPedido>();

            itens = (from A in db.ProdutosPedido where A.idPedido.Equals(idPedido) select A).ToList();

            return HelperAssociate.ConvertToGridProdutoPedido(itens);
        }
       
    }
}
