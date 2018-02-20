using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using System.Collections.Generic;

namespace OscaApp.RulesServices
{
    public static class OrdemServicoRules
    {
        public static bool OrdemServicoCreate(OrdemServicoViewModel entrada,out OrdemServico ordemServico)
        {
            ordemServico = new OrdemServico();
            ordemServico = entrada.ordemServico;

            SqlGenericService servico = new SqlGenericService();           
         
            if (ordemServico.idOrganizacao != null)
            {
                ordemServico.codigo = servico.RetornaNovaPosicao(5, entrada.contexto.idOrganizacao);
                ordemServico.statusOrdemServico = CustomEnumStatus.StatusOrdemServico.EmAndamento;
                ordemServico.idCliente = entrada.cliente.id;
                ordemServico.idCategoriaManutencao = entrada.categoriaManutencao.id;

                //************ Objetos de controle de acesso para Create ******************
                ordemServico.criadoEm = DateTime.Now;
                ordemServico.criadoPor = entrada.contexto.idUsuario;
                ordemServico.criadoPorName = entrada.contexto.nomeUsuario;
                ordemServico.modificadoEm = DateTime.Now;
                ordemServico.modificadoPor = entrada.contexto.idUsuario;
                ordemServico.modificadoPorName = entrada.contexto.nomeUsuario;
                ordemServico.idOrganizacao = entrada.contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool OrdemServicoUpdate(OrdemServicoViewModel entrada, out OrdemServico ordemServico)
        {
            ordemServico = new OrdemServico();
            ordemServico = entrada.ordemServico;
            ordemServico.idCategoriaManutencao = entrada.categoriaManutencao.id; 

            //************ Objetos de controle de acesso para Update*******************
            ordemServico.modificadoEm = DateTime.Now;
            ordemServico.modificadoPor = entrada.contexto.idUsuario;
            ordemServico.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }
        public static bool OrdemServicoUpdateStatus(OrdemServicoViewModel entrada, out OrdemServico ordemServico)
        {
            ordemServico = new OrdemServico();
            ordemServico = entrada.ordemServico;           

            //************ Objetos de controle de acesso para Update*******************
            ordemServico.modificadoEm = DateTime.Now;
            ordemServico.modificadoPor = entrada.contexto.idUsuario;
            ordemServico.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }
        public static void CalculoOrdem(ref OrdemServico ordem, IServicoOrdemData ServicosOrdem)
        {
            List<ServicoOrdem> ordens = new List<ServicoOrdem>();

            ordens = ServicosOrdem.GetByServicoOrdemId(ordem.id);

            decimal Total = 0;

            foreach (var item in ordens)
            {
                Total += item.totalGeral;
            }

            if (ordem.tipoDesconto == CustomEnum.tipoDesconto.Money)
            {
                Total = Total - ordem.valorDesconto;
            }
            else
            {
                Total = (Total / 100) * ordem.valorDescontoPercentual;
            }

            ordem.valorTotal = Total;


        }

    }
}
