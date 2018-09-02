using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ServicoData : IServicoData
    {
        private ContexDataService db;

          public ServicoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(Servico servico)
        {
            
                db.Servicos.Add(servico);
                db.SaveChanges();
       
        }
        public void Update(Servico modelo)
        {
             
                db.Attach(modelo);
               
                db.Entry(modelo).Property("formaVendaServico").IsModified = true;
                db.Entry(modelo).Property("valor").IsModified = true;
                db.Entry(modelo).Property("descricao").IsModified = true;
                db.Entry(modelo).Property("nomeServico").IsModified              = true;
                db.Entry(modelo).Property("tipoServico").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();         

        }
        public Servico Get(Guid id )
        {
            List<Servico> retorno = new List<Servico>();          
               
            retorno = (from A in db.Servicos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Servico> GetAll(Guid idOrg)
        {
            List<Servico> retorno = new List<Servico>();
            retorno = (from A in db.Servicos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
    }
}
