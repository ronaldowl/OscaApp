using Microsoft.AspNetCore.Mvc.Rendering;
using OscaApp.framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.framework
{   

    /// <summary>
    /// Classe especializada em tratar campos do tipo Lookup
    /// </summary>
    public static class HelperLookup
    {
        public static Relacao PreencheOrigemContasReceber(CustomEnum.OrigemContaReceber origem,Guid idReferencia ,SqlGenericData sqlData)
        {
            Relacao retorno = new Relacao();           

            if (origem == CustomEnum.OrigemContaReceber.BalcaoVendas)
            {
                retorno = sqlData.RetornaRelacaoBalcaoVendas(idReferencia);

                retorno.tipoObjeto = CustomEntityEnum.Entidade.BalcaoVendas;
                retorno.id = idReferencia;
                retorno.idName = retorno.codigo;
            }

            if (origem == CustomEnum.OrigemContaReceber.Atendimento)
            {
                retorno = sqlData.RetornaRelacaoAtendimento(idReferencia);

                retorno.tipoObjeto = CustomEntityEnum.Entidade.Atendimento;
                retorno.id = idReferencia;
                retorno.idName = retorno.codigo;
            }

            if (origem == CustomEnum.OrigemContaReceber.OrdemServico)
            {
                retorno = sqlData.RetornaRelacaoOrdemServico(idReferencia);

                retorno.tipoObjeto = CustomEntityEnum.Entidade.OrdemServico;
                retorno.id = idReferencia;
                retorno.idName = retorno.codigo;
            }

            if (origem == CustomEnum.OrigemContaReceber.Pedido)
            {
                retorno = sqlData.RetornaRelacaoPedido(idReferencia);

                retorno.tipoObjeto = CustomEntityEnum.Entidade.Pedido;
                retorno.id = idReferencia;
                retorno.idName = retorno.codigo;
            } 

            return retorno;
        }
    }
}
