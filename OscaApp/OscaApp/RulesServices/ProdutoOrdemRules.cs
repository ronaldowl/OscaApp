using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ProdutoOrdemRules
    {
        public static bool ProdutoOrdemCreate(ProdutoOrdemViewModel entrada,out ProdutoOrdem modelo, ContextPage contexto )
        {
            modelo = new ProdutoOrdem();
            modelo = entrada.produtoOrdem;
            modelo.idProduto = entrada.produto.id;
            modelo.idOrdemServico = entrada.ordemServico.id;
            modelo.status = CustomEnumStatus.Status.Ativo;      
                     
            if (modelo.idOrganizacao != null)
            {
                //************ Objetos de controle de acesso ******************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                //************* Executa calculo ************
                ProdutoOrdemRules.CalculaProdutoOrdem(ref modelo);

                return true;
            }           
            
            return false;
        }
        public static bool ProdutoOrdemUpdate(ProdutoOrdemViewModel entrada,out ProdutoOrdem modelo, ContextPage contexto)
        {
            modelo = new ProdutoOrdem();
            modelo = entrada.produtoOrdem;
            modelo.idProduto = entrada.produto.id;

            //************ Objetos de controle de acesso *******************           
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor =  contexto.idUsuario;
            modelo.modificadoPorName = contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            //************* Executa calculo ************
            ProdutoOrdemRules.CalculaProdutoOrdem(ref modelo);

            return true;
        }
        public static void CalculaProdutoOrdem(ref ProdutoOrdem modelo)
        {

            if (modelo.tipoDesconto == CustomEnum.tipoDesconto.Money)
            {
                modelo.total = modelo.valor * modelo.quantidade;
                modelo.valorDesconto = modelo.valorDescontoMoney;
                modelo.totalGeral = modelo.total - modelo.valorDesconto;
            }
            else
            {
                modelo.total = modelo.valor * modelo.quantidade;
                modelo.valorDesconto = (modelo.total / 100) * modelo.valorDescontoPercentual;
                modelo.totalGeral = modelo.total - modelo.valorDesconto;
            }
        }
    }
}
