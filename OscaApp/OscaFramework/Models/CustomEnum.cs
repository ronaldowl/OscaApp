using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OscaFramework.Models
{
     public class CustomEnumStatus
    {
        public enum StatusLead
        {
            Ativo = 0,
            Inativo = 1,
            Qualificado = 2
        }
        public enum StatusAtividade
        {
            Aberta = 0,
            Concluida = 1,
            Cancelada = 2
        }
        public enum Status
        {
            Inativo = 0,
            Ativo = 1
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
            ParaEntrega = 4,
            Cancelado = 5
        }

        public enum StatusOrdemServico
        {
            EmAndamento = 1,
            Fechado = 2,
            AguardandoProduto = 3,
            ParaEntrega = 4,
            Cancelado = 5
        }
        public enum StatusAtendimento
        {
            agendado = 0,
            atendido = 1,
            cancelado = 2            
        }
        public enum StatusAgendamento
        {
            agendado = 0,
            concluido = 1,
            cancelado = 2
        }
        public enum StatusContaReceber
        {
            agendado = 0,
            recebido = 1,
            cancelado = 2,
            atrasado = 3
        }
        public enum StatusContaPagar
        {
            agendado = 0,
            pago = 1,
            cancelado = 2,
            atrasado = 3
        }

    }

    public class CustomEnum
    {
        
        public enum TipoProduto
        {
            produto = 0,
            servico = 1
        }


        public enum TipoAtividade
        {
            Tarefa = 1,
            Telefonema = 2,
            Servico = 3,
            Cobranca = 4                 
        }
        public enum TipoReferencia
        {
            Atendimento = 0,
            Servico = 1,
            OrdemServico = 2,
            Pedido = 3
        }
        public enum OrigemFaturamento
        {
            Atendimento = 0,
            Servico = 1,
            OrdemServico = 2,
            Pedido = 3,
            Balcao = 4
        }
        public enum TipoLancamento
        {
            manual = 0,
            automatico = 1            
        }

        public enum OrigemContaReceber
        {
            Pedido = 0,
            Atendimento = 1,
            OrdemServico = 2,           
            Outros = 100
        }

        public enum OrigemContaPagar
        {
            Fornecedor = 0,
            DespesaEmpresa = 1,
            Outros = 2             
        }

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
            Lote = 0,
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
            Parcelado = 2,     
            Prazo = 3
        }

        public enum tipoPagamento
        {
            Dinheiro = 1,
            Cartao = 2,
            Cheque = 3,
            Deposito = 4          
        }
        public enum metodoEntrega
        {
            ClienteRetira = 1,
            Sedex = 2,
            Transportadora = 3,
            Deposito = 4,
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
            Execucao = 2

        }

        public enum hora
        {
            UmaHora = 1,
            DuasHoras = 2,
            TresHoras = 3

        }  

        public enum itemHora
        {
            [Description("01:00")]
                umHora =  1,   

            [Description("01:30")]
                umHoraMeia =  2,
                                                 
            [Description("02:00")]
                duasHoras = 3,    

            [Description("02:30")]
                duasHorasMeia =  4, 
                                                 
            [Description("03:00")]
                tresHoras =  5,

            [Description("03:30")]
                tresHorasMeia =  6,

            [Description("04:00")]
                quatroHoras =  7,

            [Description("04:30")]
                quatroHorasMeia =  8,

            [Description("05:00")]
                cincoHoras =  9,

            [Description("05:30")]
                cincoHorasMeia =  10,

            [Description("06:00")]
                seisHoras =  11,

            [Description("06:30")]
                seisHorasMeia =  12,

            [Description("07:00")]
                seteHoras =  13,

            [Description("07:30")]
                seteHorasMeia =  14 ,

            [Description("08:00")]
                oitoHoras =  15, 

            [Description("08:30")]
                oitoHorasMeia =  16,  

            [Description("09:00")]                                                
                noveHoras =  17,
                                                 
            [Description("09:30")]
                noveHorasMeia =  18,
                                                 
            [Description("10:00")]                                                 
                dezHoras =  19,
                                                                                                  
            [Description("10:30")]
                dezHorasMeia =  20, 
                                                                                                  
            [Description("11:00")]
                onzeHoras =  21,
                                                                                                  
            [Description("11:30")]
                onzeHorasMeia =  22, 
                                                                                                  
            [Description("12:00")]
                meioDia =  23, 
                                                                                                  
            [Description("12:30")]
                meioDiaMeia =  24, 
                                                                                                  
            [Description("13:00")]
                trezeHoras =  25,  
                                                                                                  
            [Description("13:30")]
                trezeHorasMeia =  26,  
                                                                                                  
            [Description("14:00")]
                quatozeHoras =  27, 
                                                                                                  
            [Description("14:30")]
                quatozeHorasMeia =  28, 
                                                                                                  
            [Description("15:00")]
                quinzeHoras =  29,
                                                                                                  
            [Description("15:30")]
                quinzeHorasMeia =  30, 
                                                                                                  
            [Description("16:00")]
                dezesseisHoras =  31, 
                                                                                                  
            [Description("16:30")]
                dezesseisHorasMeia =  32, 
                                                                                                  
            [Description("17:00")]
                dezesseteHoras =  33, 
                                                                                                  
            [Description("17:30")]
                dezesseteHorasMeia =  34, 
                                                                                                  
            [Description("18:00")]
                dezoitoHoras =  35, 
                                                                                                  
            [Description("18:30")]
                dezoitoHorasMeia =  36, 
                                                                                                  
            [Description("19:00")]
                dezenoveHoras =  37,  
                                                                                                  
            [Description("19:30")]
                dezenoveHorasMeia =  38, 
                                                                                                  
            [Description("20:00")]
                vinteHoras =  39, 
                                                                                                  
            [Description("20:30")]
                vinteHorasMeia =  40,   
                                                                                                 
            [Description("21:00")]
                vinteUmaHora =  41,  
                                                                                                  
            [Description("21:30")]
                vinteUmaHorasMeia =  42,  
                                                                                                  
            [Description("22:00")]
                vinteDuasHoras =  43,  
                                                                                                  
            [Description("22:30")]
                vinteDuasHorasMeia =  44,   
                                                                                                 
            [Description("23:00")]
                vinteTresHoras =  45,  
                                                                                                  
            [Description("23:30")]
                vinteTresHorasMeia =  46,  
                                                                                                  
            [Description("00:00")]
                meiaNoite =  47,
                                                                                                 
            [Description("00:30")]
                meiaNoiteMeia = 48
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
            Agendamento = 24,
            ItemProdutoLista =25,
            ServicoOrdem = 26,
            Atividade = 27,
            ClientePotencial = 28,
            PerfilAcesso = 29,
            Faturamento = 30,
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