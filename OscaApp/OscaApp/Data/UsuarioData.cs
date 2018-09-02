using Microsoft.EntityFrameworkCore;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.Data
{
    public class UsuarioData
    {
        private ApplicationDbContext db;
        //public DbSet<Cliente> Books { get; set; }

        public UsuarioData(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public List<ApplicationUser> GetAll(Guid idOrg)
        {
            List<ApplicationUser> retorno = new List<ApplicationUser>();        
            retorno = (from A in db.Users where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }

        public ApplicationUser Get(string id)
        {
            List<ApplicationUser> retorno = new List<ApplicationUser>();
            retorno = (from A in db.Users where A.Id.Equals(id) select A).ToList();
            return retorno[0];

        }
    }
}
