using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
     public class CustomEnumStatus
    {
        public enum Status
        {
            Ativo = 1,
            Inativo = 0
        }

        public enum StatusOrg
        {
            Inativo = 0,
            Ativo = 1,            
            EmAvaliacao = 2,
            Expirada = 3
        }

        public enum StatusPedido
        {                      
            EmAndamento = 1,
            Fechado = 2,
            AguardandoProduto = 3,
            ParaEntrega = 4
        }

        public enum StatusOrdemServico
        {
            EmAndamento = 1,
            Fechado = 2,
            AguardandoProduto = 3,
            Cancelada = 4
        }

    }

    public class CustomEnum
    {
        public enum Sexo
        {
            masculino = 1,
            feminino = 2,
            outros = 3
        }     

        public enum TipoParametro
        {
            texto = 1,
            inteiro = 2,
            data = 3,
            logico = 4
        }
        public enum TipoPessoa
        {
            fisica = 1,
            juridica = 2
        }
        public enum TipoEndereco
        {
            Entrega = 1,
            Cobrança = 2,
            Faturamento = 3
        }

        public enum Plano
        {
            Nuvem = 1,
            NuvemCustom = 2,
            Local = 3
        }

        public enum FormaVendaProduto
        {
            Unidade = 1,
            Peso = 2,
            Hora = 3,
            Taxa = 4,
            Aluguel = 5
        }

        public enum tipoDesconto
        {
            Money = 1,
            Percentual = 2          
        }

        public enum codicaoPagamento
        {
            Avista = 1,
            Parcelado = 2
                     
        }

        public enum tipoPagamento
        {
            Dinheiro = 1,
            Cartao = 2,
            Cheque = 3,
            Deposito =4          
        }
        public enum metodoEntrega
        {
            ClienteRetira = 1,
            Sedex = 2,
            Transportadora = 3,
            Deposito =4,
            FedEx = 5,
            Correio = 6          
        }

        public enum tipoConta
        {
            ContaCorrente = 1,
            Poupanca = 2,
            Salario = 3,
            Outros = 4
        }

        public enum tipoItemCaledario
        {
            Atendimento = 1,
            OrdemServico = 2
           
        }

        public enum tipoOrdemServico
        {
            Orcamento = 1,
            OrdemServico = 2

        }

        public enum hora
        {
            UmaHora = 1,
            DuasHoras = 2,
            TresHoras = 3

        }
    }

    public class CustomEntityEnum
    {
        public enum Entidade
        {
            Cliente = 1,
            Contato = 2,
            Atendimento = 3,
            Pedido = 4,
            OrdemServico = 5,
            Servico = 6,
            Produto = 7,
            Parametro = 8,
            Endereco = 9,
            Usuario = 10,
            Incidente = 11,
            ListaPreco = 12,
            ItemListaPreco = 13,
            Fornecedor = 14,
            Log = 15,          
            ProdutoPedido = 16,
            Profissional = 17,
            ProdutoOrdem = 18,
            Comunicado = 19,
            ContasPagar = 20,
            ContasReceber = 21,
            Recurso = 22,
            Banco = 23,
            Organizacao = 1000

        }
        public enum Estado
        {
            Acre = 1,
            Alagoas = 2,
            Amapa = 3,
            Amazonas = 4,
            Bahia = 5,
            Ceara = 6,
            DistritoFederal = 7,
            EspiritoSanto = 8,
            Goiás = 9,
            Maranhao = 10,
            MatoGrosso = 11,
            MatoGrossoDoSul = 12,
            MinasGerais = 13,
            Para = 14,
            Paraiba = 15,
            Parana = 16,
            Pernambuco = 17,
            Piaui = 18,
            RioDeJaneiro = 19,
            RioGrandeDoNorte = 20,
            RioGrandeDoSul = 21,
            Rondônia = 22,
            Roraima = 23,
            SantaCatarina = 24,
            SãoPaulo = 25,
            Sergipe = 26,
            Tocantins = 27
        }
    }
}