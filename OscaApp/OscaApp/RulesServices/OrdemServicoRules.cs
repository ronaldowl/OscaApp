using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class OrdemServicoRules
    {
        public static bool OrdemServicoCreate(OrdemServicoViewModel entrada,out OrdemServico ordemServico, ContextPage contexto )
        {
            ordemServico = new OrdemServico();
            ordemServico = entrada.ordemServico;

            SqlGenericService servico = new SqlGenericService();           
         
            if (ordemServico.idOrganizacao != null)
            {
                ordemServico.codigo = servico.RetornaNovaPosicao(5, contexto.idOrganizacao);
                ordemServico.statusOrdemServico = CustomEnumStatus.StatusOrdemServico.EmAndamento;
                ordemServico.idCliente = entrada.cliente.id;
                ordemServico.idCategoriaManutencao = entrada.categoriaManutencao.id;

                //************ Objetos de controle de acesso ******************
                ordemServico.criadoEm = DateTime.Now;
                ordemServico.criadoPor = contexto.idUsuario;
                ordemServico.criadoPorName = contexto.nomeUsuario;
                ordemServico.modificadoEm = DateTime.Now;
                ordemServico.modificadoPor = contexto.idUsuario;
                ordemServico.modificadoPorName = contexto.nomeUsuario;
                ordemServico.idOrganizacao = contexto.idOrganizacao;
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

            //************ Objetos de controle de acesso *******************
            ordemServico.modificadoEm = DateTime.Now;
            ordemServico.modificadoPor = entrada.contexto.idUsuario;
            ordemServico.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
