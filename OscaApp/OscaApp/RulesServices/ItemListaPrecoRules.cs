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
    public static class ItemListaPrecoRules
    {
        public static bool ItemListaPrecoCreate(ItemListaPrecoViewModel entrada,out ItemListaPreco modelo, ContextPage contexto )
        {
            modelo = new ItemListaPreco ();
            modelo = entrada.itemlistaPreco;
                                
                     
            if (modelo.idListaPreco  != null)
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

                return true;
            }           
            
            return false;
        }
        public static bool ItemListaPrecoUpdate(ItemListaPrecoViewModel entrada,out ItemListaPreco modelo)
        {
            modelo = new ItemListaPreco();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.itemlistaPreco;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }


      
    }
}
