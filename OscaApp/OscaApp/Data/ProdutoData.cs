using System;
using System.Collections.Generic;
using System.Linq; 
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ProdutoData : IProdutoData
    {
        private ContexDataService db;
     
        public ProdutoData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(Produto produto)
        {      
                db.Produtos.Add(produto);                
                db.SaveChanges();    
        }
        public void Update(Produto produto)
        {
            
                db.Attach(produto);                
                db.Entry(produto).Property("codigoBarra").IsModified = true;
                db.Entry(produto).Property("nome").IsModified = true;
                db.Entry(produto).Property("quantidade").IsModified = true;
                db.Entry(produto).Property("quantidadeMinima").IsModified = true;
                db.Entry(produto).Property("icms").IsModified = true;
                db.Entry(produto).Property("iss").IsModified = true;
                db.Entry(produto).Property("ipi").IsModified = true;
                db.Entry(produto).Property("modelo").IsModified = true;
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
                db.Entry(produto).Property("classificacaoProduto").IsModified = true;                

                db.SaveChanges(); 
        

        }
        public void UpdateQuantity(Produto produto)
        {
            
                db.Attach(produto); 
                db.Entry(produto).Property("quantidade").IsModified = true;
                db.Entry(produto).Property("modificadoPorName").IsModified = true;
                db.Entry(produto).Property("modificadoPor").IsModified = true;
                db.Entry(produto).Property("modificadoEm").IsModified = true;

                db.SaveChanges();  

        }
        public void SetStatus(Produto produto)
        {
           
                db.Attach(produto);
                db.Entry(produto).Property("status").IsModified = true;
                db.Entry(produto).Property("modificadoPorName").IsModified = true;
                db.Entry(produto).Property("modificadoPor").IsModified = true;
                db.Entry(produto).Property("modificadoEm").IsModified = true;

                db.SaveChanges();       

        }
        public void UpdateImage(Produto produto)
        {
           
                db.Attach(produto);
                db.Entry(produto).Property("urlProduto").IsModified = true;
              
                db.Entry(produto).Property("modificadoPorName").IsModified = true;
                db.Entry(produto).Property("modificadoPor").IsModified = true;
                db.Entry(produto).Property("modificadoEm").IsModified = true;
      

                db.SaveChanges();
            
        }
        public Produto Get(Guid id )
        {
            List<Produto> retorno = new List<Produto>();
            retorno = (from A in db.Produtos where A.id.Equals(id) select A).ToList();
            return retorno[0];
        }
        public Relacao GetRelacao(Guid id)
        {
            Relacao relacao = new Relacao();
            relacao.tipoObjeto = CustomEntityEnum.Entidade.Produto;

            List<Produto> retorno = new List<Produto>();
           
            retorno = (from A in db.Produtos where A.id.Equals(id) select A).ToList();
            relacao.id = retorno[0].id;
            relacao.idName = retorno[0].nome;
            relacao.idOrganizacao = retorno[0].idOrganizacao;

            return relacao;
        }
        public List<Produto> GetAll(Guid idOrg, int view)
        {
            List<Produto> itens = new List<Produto>();

            //Produtos Ativos
            if (view == 0)
            {
                itens = (from A in db.Produtos where A.status == CustomEnumStatus.Status.Ativo & A.idOrganizacao.Equals(idOrg) select A).ToList();
            }

            //Produtos inativos
            if (view == 1)
            {              
                itens = (from A in db.Produtos where A.status == CustomEnumStatus.Status.Inativo & A.idOrganizacao.Equals(idOrg) select A).ToList();
            }

            //Produto - Quantidade <= 0
            if (view == 2)
            {
                itens = (from bl in db.Produtos where (bl.quantidade <= 0) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();
            }

            //Produto - Quantidade Minima <= 0
            if (view == 3)
            {
                itens = (from bl in db.Produtos where (bl.quantidade <= bl.quantidadeMinima  ) & (bl.idOrganizacao.Equals(idOrg)) select bl).ToList();
            }
          
            return itens;

        }
    }
}
