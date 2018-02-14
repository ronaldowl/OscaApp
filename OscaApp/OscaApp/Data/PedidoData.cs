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
        public void Add(Pedido modelo)
        {
            try
            {
                db.Pedidos.Add(modelo);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }
           
        }
        public void Update(Pedido modelo)
        {
            try
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
        public Pedido Get(Guid id )
        {
            List<Pedido> retorno = new List<Pedido>();
            try
            {
               
                retorno = db.Pedidos.FromSql("SELECT * FROM Pedido where  id = '" + id.ToString() +  "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<Pedido> GetAll(Guid idOrg)
        {
            List<Pedido> retorno = new List<Pedido>();
            retorno = db.Pedidos.FromSql("SELECT * FROM Pedido  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }       

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<Pedido> retorno = new List<Pedido>(); 

            retorno = db.Pedidos.FromSql("SELECT * FROM Pedido  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }

        public List<PedidoGridViewModel> GetAllGridViewModel(Guid idOrg)
        {
            List<Pedido> itens = new List<Pedido>();

            itens = db.Pedidos.FromSql("SELECT * FROM Pedido  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return HelperAssociate.ConvertToGridPedido(itens);
        }

        public List<Pedido> GetAllByIdCliente(Guid idCliente)
        {
            List<Pedido> retorno = new List<Pedido>();
            retorno = db.Pedidos.FromSql("SELECT * FROM Pedido where  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }

    }
}
