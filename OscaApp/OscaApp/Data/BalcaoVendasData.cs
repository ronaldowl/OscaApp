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
               db.BalcaoVendas.Remove(modelo);
                db.SaveChanges();      
         }
        public void Add(BalcaoVendas modelo)
        {          
                db.BalcaoVendas.Add(modelo);                
                db.SaveChanges();         
           
        }
        public void Update(BalcaoVendas modelo)
        {
            
                db.Attach(modelo);              
                  
                db.Entry(modelo).Property("statusBalcaoVendas").IsModified  = true;              
                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;
                db.Entry(modelo).Property("idCliente").IsModified = true;
                       
                db.SaveChanges();          

        }
        public BalcaoVendas Get(Guid id )
        {
            List<BalcaoVendas> retorno = new List<BalcaoVendas>();
           
            retorno = (from bl in db.BalcaoVendas where bl.id.Equals(id) select bl).ToList();

            return retorno[0];
        }

        public void UpdateStatus(BalcaoVendas modelo)
        {            
                db.Attach(modelo);          
                db.Entry(modelo).Property("statusBalcaoVendas").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                db.SaveChanges();
         
        }

        public List<BalcaoVendas> GetAll(Guid idOrg)
        {         
       
            List<BalcaoVendas> retorno = (from bl in db.BalcaoVendas where bl.criadoEm == DateTime.Now.Date & bl.idOrganizacao == idOrg select bl).ToList();
                                              
            return retorno;
        }

        public List<BalcaoVendas> GetAllByIdCliente(Guid idCliente)
        {
            List<BalcaoVendas> retorno = new List<BalcaoVendas>();
            retorno = (from bl in db.BalcaoVendas where bl.idCliente.Equals(idCliente) select bl).ToList();
            return retorno;
        }      
        
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<BalcaoVendas> retorno = new List<BalcaoVendas>();

            retorno = (from bl in db.BalcaoVendas where bl.idOrganizacao.Equals(idOrg) select bl).ToList();

            return Relacao.ConvertToRelacao(retorno);

        }

        public List<BalcaoVendasGridViewModel> GetByCodigo(Guid idOrg, string codigo)
        {
            List<BalcaoVendas> itens = (from bl in db.BalcaoVendas where (bl.codigo == codigo) & (bl.idOrganizacao.Equals(idOrg))  select bl).ToList();
             
            return HelperAssociate.ConvertToGridBalcaoVendas(itens);
        }

        public List<BalcaoVendasGridViewModel> GetAllGridViewModelDay(Guid idOrg, string Date)
        {
            List<BalcaoVendas> itens = new List<BalcaoVendas>();

            itens = (from bl in db.BalcaoVendas where bl.idOrganizacao.Equals(idOrg) select bl).ToList();

            return HelperAssociate.ConvertToGridBalcaoVendas(itens);
        }
        public List<BalcaoVendasGridViewModel> GetAll(Guid idOrg, int view, DateTime inicio, DateTime fim)
        {
            List<BalcaoVendas> itens = new List<BalcaoVendas>();


            //Contas do dia
            if (view == 0)
            {
                itens = (from bl in db.BalcaoVendas where (bl.criadoEm.Date == DateTime.Now.Date) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();
            }

            //Contas do ultimos 7
            if (view == 1)
            {
                itens = (from bl in db.BalcaoVendas where (bl.criadoEm.Date >= DateTime.Now.Date.AddDays( -3)) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();
                
            }

            //Todos Contas do Mes
            if (view == 2)
            {

                itens = (from bl in db.BalcaoVendas where (bl.criadoEm.Date.Month == DateTime.Now.Date.Month) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();
            }

            //Todos Contas do Periodo
            if (view == 3)
            {
                itens = (from bl in db.BalcaoVendas where  (bl.idOrganizacao.Equals(idOrg)) & ((bl.criadoEm.Date >= inicio.Date & bl.criadoEm.Date <= fim.Date)) select bl).ToList();

            }

            //Todos Contas da Base
            if (view == 4)
            {
                itens = (from bl in db.BalcaoVendas where (bl.idOrganizacao.Equals(idOrg))  select bl).ToList();

            }

            return HelperAssociate.ConvertToGridBalcaoVendas(itens);
        }

    }
}
