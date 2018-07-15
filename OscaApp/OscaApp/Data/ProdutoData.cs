using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using OscaApp.framework.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ProdutoData : IProdutoData
    {
        private ContexDataService db;
        //public DbSet<Cliente> Books { get; set; }

        public ProdutoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(Produto produto)
        {
            try
            {
                db.Produtos.Add(produto);                
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public void Update(Produto produto)
        {
            try
            {
                db.Attach(produto);
                
                db.Entry(produto).Property("codigoBarra").IsModified = true;
                db.Entry(produto).Property("nome").IsModified = true;
                db.Entry(produto).Property("quantidade").IsModified = true;
                db.Entry(produto).Property("icms").IsModified = true;
                db.Entry(produto).Property("iss").IsModified = true;
                db.Entry(produto).Property("ipi").IsModified = true;

                db.Entry(produto).Property("descricao").IsModified = true;
                db.Entry(produto).Property("codigoFabricante").IsModified = true;
                db.Entry(produto).Property("fabricante").IsModified = true;
                db.Entry(produto).Property("altura").IsModified = true;
                db.Entry(produto).Property("largura").IsModified = true;
                db.Entry(produto).Property("peso").IsModified = true;
                db.Entry(produto).Property("area").IsModified = true;
                db.Entry(produto).Property("valorCompra").IsModified = true;
                db.Entry(produto).Property("margemLucroBase").IsModified = true;
                db.Entry(produto).Property("tipoProduto").IsModified = true;                
                db.Entry(produto).Property("formaVendaProduto").IsModified = true;
                db.Entry(produto).Property("modificadoPorName").IsModified = true;
                db.Entry(produto).Property("modificadoPor").IsModified = true;
                db.Entry(produto).Property("modificadoEm").IsModified = true;

                db.SaveChanges(); 
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void UpdateImage(Produto produto)
        {
            try
            {
                db.Attach(produto);
                db.Entry(produto).Property("urlProduto").IsModified = true;
              
                db.Entry(produto).Property("modificadoPorName").IsModified = true;
                db.Entry(produto).Property("modificadoPor").IsModified = true;
                db.Entry(produto).Property("modificadoEm").IsModified = true;
                


                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public Produto Get(Guid id, Guid idOrg)
        {
            List<Produto> retorno = new List<Produto>();
            retorno = db.Produtos.FromSql("SELECT * FROM Produto where  id = '" + id.ToString() + "' and idOrganizacao = '"+ idOrg.ToString() + "'" ).ToList();
            return retorno[0];
        }

        public Relacao GetRelacao(Guid id)
        {
            Relacao relacao = new Relacao();
            relacao.tipoObjeto = CustomEntityEnum.Entidade.Produto;

            List<Produto> retorno = new List<Produto>();
            retorno = db.Produtos.FromSql("SELECT * FROM Produto where  id = '" + id.ToString() +  "'").ToList();
            relacao.id = retorno[0].id;
            relacao.idName = retorno[0].nome;
            relacao.idOrganizacao = retorno[0].idOrganizacao;

            return relacao;
        }
        public List<Produto> GetAll(Guid idOrg)
        {
            List<Produto> retorno = new List<Produto>();
            retorno = db.Produtos.FromSql("SELECT * FROM Produto where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
    }
}
