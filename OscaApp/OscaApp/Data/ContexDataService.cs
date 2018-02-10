using Microsoft.EntityFrameworkCore;
using OscaApp.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContexDataService : DbContext
    {   

        public ContexDataService(DbContextOptions<ContexDataService> options) : base(options)
        {

        }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Cliente> Fornecedor  { get; set; }       
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Comunicado> Comunicados { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<ContasPagar> ContasP { get; set; }
        public DbSet<ContasReceber> ContasR { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ListaPreco> ListaPrecos { get; set; }
        public DbSet<ItemListaPreco> ItemListaPrecos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedido { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
       
    }
}
