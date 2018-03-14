using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using System.Collections.Generic;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class OrdemServicoRules
    {
        public static bool OrdemServicoCreate(OrdemServicoViewModel entrada,out OrdemServico ordemServico)
        {
            ordemServico = new OrdemServico();
            ordemServico = entrada.ordemServico;

            SqlGeneric servico = new SqlGeneric();           
         
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
            ordemServico.idProfissional = entrada.profissional.id;
            ordemServico.idCliente = entrada.cliente.id;

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
        public static void CalculoOrdem(ref OrdemServico ordem, IServicoOrdemData ServicosOrdem, IProdutoOrdemData ProdutosOrdem)
        {
            List<ServicoOrdem> ordens = new List<ServicoOrdem>();
            List<ProdutoOrdem> produtos = new List<ProdutoOrdem>();

            ordens = ServicosOrdem.GetByServicoOrdemId(ordem.id);
            produtos = ProdutosOrdem.GetByProdutoOrdemId(ordem.id);

            decimal TotalS = 0;
            decimal TotalP = 0;
            decimal Total = 0;

            foreach (var item in ordens)
            {
                TotalS += item.totalGeral;
            }

            foreach (var item in produtos)
            {
                TotalP += item.totalGeral;
            }

            Total = TotalP + TotalS;

            decimal totalPercentual = 0;

            if (ordem.tipoDesconto == CustomEnum.tipoDesconto.Money)
            {
                Total = Total - ordem.valorDesconto;
            }
            else
            {
                totalPercentual = (Total / 100) * ordem.valorDescontoPercentual;
            }

            ordem.valorTotal = Total - totalPercentual;


        }

    }
}
