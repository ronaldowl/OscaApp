using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ContasReceberRules
    {
        public static bool ContasReceberCreate(ContasReceberViewModel entrada,out ContasReceber contasReceber, ContextPage contexto )
        {
            contasReceber = new ContasReceber();
            contasReceber = entrada.contasReceber;

            if (entrada.cliente != null) contasReceber.idCliente = entrada.cliente.id;

            contasReceber.codigo = AutoNumber.GeraCodigo(21, contexto.idOrganizacao);
         
            if (contasReceber.codigo != null)
            {
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


            //************ Objetos de controle de acesso *******************
            contasReceber.modificadoEm = DateTime.Now;
            contasReceber.modificadoPor = entrada.contexto.idUsuario;
            contasReceber.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }
        
        public static void GravaParcela(BalcaoVendas balcaoVendas, IContasReceberData contaReceberData, ContextPage contexto)
        {
            decimal valorParcela = balcaoVendas.valorTotal / balcaoVendas.parcelas;
                                 

            for (int i = 1; i < balcaoVendas.parcelas; i++)
            {
                ContasReceber contaReceber = new ContasReceber();
                contaReceber.valor = valorParcela;
                contaReceber.tipoLancamento = CustomEnum.TipoLancamento.automatico;
                contaReceber.statusContaReceber = CustomEnumStatus.StatusContaReceber.agendado;
                contaReceber.origemContaReceber = CustomEnum.OrigemContaReceber.BalcaoVendas;
                contaReceber.titulo = "Parcela -" + i + "/" + balcaoVendas.parcelas.ToString() + " - Venda Balcão";
                contaReceber.numeroReferencia = balcaoVendas.codigo;

                if (balcaoVendas.tipoPagamento == CustomEnum.tipoPagamento.Boleto)
                {
                    contaReceber.dataPagamento = DateTime.Now.AddMonths(i).AddDays(balcaoVendas.diaVencimento);
                }

                ContasReceberRules.ContasReceberCreate(contaReceber, contaReceberData, contexto);
            }

        }
    }
}
