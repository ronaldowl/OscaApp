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
    public static class ProdutoFornecedorRules
    {
        public static bool ProdutoFornecedorCreate(ProdutoFornecedorViewModel entrada,out ProdutoFornecedor modelo, ContextPage contexto )
        {
            modelo = new ProdutoFornecedor ();
            modelo = entrada.produtoFornecedor;

            if (entrada.produto.id != Guid.Empty) modelo.idProduto = entrada.produto.id;

            if (entrada.fornecedor.id != Guid.Empty) modelo.idFornecedor = entrada.fornecedor.id;

            if (entrada.fornecedor.id != Guid.Empty & entrada.produto.id != Guid.Empty)
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

        public static ProdutoFornecedor ProdutoFornecedorCreateRelacionado(ProdutoFornecedor modelo, ContextPage contexto)
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
 

            return modelo;
        }

        public static bool ProdutoFornecedorUpdate(ProdutoFornecedorViewModel entrada,out ProdutoFornecedor modelo)
        {
            modelo = new ProdutoFornecedor();

            modelo = entrada.produtoFornecedor;

            if (entrada.produto.id != Guid.Empty) modelo.idProduto = entrada.produto.id;

            if (entrada.fornecedor.id != Guid.Empty) modelo.idFornecedor = entrada.fornecedor.id;

            //************ Objetos de controle de acesso *******************        

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************

            return true;
        }


      
    }
}
