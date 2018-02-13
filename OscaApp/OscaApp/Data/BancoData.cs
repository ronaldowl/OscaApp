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

        public Banco Get(Guid id, Guid idOrg)
        {
            List<Banco> retorno = new List<Banco>();
            try
            {
                retorno = db.Bancos.FromSql("SELECT * FROM Banco where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<Banco> GetAll()
        {
            List<Banco> retorno = new List<Banco>();
            retorno = db.Bancos.FromSql("SELECT * FROM Banco").ToList();
            return retorno;
        }
    }
}
