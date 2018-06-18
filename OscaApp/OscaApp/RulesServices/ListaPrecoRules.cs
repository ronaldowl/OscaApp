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
    public static class ListaPrecoRules
    {
        public static bool ListaPrecoCreate(ListaPrecoViewModel entrada,out ListaPreco listapreco, ContextPage contexto )
        {
            listapreco = new ListaPreco ();
            listapreco = entrada.listaPreco;

         
            if (listapreco.nome != null)
            {
                //************ Objetos de controle de acesso ******************
                listapreco.criadoEm = DateTime.Now;
                listapreco.criadoPor = contexto.idUsuario;
                listapreco.criadoPorName = contexto.nomeUsuario;
                listapreco.modificadoEm = DateTime.Now;
                listapreco.modificadoPor = contexto.idUsuario;
                listapreco.modificadoPorName = contexto.nomeUsuario;
                listapreco.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool ListaPrecoUpdate(ListaPrecoViewModel entrada,out ListaPreco listapreco)
        {
            listapreco = new ListaPreco();

            //************ Objetos de controle de acesso *******************
            listapreco = entrada.listaPreco;
            listapreco.modificadoEm = DateTime.Now;
            listapreco.modificadoPor = entrada.contexto.idUsuario;
            listapreco.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }
        public static bool ConsultaListaPadrao(string idOrg, SqlGenericData sqlService)
        {
            Relacao listaPadrao = new Relacao();
            listaPadrao = sqlService.RetornaRelacaoListaPrecoPadrao(new Guid(idOrg));

            if (listaPadrao.idName != "") return true;

         
            return false;
        }
    }
}
