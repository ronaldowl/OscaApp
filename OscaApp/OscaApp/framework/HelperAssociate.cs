using OscaApp.Data;
using OscaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.framework
{
    public static class HelperAssociate
    {
        public static List<LookupItemLista> ConvertToLookupItemLista(List<ItemListaPreco> itens)
        {
            List<LookupItemLista> retorno = new List<LookupItemLista>();
            SqlGenericData sqldata = new SqlGenericData();

            foreach (var item in itens)
            {
                LookupItemLista X = new LookupItemLista();
                X.produto = sqldata.RetornaProduto(item.idProduto);
                X.itemlistaPreco = item;

                retorno.Add(X);
            }
            return retorno;
        }
    }
}
