using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;

namespace OscaApp.Data
{
    public class ProdutoPedidoData : IProdutoPedidoData
    {
        private ContexDataService db;

        public ProdutoPedidoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Delete(ProdutoPedido modelo)
        {
            try
            {
                db.ProdutosPedido.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Add(ProdutoPedido modelo)
        {
            try
            {
                db.ProdutosPedido.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Update(ProdutoPedido modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("nome").IsModified = true;
                db.Entry(modelo).Property("descricao").IsModified = true;
                db.Entry(modelo).Property("dataValidade").IsModified = true;
                db.Entry(modelo).Property("fimValidade").IsModified = true;

                db.Entry(modelo).Property("padrao").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;


                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public ProdutoPedido Get(Guid id)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            try
            {
                retorno = db.ProdutosPedido.FromSql("SELECT * FROM ProdutoPedido where  id = '" + id.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<ProdutoPedido> GetAll(Guid idOrg)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            retorno = db.ProdutosPedido.FromSql("SELECT * FROM ProdutoPedido  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }

        public List<ProdutoPedido> GetByPedidoId(Guid idPedido)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            retorno = db.ProdutosPedido.FromSql("SELECT * FROM ProdutoPedido  where  idPedido = '" + idPedido.ToString() + "'").ToList();
            return retorno;

        }

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ProdutoPedido> retorno = new List<ProdutoPedido>();
            List<Relacao> lista = new List<Relacao>();

            retorno = db.ProdutosPedido.FromSql("SELECT * FROM ProdutoPedido  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
    }
}
