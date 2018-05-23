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
            retorno = db.Users.FromSql("SELECT * FROM AspNetUsers where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }

        public ApplicationUser Get(string id)
        {
            List<ApplicationUser> retorno = new List<ApplicationUser>();
            retorno = db.Users.FromSql("SELECT * FROM AspNetUsers where  id = '" + id + "'").ToList();
            return retorno[0];

        }
    }
}
