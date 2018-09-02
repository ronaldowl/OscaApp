using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class BancoData : IBancoData
    {
        private ContexDataService db;

          public BancoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public Banco Get(Guid id)
        {
            List<Banco> retorno = new List<Banco>();
            retorno = (from A in db.Bancos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Banco> GetAll()
        {
            List<Banco> retorno = new List<Banco>();
        
            retorno = (from A in db.Bancos select A).ToList();
            return retorno;
        }
    }
}
