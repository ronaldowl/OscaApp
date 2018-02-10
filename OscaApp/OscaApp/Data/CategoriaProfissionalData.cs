using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class CategoriaProfissionalData : ICategoriaProfissionalData
    {
        private ContexDataService db;

          public CategoriaProfissionalData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public CategoriaProfissional Get(Guid id, Guid idOrg)
        {
            List<CategoriaProfissional> retorno = new List<CategoriaProfissional>();
            try
            {
                retorno = db.CategoriasProfissionais.FromSql("SELECT * FROM CategoriaProfissional where  id = '" + id.ToString() + "' and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<CategoriaProfissional> GetAll()
        {
            List<CategoriaProfissional> retorno = new List<CategoriaProfissional>();
            retorno = db.CategoriasProfissionais.FromSql("SELECT * FROM CategoriaProfissional").ToList();
            return retorno;
        }
    }
}
