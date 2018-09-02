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
    public class PedidoData : IPedidoData
    {
        private ContexDataService db;       

        public PedidoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Delete(Pedido modelo)
        {            
                db.Pedidos.Remove(modelo);
                db.SaveChanges();  
        }
        public void Add(Pedido modelo)
        {           
                db.Pedidos.Add(modelo);                
                db.SaveChanges();           
        }
        public void Update(Pedido modelo)
        {
          
                db.Attach(modelo);
                db.Entry(modelo).Property("anotacao").IsModified                = true;
                db.Entry(modelo).Property("valorTotal").IsModified              = true;
                db.Entry(modelo).Property("valorFrete").IsModified              = true;
                db.Entry(modelo).Property("valorDesconto").IsModified           = true;
                db.Entry(modelo).Property("valorDescontoPercentual").IsModified = true;
                db.Entry(modelo).Property("tipoDesconto").IsModified            = true;
                db.Entry(modelo).Property("condicaoPagamento").IsModified       = true;
                db.Entry(modelo).Property("tipoPagamento").IsModified           = true;
                db.Entry(modelo).Property("metodoEntrega").IsModified           = true;
                db.Entry(modelo).Property("statusPedido").IsModified            = true;
                db.Entry(modelo).Property("dataFechamento").IsModified          = true;
                db.Entry(modelo).Property("modificadoPor").IsModified           = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified       = true;
                db.Entry(modelo).Property("modificadoEm").IsModified            = true;
               
                db.SaveChanges();     

        }
        public Pedido Get(Guid id )
        {
            List<Pedido> retorno = new List<Pedido>();
       
            retorno = (from A in db.Pedidos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Pedido> GetAll(Guid idOrg)
        {
            List<Pedido> retorno = new List<Pedido>();
            retorno = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }       

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<Pedido> retorno = new List<Pedido>();  
            retorno = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) select A).ToList();            
            return Relacao.ConvertToRelacao(retorno);

        }

        public List<PedidoGridViewModel> GetAllGridViewModel(Guid idOrg, int view)
        {
            List<Pedido> itens = new List<Pedido>();

            //Em Andamento
            if (view == 0)
            {                 
                itens = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) & (A.statusPedido == CustomEnumStatus.StatusPedido.EmAndamento) select A).ToList();
            }

            //Aguardando produto
            if (view == 1)
            {                 
                itens = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) & (A.statusPedido == CustomEnumStatus.StatusPedido.AguardandoProduto) select A).ToList();
            }

            //Para Entrega
            if (view == 2)
            {
                itens = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) & (A.statusPedido == CustomEnumStatus.StatusPedido.ParaEntrega) select A).ToList();
            }

            //Todos  Fechado
            if (view == 3)
            {                
                itens = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) &   (A.statusPedido == CustomEnumStatus.StatusPedido.Fechado || A.statusPedido == CustomEnumStatus.StatusPedido.Cancelado) select A).ToList();
            }

            //Todos   
            if (view == 4)
            {                 
                itens = (from A in db.Pedidos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            }


            return HelperAssociate.ConvertToGridPedido(itens);
        }
        public List<PedidoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        {
            List<Pedido> itens = new List<Pedido>();

            itens = (from A in db.Pedidos where A.idCliente.Equals(idCliente) select A).ToList();

            return HelperAssociate.ConvertToGridPedido(itens);
        }

        public List<Pedido> GetAllByIdCliente(Guid idCliente)
        {
            List<Pedido> retorno = new List<Pedido>();
            retorno = (from A in db.Pedidos where A.idCliente.Equals(idCliente) select A).ToList();

            return retorno;

        }
    }
}
