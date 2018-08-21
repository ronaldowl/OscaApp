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
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Cliente> Clientes  { get; set; }  
        public DbSet<CategoriaProfissional> CategoriasProfissionais { get; set; }
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
        public DbSet<PedidoRetirada> PedidosRetirada { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<ServicoPedidoRetirada> ServicosPedidoRetirada { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<LocalProduto> LocalProdutos { get; set; }
        public DbSet<MovimentacaoProduto> MovimentacaoProdutos { get; set; }
        public DbSet<DetalheMovimentacaoProduto> DetalheMovimentacaoProdutos { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<ServicoOrdem> ServicosOrdem { get; set; }
        public DbSet<ProdutoOrdem> ProdutosOrdem { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Incidente> Incidente { get; set; }
        public DbSet<Organizacao> Organizacao { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<ClientePotencial> ClientePotencial { get; set; }
        public DbSet<PerfilAcesso> PerfilAcessos { get; set; }
        public DbSet<BalcaoVendas> BalcaoVendas { get; set; }
        public DbSet<ProdutoBalcao> ProdutosBalcao { get; set; }
        public DbSet<OrgConfig> OrgsConfig { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ProdutoFornecedor> ProdutosFornecedor { get; set; }

    }
}
