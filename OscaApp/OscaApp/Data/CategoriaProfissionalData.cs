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

        public CategoriaProfissional Get(Guid id)
        {
            List<CategoriaProfissional> retorno = new List<CategoriaProfissional>();

            retorno = (from A in db.CategoriasProfissionais where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<CategoriaProfissional> GetAll()
        {
            List<CategoriaProfissional> retorno = new List<CategoriaProfissional>();
            retorno = (from A in db.CategoriasProfissionais  select A).ToList();
            return retorno;
        }
    }
}
