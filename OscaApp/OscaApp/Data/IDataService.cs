using System;
using System.Collections.Generic;
using OscaApp.Models;
using OscaApp.framework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.Models;

namespace OscaApp.Data
{

    //TODO: Criar uma inteface para cada objeto gravado no banco

    public interface IAtividadeData
    {
        void Add(Atividade atividade);
        void Update(Atividade atividade);
        void UpdateStatus(Atividade atividade);
        void Delete(Atividade atividade);
        Atividade Get(Guid id);
        List<Atividade> GetAll(Guid idOrg);
        List<AtividadeGridViewModel> GetAllGridViewModel(Guid idOrg, int view, string idProfissional);
        List<AtividadeGridViewModel> GetAllGridDia(string idProfissional);

    }

    public interface IBancoData
    {
        Banco Get(Guid id, Guid idOrg);
        List<Banco> GetAll();
    }

    public interface IIncidenteData
    {
        void Add(Incidente incidente);
        void Update(Incidente incidente);
        Incidente Get(Guid id, Guid idOrganizacao);
        List<Incidente> GetAll(Guid idOrg);
    }

    public interface ICategoriaProfissionalData
    {
        CategoriaProfissional Get(Guid id, Guid idOrg);
        List<CategoriaProfissional> GetAll();
    }

    public interface IFornecedorData
    {
        void Add(Fornecedor fornecedor);
        void Update(Fornecedor fornecedor);
        Fornecedor Get(Guid id, Guid idOrg);
        List<Fornecedor> GetAll(Guid idOrg);
    }
    public interface IComunicadoData
    {
        void Add(Comunicado comunicado);
        void Update(Comunicado comunicado);
        void Delete(Comunicado comunicado);
        Comunicado Get(Guid id, Guid idOrg);
        List<Comunicado> GetAll(Guid idOrg);
    }
    public interface IProfissionalData
    {
        void Add(Profissional profissional);
        void Update(Profissional profissional);
        Profissional Get(Guid id, Guid idOrg);
        List<Profissional> GetAll(Guid idOrg);
    }
    public interface IOrdemServicoData
    {
        void Add(OrdemServico ordemServico);
        void Update(OrdemServico ordemServico);
        void UpdateStatus(OrdemServico ordemServico);
        void Delete(OrdemServico ordemServico);

        OrdemServico Get(Guid id );
        List<OrdemServico> GetAll(Guid id);
        List<OrdemServicoGridViewModel> GetAllGridViewModel(Guid idOrg, int view);
        List<OrdemServicoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente);

    }
    public interface IServicoData
    {
        void Add(Servico servico);
        void Update(Servico servico);
        Servico Get(Guid id, Guid idOrg);
        List<Servico> GetAll(Guid idOrg);
    }
    public interface IClienteData
    {
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void SetStatus(Cliente modelo);
        Cliente Get(Guid id, Guid idOrg);
        List<Cliente> GetAll(Guid idOrg, int view);       
    }
    public interface IListaPrecoData
    {
        void Add(ListaPreco listaPreco);
        void Update(ListaPreco listaPreco);
        ListaPreco Get(Guid id, Guid idOrg);
        List<ListaPreco> GetAll(Guid idOrg);       
        List<Relacao> GetAllRelacao(Guid idOrg);
    }

    public interface IAtendimentoData
    {
        void Add(Atendimento atendimento);
        void Update(Atendimento atendimento);
        void Delete(Atendimento atendimento);
        Atendimento Get(Guid id);
        List<Atendimento> GetAll(Guid idOrg);
        List<Atendimento> GetAllByIdCliente(Guid idCliente);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<AtendimentoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente); 
        List<AtendimentoGridViewModel> GetAllGridViewModelDia(Guid idProfissional);

    }

    public interface IAgendamentoData
    {
        void Add(Agendamento agendamento);
        void Update(Agendamento agendamento);
        void Delete(Agendamento agendamento);
        Agendamento Get(Guid id);
        List<Agendamento> GetAll(Guid idOrg);
        List<Agendamento> GetAllByIdCliente(Guid idCliente);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<AgendamentoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente);
        List<AgendamentoGridViewModel> GetAllGridViewModelDia(Guid idProfissional);

    }

    public interface IContasPagarData
    {
        void Add(ContasPagar contasPagar);
        void Update(ContasPagar contasPagar);
        void Delete(ContasPagar contasPagar);
        void UpdateStatus(ContasPagar contasPagar);
        ContasPagar Get(Guid id );
        List<ContasPagar> GetAll(Guid idOrg, int view);
    }

    public interface IRecursoData
    {
        void Add(Recurso recurso);
        void Update(Recurso recurso);
        void Delete(Recurso recurso);
        Recurso Get(Guid id, Guid idOrg);
        List<Recurso> GetAll(Guid idOrg);
    }

    public interface IPedidoRetiradaData
    {
        void Add(PedidoRetirada pedidoRetirada);
        void Update(PedidoRetirada pedidoRetirada);
        void Delete(PedidoRetirada pedidoRetirada);
        PedidoRetirada Get(Guid id, Guid idOrg);
        List<PedidoRetirada> GetAll(Guid idOrg);
    }

    public interface IContasReceberData
    {
        void Add(ContasReceber contasReceber);
        void Update(ContasReceber contasReceber);
        void UpdateStatus(ContasReceber contasReceber);
        void Delete(ContasReceber contasReceber);

        ContasReceber Get(Guid id );
        List<ContasReceber> GetAll(Guid idOrg, int view);
        List<ContasReceber> GetAllByIdCliente(Guid idCliente);
        
    }

    public interface IPedidoData
    {
        void Add(Pedido pedido);
        void Update(Pedido pedido);
        void Delete(Pedido pedido);
        Pedido Get(Guid id);
        List<Pedido> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<PedidoGridViewModel> GetAllGridViewModel(Guid idOrg, int view);
        List<PedidoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente);        
    }
    public interface IBalcaoVendasData
    {
        void Add(BalcaoVendas balcaoVendas);
        void Update(BalcaoVendas balcaoVendas);
        void UpdateStatus(BalcaoVendas balcaoVendas);

        void Delete(BalcaoVendas balcaoVendas);
        BalcaoVendas Get(Guid id);
        List<BalcaoVendas> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<BalcaoVendasGridViewModel> GetAllGridViewModel(Guid idOrg);
        //List<BalcaoVendasGridViewModel> GetAllGridViewModelByCliente(Guid idCliente);


    }

    public interface IItemListaPrecoData
    {
        void Add(ItemListaPreco itemListaPreco);
        void Update(ItemListaPreco itemListaPreco);
        ItemListaPreco Get(Guid id);
        List<ItemListaPreco> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<LookupItemLista> GetAllByListaPreco(Guid idLista);
        List<LookupItemLista> GetAllByIdProduto(Guid idProduto);


    }

    public interface IProdutoPedidoData
    {
        void Add(ProdutoPedido produtoPedido);
        void Update(ProdutoPedido produtoPedido);
        void Delete(ProdutoPedido produtoPedido);
        ProdutoPedido Get(Guid id);
        List<ProdutoPedido> GetByPedidoId(Guid idPedido);
        List<ProdutoPedido> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<ProdutoPedidoGridViewModel> GetAllGridViewModel(Guid idPedido);
    }

    public interface IServicoOrdemData
    {
        void Add(ServicoOrdem servicoOrdem);
        void Update(ServicoOrdem servicoOrdem);
        void Delete(ServicoOrdem servicoOrdem);
        ServicoOrdem Get(Guid id);
        List<ServicoOrdem> GetByServicoOrdemId(Guid idPedido);
        List<ServicoOrdem> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<ServicoOrdemGridViewModel> GetAllGridViewModel(Guid idServico);

    }

    public interface IProdutoOrdemData
    {
        void Add(ProdutoOrdem produtoOrdem);
        void Update(ProdutoOrdem produtoOrdem);
        void Delete(ProdutoOrdem produtoOrdem);
        ProdutoOrdem Get(Guid id);
        List<ProdutoOrdem> GetByProdutoOrdemId(Guid idOrdem);
        List<ProdutoOrdem> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<ProdutoOrdemGridViewModel> GetAllGridViewModel(Guid idProduto);

    }

    public interface IContatoData
    {
        void Add(Contato contato);
        void Update(Contato contato);
        void Delete(Contato contato);
        Contato Get(Guid id, Guid idOrg);
        List<Contato> GetAll(Guid idOrg);
    }

    public interface IProdutoData
    {
        void Add(Produto produto);
        void Update(Produto produto);
        Produto Get(Guid id, Guid idOrg);
        Relacao GetRelacao(Guid id);
        List<Produto> GetAll(Guid idOrg);
    }
    public interface IEnderecoData
    {
        void Add(Endereco endereco);
        void Update(Endereco endereco);
        void Delete(Endereco endereco);
        Endereco Get(Guid id);
        List<Endereco> GetAll(Guid idOrg);
        List<Endereco> GetAllByIdClinte(Guid idCliente);
    }
    public interface IOrganizacaoData
    {
       
        void Update(Organizacao organizacao);
        Organizacao Get(Guid idOrg);
        
    }

    public interface IClientePotencialData
    {
        void Add(ClientePotencial cliente);
        void Update(ClientePotencial cliente);
        void SetStatus(ClientePotencial modelo);
        ClientePotencial Get(Guid id);
        List<ClientePotencial> GetAll(Guid idOrg, int view);
    }

    public interface ISqlGenericService
    {
        String RetornaNovaPosicao(int Entidade, Guid idOrganizacao);    
    }

    public interface ISqlGenericManager
    {
        bool ExisteOrganizacao(string org, out Guid id);

        Guid CriaOrganizacao(string org, string email);

        
    }

    public interface ISqlGenericData
    {
        
    }

    public interface IPerfilAcesso
    {
        void Add(PerfilAcesso perfilAcesso);
        void Update(PerfilAcesso perfilAcesso);
        PerfilAcesso Get(Guid id );
        List<PerfilAcesso> GetAll(Guid idOrg);
    }

    public interface IProdutoBalcaoData
    {
        void Add(ProdutoBalcao produtoBalcao);
        void Update(ProdutoBalcao produtoBalcao);
        ProdutoBalcao Get(Guid id);
        List<ProdutoBalcao> GetAll(Guid idOrg);
    }

}
