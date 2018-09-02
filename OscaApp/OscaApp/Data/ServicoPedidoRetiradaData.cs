using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ServicoPedidoRetiradaData : IServicoPedidoRetiradaData
    {
        private ContexDataService db;

          public ServicoPedidoRetiradaData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(ServicoPedidoRetirada servicoPedidoRetirada)
        {
             
                db.ServicosPedidoRetirada.Add(servicoPedidoRetirada);
                db.SaveChanges();
      
        }
        public void Update(ServicoPedidoRetirada modelo)
        {
            
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified                    = true;
                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();
     

        }
        public ServicoPedidoRetirada Get(Guid id)
        {
            List<ServicoPedidoRetirada> retorno = new List<ServicoPedidoRetirada>();

            retorno = (from A in db.ServicosPedidoRetirada where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<ServicoPedidoRetirada> GetAll(Guid idOrg)
        {
            List<ServicoPedidoRetirada> retorno = new List<ServicoPedidoRetirada>();
            retorno = (from A in db.ServicosPedidoRetirada where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
    }
}
