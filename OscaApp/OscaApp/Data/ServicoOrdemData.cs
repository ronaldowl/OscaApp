using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ServicoOrdemData : IServicoOrdemData
    {
        private ContexDataService db;

        public ServicoOrdemData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }

        public void Delete(ServicoOrdem modelo)
        {
            try
            {
                db.ServicosOrdem.Remove(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Add(ServicoOrdem modelo)
        {
            try
            {
                db.ServicosOrdem.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Update(ServicoOrdem modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("valor").IsModified = true;
                db.Entry(modelo).Property("valorDescontoMoney").IsModified = true;
                db.Entry(modelo).Property("valorDescontoPercentual").IsModified = true;
                db.Entry(modelo).Property("tipoDesconto").IsModified = true;
                db.Entry(modelo).Property("quantidade").IsModified = true;
                db.Entry(modelo).Property("valorDesconto").IsModified = true;
                db.Entry(modelo).Property("total").IsModified = true;
                db.Entry(modelo).Property("totalGeral").IsModified = true;
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
        public ServicoOrdem Get(Guid id)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            try
            {
                retorno = db.ServicosOrdem.FromSql("SELECT * FROM ServicoOrdem where  id = '" + id.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }
            return retorno[0];
        }
        public List<ServicoOrdem> GetAll(Guid idOrg)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            retorno = db.ServicosOrdem.FromSql("SELECT * FROM ServicoOrdem  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }

        public List<ServicoOrdem> GetByServicoOrdemId(Guid idOrdemServico)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            retorno = db.ServicosOrdem.FromSql("SELECT * FROM ServicoOrdem  where  idOrdemServico = '" + idOrdemServico.ToString() + "'").ToList();
            return retorno;

        }

        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            List<Relacao> lista = new List<Relacao>();

            retorno = db.ServicosOrdem.FromSql("SELECT * FROM ServicoOrdem  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
    }
}
