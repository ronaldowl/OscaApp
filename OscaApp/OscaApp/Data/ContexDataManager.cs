using Microsoft.EntityFrameworkCore;
using OscaApp.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContexDataManager : DbContext
    {   

        public ContexDataManager(DbContextOptions<ContexDataManager> options) : base(options)
        {

        }
        public DbSet<Organizacao> Organizacao { get; set; }       

    }
}
