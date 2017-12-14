﻿using Microsoft.EntityFrameworkCore;
using OscaApp.Models;

namespace OscaApp.Data
{
    public class ContexDataService : DbContext
    {   

        public ContexDataService(DbContextOptions<ContexDataService> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes  { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
     

    }
}
