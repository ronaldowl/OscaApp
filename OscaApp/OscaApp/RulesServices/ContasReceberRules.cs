using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using System.Collections.Generic;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class ContasReceberRules
    {
        public static bool ContasReceberCreate(ContasReceberViewModel entrada, out ContasReceber contasReceber, ContextPage contexto)
        {
            contasReceber = new ContasReceber();
            contasReceber = entrada.contasReceber;

            if (entrada.cliente != null) contasReceber.idCliente = entrada.cliente.id;

            contasReceber.codigo = AutoNumber.GeraCodigo(21, contexto.idOrganizacao);

            if (contasReceber.codigo != null)
            {

                contasReceber.valorRestante = contasReceber.valor;
                //************ Objetos de controle de acesso ******************
                contasReceber.criadoEm = DateTime.Now;
                contasReceber.criadoPor = contexto.idUsuario;
                contasReceber.criadoPorName = contexto.nomeUsuario;
                contasReceber.modificadoEm = DateTime.Now;
                contasReceber.modificadoPor = contexto.idUsuario;
                contasReceber.modificadoPorName = contexto.nomeUsuario;
                contasReceber.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }
            return false;
        }

        public static bool ContasReceberCreate(ContasReceber entrada, IContasReceberData contaReceberData, ContextPage contexto)
        {

            entrada.codigo = AutoNumber.GeraCodigo(21, contexto.idOrganizacao);

            if (entrada.codigo != null)
            {
                //************ Objetos de controle de acesso ******************
                entrada.criadoEm = DateTime.Now;
                entrada.criadoPor = contexto.idUsuario;
                entrada.criadoPorName = contexto.nomeUsuario;
                entrada.modificadoEm = DateTime.Now;
                entrada.modificadoPor = contexto.idUsuario;
                entrada.modificadoPorName = contexto.nomeUsuario;
                entrada.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                contaReceberData.Add(entrada);

                return true;
            }
            return false;
        }

        public static bool ContasReceberUpdate(ContasReceberViewModel entrada, out ContasReceber contasReceber)
        {
            contasReceber = new ContasReceber();
            contasReceber = entrada.contasReceber;
            if (entrada.cliente != null) contasReceber.idCliente = entrada.cliente.id;

            if (entrada.contasReceber.statusContaReceber == CustomEnumStatus.StatusContaReceber.recebido)
            {
                contasReceber.dataRecebimento = DateTime.Now;
            }

            if (entrada.contasReceber.statusContaReceber == CustomEnumStatus.StatusContaReceber.agendado || entrada.contasReceber.statusContaReceber == CustomEnumStatus.StatusContaReceber.atrasado || entrada.contasReceber.statusContaReceber == CustomEnumStatus.StatusContaReceber.cancelado)
            {
                contasReceber.dataRecebimento = new DateTime();
            }

            //************ Objetos de controle de acesso *******************
            contasReceber.modificadoEm = DateTime.Now;
            contasReceber.modificadoPor = entrada.contexto.idUsuario;
            contasReceber.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }

        public static void GravaParcela(BalcaoVendas balcaoVendas, IContasReceberData contaReceberData, ContextPage contexto, OrgConfig orgConfig)
        {
            decimal valorParcela = balcaoVendas.valorTotal / balcaoVendas.parcelas;

            DateTime dataCredito = DateTime.Now.AddDays(orgConfig.qtdDiasCartaoCredito);

            int parcela = 1;
            for (int i = 0; i < balcaoVendas.parcelas; i++)
            {


                ContasReceber contaReceber = new ContasReceber();
                contaReceber.valor = valorParcela;
                contaReceber.valorRestante = valorParcela;
                contaReceber.tipoLancamento = CustomEnum.TipoLancamento.automatico;
                contaReceber.statusContaReceber = CustomEnumStatus.StatusContaReceber.agendado;
                contaReceber.origemContaReceber = CustomEnum.OrigemContaReceber.BalcaoVendas;

                if (balcaoVendas.idCliente != Guid.Empty) contaReceber.idCliente = balcaoVendas.idCliente;

                contaReceber.numeroReferencia = balcaoVendas.codigo;

                if (balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Boleto || balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Cheque)
                {
                    contaReceber.titulo = "Parcela Boleto/Cheque -" + parcela.ToString() + "/" + balcaoVendas.parcelas.ToString() + " - Venda Balcão";
                    contaReceber.dataPagamento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, balcaoVendas.diaVencimento);
                    contaReceber.dataPagamento = contaReceber.dataPagamento.AddMonths(parcela);
                    ContasReceberRules.ContasReceberCreate(contaReceber, contaReceberData, contexto);
                }

                if (balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.CartaoCredito & orgConfig.creditoGeraContasReceber == true)
                {
                    contaReceber.titulo = "Parcela Cartão Crédito -" + parcela.ToString() + "/" + balcaoVendas.parcelas.ToString() + " - Venda Balcão";

                    if (i == 0)
                    {
                        contaReceber.dataPagamento = dataCredito;
                    }
                    else
                    {
                        dataCredito = dataCredito.AddMonths(1);
                        contaReceber.dataPagamento = dataCredito;

                    }

                    ContasReceberRules.ContasReceberCreate(contaReceber, contaReceberData, contexto);
                }

                parcela++;
            }
        }

        public static void GravaDebito(BalcaoVendas balcaoVendas, IContasReceberData contaReceberData, ContextPage contexto, OrgConfig orgConfig)
        {
            ContasReceber contaReceber = new ContasReceber();
            contaReceber.valor = balcaoVendas.valorTotal;
            contaReceber.valorRestante = balcaoVendas.valorTotal;

            contaReceber.tipoLancamento = CustomEnum.TipoLancamento.automatico;
            contaReceber.statusContaReceber = CustomEnumStatus.StatusContaReceber.agendado;
            contaReceber.origemContaReceber = CustomEnum.OrigemContaReceber.BalcaoVendas;

            if (balcaoVendas.idCliente != Guid.Empty) contaReceber.idCliente = balcaoVendas.idCliente;

            contaReceber.numeroReferencia = balcaoVendas.codigo;

            if (balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.CartaoDebito & orgConfig.debitoGeraContasReceber == true)
            {
                contaReceber.titulo = "Débito - Venda Balcão";
                contaReceber.dataPagamento = DateTime.Now;
                contaReceber.dataPagamento = contaReceber.dataPagamento.AddDays(orgConfig.qtdDiasCartaoDebito);
                ContasReceberRules.ContasReceberCreate(contaReceber, contaReceberData, contexto);
            }
        }
        public static void GravaConsignado(BalcaoVendas balcaoVendas, IContasReceberData contaReceberData, ContextPage contexto, OrgConfig orgConfig)
        {
            ContasReceber contaReceber = new ContasReceber();
            contaReceber.valor = balcaoVendas.valorTotal;
            contaReceber.valorRestante = balcaoVendas.valorTotal;

            contaReceber.tipoLancamento = CustomEnum.TipoLancamento.automatico;
            contaReceber.statusContaReceber = CustomEnumStatus.StatusContaReceber.agendado;
            contaReceber.origemContaReceber = CustomEnum.OrigemContaReceber.BalcaoVendas;

            if (balcaoVendas.idCliente != Guid.Empty) contaReceber.idCliente = balcaoVendas.idCliente;

            contaReceber.numeroReferencia = balcaoVendas.codigo;
            contaReceber.titulo = "Consignado - Venda Balcão";
            contaReceber.dataPagamento = DateTime.Now;
            contaReceber.dataPagamento = contaReceber.dataPagamento.AddDays(1);
            ContasReceberRules.ContasReceberCreate(contaReceber, contaReceberData, contexto);

        }

        public static void CalculoPagamento(Guid idContasReceber, IPagamentoData pagamentoData, IContasReceberData contasReceberData)
        {
            List<Pagamento> pagamentos = new List<Pagamento>();
            ContasReceber contasReceber = contasReceberData.Get(idContasReceber);

            pagamentos = pagamentoData.GetAllByContasReceber(idContasReceber);         

            decimal Total = 0;            

            foreach (var item in pagamentos)
            {
                Total += item.valor;
            } 

            SqlGeneric sql = new SqlGeneric();
            sql.AtualizaContasReceber(Total, contasReceber.valor - Total, contasReceber.id);

        }

        public static bool ValidaCalculoPagamento(ref ContasReceber contasReceber, IPagamentoData pagamentoData)
        {
            List<Pagamento> pagamentos = new List<Pagamento>();

            pagamentos = pagamentoData.GetAllByContasReceber(contasReceber.id);

            decimal Total = 0;

            foreach (var item in pagamentos)
            {
                Total += item.valor;
            }          

            if (Total == contasReceber.valor)
            {
                return true;
            }

            return false;

        }
    }
}
