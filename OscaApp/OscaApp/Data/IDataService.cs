using System;
using System.Collections.Generic;

using OscaApp.Models;
using OscaApp.framework.Models;

namespace OscaApp.Data
{

    //TODO: Criar uma inteface para cada objeto gravado no banco
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
        List<Produto> GetAll(Guid idOrg);

    }
    public interface IEnderecoData
    {
        void Add(Endereco endereco);
        void Update(Endereco endereco);
        Endereco Get(Guid id);
        List<Endereco> GetAll(Guid idOrg);

    }

    public interface ISqlGenericService
    {

        String RetornaNovaPosicao(int Entidade, Guid idOrganizacao);    

    }
    public interface ISqlGenericManager
    {
        bool ExisteOrganizacao(string org, out Guid id);

        Guid CriaOrganizacao(string org);

        Relacao RetornaContextPage(string email,string org);
    

    }


    
}
