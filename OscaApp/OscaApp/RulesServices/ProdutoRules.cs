using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.RulesServices
{
    public static class ProdutoRules
    {
        public static bool MontaProdutoCreate(ProdutoViewModel entrada,out Produto prod, ContextPage contexto )
        {
            prod = new Produto ();
            prod = entrada.produto;

            prod.codigo = AutoNumber.GeraCodigo(7, contexto.idOrganizacao);

            if (prod.codigo != null)
            {                       

                //Preenche campo default 
                prod.modificadoEm  = DateTime.Now;
                prod.criadoEm = DateTime.Now;             
                prod.idOrganizacao = contexto.idOrganizacao;
                prod.criadoPor = contexto.idUsuario;

                return true;
            }           
            
            return false;
        }
        public static bool MontaProdutoUpdate(ProdutoViewModel entrada,out Produto modelo)
        {
                modelo = new Produto();
                modelo = entrada.produto;
                modelo.id = entrada.produto.id;         
                             
                return true;
        }       
    }
}
