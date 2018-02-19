using System;
using System.Collections.Generic;
using OscaApp.Models;
using OscaApp.framework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaFramework.Models;

namespace OscaApp.Data
{

    //TODO: Criar uma inteface para cada objeto gravado no banco

    public interface IBancoData
    {
        Banco Get(Guid id, Guid idOrg);
        List<Banco> GetAll();
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
        OrdemServico Get(Guid id );
        List<OrdemServico> GetAll(Guid id);
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
        Cliente Get(Guid id, Guid idOrg);
        List<Cliente> GetAll(Guid idOrg);       
    }
    public interface IListaPrecoData
    {
        void Add(ListaPreco contato);
        void Update(ListaPreco contato);
        ListaPreco Get(Guid id, Guid idOrg);
        List<ListaPreco> GetAll(Guid idOrg);       
        List<Relacao> GetAllRelacao(Guid idOrg);
    }

    public interface IAtendimentoData
    {
        void Add(Atendimento atendimento);
        void Update(Atendimento atendimento);
        Atendimento Get(Guid id);
        List<Atendimento> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
    }

    public interface IContasPagarData
    {
        void Add(ContasPagar contasPagar);
        void Update(ContasPagar contasPagar);
        ContasPagar Get(Guid id, Guid idOrg);
        List<ContasPagar> GetAll(Guid idOrg);
    }

    public interface IRecursoData
    {
        void Add(Recurso recurso);
        void Update(Recurso recurso);
        Recurso Get(Guid id, Guid idOrg);
        List<Recurso> GetAll(Guid idOrg);
    }

    public interface IContasReceberData
    {
        void Add(ContasReceber contasReceber);
        void Update(ContasReceber contasReceber);
        ContasReceber Get(Guid id, Guid idOrg);
        List<ContasReceber> GetAll(Guid idOrg);
    }

    public interface IPedidoData
    {
        void Add(Pedido contato);
        void Update(Pedido contato);
        Pedido Get(Guid id);
        List<Pedido> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<PedidoGridViewModel> GetAllGridViewModel(Guid idOrg);

    }

    public interface IItemListaPrecoData
    {
        void Add(ItemListaPreco contato);
        void Update(ItemListaPreco contato);
        ItemListaPreco Get(Guid id);
        List<ItemListaPreco> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<LookupItemLista> GetAllByListaPreco(Guid idLista);

    }

    public interface IProdutoPedidoData
    {
        void Add(ProdutoPedido contato);
        void Update(ProdutoPedido contato);
        void Delete(ProdutoPedido contato);
        ProdutoPedido Get(Guid id);
        List<ProdutoPedido> GetByPedidoId(Guid idPedido);
        List<ProdutoPedido> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);
        List<ProdutoPedidoGridViewModel> GetAllGridViewModel(Guid idPedido);
    }

    public interface IServicoOrdemData
    {
        void Add(ServicoOrdem contato);
        void Update(ServicoOrdem contato);
        void Delete(ServicoOrdem contato);
        ServicoOrdem Get(Guid id);
        List<ServicoOrdem> GetByServicoOrdemId(Guid idPedido);
        List<ServicoOrdem> GetAll(Guid idOrg);
        List<Relacao> GetAllRelacao(Guid idOrg);

    }


    public interface IContatoData
    {
        void Add(Contato contato);
        void Update(Contato contato);
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
        Endereco Get(Guid id);
        List<Endereco> GetAll(Guid idOrg);
        List<Endereco> GetAllByIdClinte(Guid idCliente);
    }
    public interface IOrganizacaoData
    {
       
        void Update(Organizacao organizacao);
        Organizacao Get(Guid idOrg);
        
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

}
