using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.framework.Models
{
    public class CustomEnum
    {
        public enum Sexo
        {
            masculino = 1,
            feminino = 2,
            outros = 3
        }

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

        public enum TipoParametro
        {
            texto = 1,
            inteiro = 2,
            data = 3,
            logico = 4,
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
    }

    public class CustomEntityEnum
    {
        public enum Entidade
        {
            Cliente = 1,
            Contato = 2,
            Agendamento = 3,
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