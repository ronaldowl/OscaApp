﻿using System;
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
        Banco Get(Guid id);
        List<Banco> GetAll();
    }

    public interface IIncidenteData
    {
        void Add(Incidente incidente);
        void Update(Incidente incidente);
        Incidente Get(Guid id);
        List<Incidente> GetAll(Guid idOrg);
    }

    public interface ICategoriaProfissionalData
    {
        CategoriaProfissional Get(Guid id);
        List<CategoriaProfissional> GetAll();
    }

    public interface IFornecedorData
    {
        void Add(Fornecedor fornecedor);
        void Update(Fornecedor fornecedor);       
        Fornecedor Get(Guid id);
        List<Fornecedor> GetAll(Guid idOrg);
    }
    public interface IComunicadoData
    {
        void Add(Comunicado comunicado);
        void Update(Comunicado comunicado);
        void Delete(Comunicado comunicado);
        Comunicado Get(Guid id);
        List<Comunicado> GetAll(Guid idOrg);
    }

    public interface IPagamentoData
    {
        void Add(Pagamento pagamento);
        void Update(Pagamento pagamento);
        void Delete(Pagamento pagamento);
        Pagamento Get(Guid id);
        List<Pagamento> GetAllByContasReceber(Guid id);
    }
    public interface IProfissionalData
    {
        void Add(Profissional profissional);
        void Update(Profissional profissional);
        Profissional Get(Guid id);
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
        Servico Get(Guid id);
        List<Servico> GetAll(Guid idOrg);
    }
    public interface IClienteData
    {
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void SetStatus(Cliente modelo);
        Cliente Get(Guid id);
        List<Cliente> GetAll(Guid idOrg, int view);       
    }
    public interface IListaPrecoData
    {
        void Add(ListaPreco listaPreco);
        void Update(ListaPreco listaPreco);
        ListaPreco Get(Guid id);
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
        Recurso Get(Guid id);
        List<Recurso> GetAll(Guid idOrg);
    }

    public interface ILocalProdutoData
    {
        void Add(LocalProduto localProduto);
        void Update(LocalProduto localProduto);
        LocalProduto Get(Guid id);
        List<LocalProduto> GetAll(Guid idOrg);
    }
    public interface IMovimentacaoProdutoData
    {
        void Add(MovimentacaoProduto movimentacaoProduto);
        void Update(MovimentacaoProduto movimentacaoProduto);
        MovimentacaoProduto Get(Guid id);
        List<MovimentacaoProduto> GetAll(Guid idOrg);
    }
    public interface IDetalheMovimentacaoProdutoData
    {
        void Add(DetalheMovimentacaoProduto detalheMovimentacaoProduto);
        void Update(DetalheMovimentacaoProduto detalheMovimentacaoProduto);
        DetalheMovimentacaoProduto Get(Guid id);
        List<DetalheMovimentacaoProduto> GetAll(Guid idOrg);
    }

    public interface IPedidoRetiradaData
    {
        void Add(PedidoRetirada pedidoRetirada);
        void Update(PedidoRetirada pedidoRetirada);
        void UpdateStatus(PedidoRetirada pedidoRetirada);
        void Delete(PedidoRetirada pedidoRetirada);
        PedidoRetirada Get(Guid id);
        List<PedidoRetiradaGridViewModel> GetAll(Guid idOrg);
        List<PedidoRetirada> GetAllByIdCliente(Guid idCliente);

    }
    public interface IOrgConfigData
    {
        void Update(OrgConfig orgConfig);
        OrgConfig Get(Guid id);
        void Add(OrgConfig orgConfig);
    }

    public interface IServicoPedidoRetiradaData
    {
        void Add(ServicoPedidoRetirada servicoPedidoRetirada);
        void Update(ServicoPedidoRetirada servicoPedidoRetirada);
        ServicoPedidoRetirada Get(Guid id);
        List<ServicoPedidoRetirada> GetAll(Guid idOrg);
    }

    public interface IContasReceberData
    {
        void Add(ContasReceber contasReceber);
        void Update(ContasReceber contasReceber);
        void UpdateStatus(ContasReceber contasReceber);
        void Delete(ContasReceber contasReceber);

        ContasReceber Get(Guid id );
        List<ContasReceberGridViewModel> GetAll(Guid idOrg, int view, ClienteData clienteData);
        List<ContasReceber> GetAllDia(Guid idOrg);
        List<ContasReceber> GetAllByIdCliente(Guid idCliente, int view);
        
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
        List<BalcaoVendasGridViewModel> GetAll(Guid idOrg, int view, DateTime inicio, DateTime fim);
        List<BalcaoVendas> GetAllByIdCliente(Guid idCliente);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<BalcaoVendasGridViewModel> GetByCodigo(Guid idOrg, string codigo);

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

    public interface IProdutoFornecedorData
    {
        void Add(ProdutoFornecedor produtoFornecedor);
        void Update(ProdutoFornecedor produtoFornecedor);
        void Delete(ProdutoFornecedor produtoFornecedor);

        ProdutoFornecedor Get(Guid id);
        List<ProdutoFornecedor> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<ProdutoFornecedorGridViewModel> GetAllByProduto(Guid idProduto);  
        List<ProdutoFornecedorGridViewModel> GetAllByFornecedor(Guid idFornecedor);

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
        Contato Get(Guid id);
        List<Contato> GetAll(Guid idOrg);
    }

    public interface IProdutoData
    {
        void Add(Produto produto);
        void Update(Produto produto);
        void UpdateQuantity(Produto produto);
        Produto Get(Guid id);      

        Relacao GetRelacao(Guid id);
        List<Produto> GetAll(Guid idOrg, int view);
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
