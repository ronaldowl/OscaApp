using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ListaPrecoData : IListaPrecoData
    {
        private ContexDataService db;       

        public ListaPrecoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(ListaPreco listapreco)
        {                  
                db.ListaPrecos.Add(listapreco);                
                db.SaveChanges();            
        }
        public void Update(ListaPreco modelo)
        {           
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified                = true;
                db.Entry(modelo).Property("descricao").IsModified           = true;
                db.Entry(modelo).Property("dataValidade").IsModified        = true;
                db.Entry(modelo).Property("fimValidade").IsModified = true;
                
                db.Entry(modelo).Property("padrao").IsModified              = true;
                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;
                        
                db.SaveChanges();   

        }
        public ListaPreco Get(Guid id )
        {
            List<ListaPreco> retorno = new List<ListaPreco>();
           
            retorno = retorno = (from A in db.ListaPrecos where A.id.Equals(id) select A).ToList();
        
            return retorno[0];
        }
        public List<ListaPreco> GetAll(Guid idOrg)
        {
            List<ListaPreco> retorno = new List<ListaPreco>();
            retorno = (from A in db.ListaPrecos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ListaPreco> retorno = new List<ListaPreco>();          
                         
            retorno = (from A in db.ListaPrecos where A.idOrganizacao.Equals(idOrg) select A ).ToList();
            retorno = retorno.OrderByDescending(A => A.padrao).ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
    }
}
