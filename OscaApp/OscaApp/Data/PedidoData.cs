using System;
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
    }
}
