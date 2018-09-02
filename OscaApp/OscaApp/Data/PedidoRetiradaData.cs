using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class PedidoRetiradaData : IPedidoRetiradaData
    {
        private ContexDataService db;

          public PedidoRetiradaData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Delete(PedidoRetirada modelo)
        {          
                db.PedidosRetirada.Remove(modelo);
                db.SaveChanges();       
        }
        public void Add(PedidoRetirada modelo)
        {        
                db.PedidosRetirada.Add(modelo);
                db.SaveChanges();      
        }
        public void Update(PedidoRetirada modelo)
        {            
                db.Attach(modelo);
                db.Entry(modelo).Property("tipoPagamento").IsModified            = true;
                db.Entry(modelo).Property("condicaoPagamento").IsModified        = true;          
                db.Entry(modelo).Property("valorTotal").IsModified               = true;
                db.Entry(modelo).Property("statusPedidoRetirada").IsModified     = true;
                db.Entry(modelo).Property("anotacao").IsModified                 = true;
                db.Entry(modelo).Property("dataFechamento").IsModified           = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;
                db.Entry(modelo).Property("dataRetirada").IsModified             = true;    
                db.Entry(modelo).Property("periodo").IsModified                  = true;
                db.Entry(modelo).Property("quantidade1").IsModified              = true;
                db.Entry(modelo).Property("quantidade2").IsModified = true;
                db.Entry(modelo).Property("valor1").IsModified = true;
                db.Entry(modelo).Property("valor2").IsModified = true;     
                db.Entry(modelo).Property("valorTotal1").IsModified = true;
                db.Entry(modelo).Property("valorTotal2").IsModified = true;
                db.Entry(modelo).Property("idProfissional").IsModified = true;
                db.Entry(modelo).Property("quantidade").IsModified = true;
                db.Entry(modelo).Property("dataEntrega").IsModified = true;
                db.Entry(modelo).Property("modeloCacamba1").IsModified = true;
                db.Entry(modelo).Property("modeloCacamba2").IsModified = true;
                db.Entry(modelo).Property("idEndereco").IsModified = true;
                db.Entry(modelo).Property("idEndereco2").IsModified = true;
            
                db.SaveChanges();       

        }

        public void UpdateStatus(PedidoRetirada modelo)
        {            
                db.Attach(modelo);   
                db.Entry(modelo).Property("statusPedidoRetirada").IsModified = true;            
                db.Entry(modelo).Property("dataFechamento").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;                       
                
                db.SaveChanges(); 
        }

        public PedidoRetirada Get(Guid id)
        {
            List<PedidoRetirada> retorno = new List<PedidoRetirada>();

            retorno = (from A in db.PedidosRetirada where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }

        public List<PedidoRetiradaGridViewModel> GetAll(Guid idOrg)
        {
            List<PedidoRetirada> retorno = new List<PedidoRetirada>();
            retorno = (from A in db.PedidosRetirada where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return HelperAssociate.ConvertToGridPedido(retorno);
        }

        public List<PedidoRetirada> GetAllByIdCliente(Guid idCliente)
        {
            List<PedidoRetirada> retorno = new List<PedidoRetirada>();
            retorno = (from A in db.PedidosRetirada where A.idCliente.Equals(idCliente) select A).ToList();
            return (retorno);
        }

        
    }
}