using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class FaturamentoRules
    {
        
        public static bool InsereFaturamento(Pedido modelo, Guid idOrganizacao)
        {
            SqlGeneric sqlGeneric = new SqlGeneric();
            Faturamento fat = new Faturamento();
            fat.valor = modelo.valorTotal;
            fat.idOrganizacao = idOrganizacao;
            fat.idReferencia = modelo.id;
            fat.origemFaturamento = CustomEnum.OrigemFaturamento.Pedido;
            sqlGeneric.InsereFaturamento(fat, 0);

            return false;
        }

        public static bool InsereFaturamento(Atendimento modelo, Guid idOrganizacao)
        {
            SqlGeneric sqlGeneric = new SqlGeneric();
            Faturamento fat = new Faturamento();
            fat.valor = modelo.valor;
            fat.idOrganizacao = idOrganizacao;
            fat.idReferencia = modelo.id;
            fat.origemFaturamento = CustomEnum.OrigemFaturamento.Atendimento;
            sqlGeneric.InsereFaturamento(fat, 1);

            return false;
        }

        public static bool InsereFaturamento(OrdemServico modelo, Guid idOrganizacao)
        {
            SqlGeneric sqlGeneric = new SqlGeneric();
            Faturamento fat = new Faturamento();
            fat.valor = modelo.valorTotal;
            fat.idOrganizacao = idOrganizacao;
            fat.idReferencia = modelo.id;
            fat.origemFaturamento = CustomEnum.OrigemFaturamento.OrdemServico;
            sqlGeneric.InsereFaturamento(fat, 2);

            return false;
        }

        public static bool InsereFaturamento(BalcaoVendas modelo, Guid idOrganizacao)
        {
            SqlGeneric sqlGeneric = new SqlGeneric();
            Faturamento fat = new Faturamento();
            fat.valor = modelo.valorTotal;
            fat.idOrganizacao = idOrganizacao;
            fat.idReferencia = modelo.id;
            fat.origemFaturamento = CustomEnum.OrigemFaturamento.BalcaoVendas;
            sqlGeneric.InsereFaturamento(fat, 3);

            return false;
        }
    }
}
