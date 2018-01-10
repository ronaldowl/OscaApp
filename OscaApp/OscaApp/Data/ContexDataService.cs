using Microsoft.EntityFrameworkCore;
using OscaApp.Models;

namespace OscaApp.Data
{
    public class ContexDataService : DbContext
    {   

        public ContexDataService(DbContextOptions<ContexDataService> options) : base(options)
        {

        }
        public DbSet<Cliente> Fornecedor  { get; set; }
  
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ListaPreco> ListaPrecos { get; set; }
        public DbSet<ItemListaPreco> ItemListaPrecos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedido { get; set; }

    }
}
