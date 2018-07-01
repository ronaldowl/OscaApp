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
using OscaApp.ViewModels;

namespace OscaApp.Data
{
    public class BalcaoVendasData : IBalcaoVendasData
    {
        private ContexDataService db;       

        public BalcaoVendasData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Delete(BalcaoVendas modelo)
        {
            try
            {
                db.BalcaoVendas.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }
        public void Add(BalcaoVendas modelo)
        {
            try
            {
                db.BalcaoVendas.Add(modelo);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }
           
        }
        public void Update(BalcaoVendas modelo)
        {
            try
            {
                db.Attach(modelo);
              
                db.Entry(modelo).Property("valorTotal").IsModified              = true;         
           
                db.Entry(modelo).Property("condicaoPagamento").IsModified       = true;
                db.Entry(modelo).Property("tipoPagamento").IsModified           = true;           
                db.Entry(modelo).Property("statusBalcaoVendas").IsModified       = true;              

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
        public BalcaoVendas Get(Guid id )
        {
            List<BalcaoVendas> retorno = new List<BalcaoVendas>();
            try
            {
               
                retorno = db.BalcaoVendas.FromSql("SELECT * FROM BalcaoVendas where  id = '" + id.ToString() +  "'").ToList();
            }         
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<BalcaoVendas> GetAll(Guid idOrg)
        {
            List<BalcaoVendas> retorno = new List<BalcaoVendas>();
            retorno = db.BalcaoVendas.FromSql("SELECT * FROM BalcaoVendas  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }       

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<BalcaoVendas> retorno = new List<BalcaoVendas>(); 

            retorno = db.BalcaoVendas.FromSql("SELECT * FROM BalcaoVendas  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }

        public List<BalcaoVendasGridViewModel> GetAllGridViewModel(Guid idOrg, int view)
        {
            List<BalcaoVendas> itens = new List<BalcaoVendas>();

            //Em Andamento
            if (view == 0)
            {
                itens = db.BalcaoVendas.FromSql("SELECT * FROM BalcaoVendas  where statusBalcaoVendas = 0  and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Fechado
            if (view == 1)
            {
                itens = db.BalcaoVendas.FromSql("SELECT * FROM BalcaoVendas  where statusBalcaoVendas in (1)  and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Cancelado
            if (view == 2)
            {
                itens = db.BalcaoVendas.FromSql("SELECT * FROM BalcaoVendas  where statusBalcaoVendas in (2) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }    


            return HelperAssociate.ConvertToGridBalcaoVendas(itens);
        }
        //public List<BalcaoVendasGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        //{
        //    List<BalcaoVendas> itens = new List<BalcaoVendas>();

        //    itens = db.BalcaoVendas.FromSql("SELECT * FROM Pedido  where  idCliente = '" + idCliente.ToString() + "'").ToList();

        //    return HelperAssociate.ConvertToGridBalcaoVendas(itens);
        //}


        //public List<BalcaoVendas> GetAllByIdCliente(Guid idCliente)
        //{
        //    List<BalcaoVendas> retorno = new List<BalcaoVendas>();
        //    retorno = db.Pedidos.FromSql("SELECT * FROM Pedido where  idCliente = '" + idCliente.ToString() + "'").ToList();
        //    return retorno;

        //}

    }
}
