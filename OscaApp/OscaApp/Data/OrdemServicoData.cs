using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class OrdemServicoData : IOrdemServicoData
    {
        private ContexDataService db;

          public OrdemServicoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(OrdemServico ordemServico)
        {
            try
            {
                db.OrdensServico.Add(ordemServico);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(OrdemServico modelo)
        {
            try
            {
                db.Attach(modelo);
                 
                db.Entry(modelo).Property("laudo").IsModified = true;
                db.Entry(modelo).Property("problema").IsModified = true;
                db.Entry(modelo).Property("diagnostico").IsModified = true;
                db.Entry(modelo).Property("modelo").IsModified = true;
                db.Entry(modelo).Property("marca").IsModified = true;
                db.Entry(modelo).Property("cor").IsModified = true;
                db.Entry(modelo).Property("numeroSerie").IsModified = true;
                db.Entry(modelo).Property("idCategoriaManutencao").IsModified = true;
                db.Entry(modelo).Property("observacao").IsModified = true;
                db.Entry(modelo).Property("idProfissional").IsModified = true;
                db.Entry(modelo).Property("idCliente").IsModified = true;

                db.Entry(modelo).Property("tipo").IsModified = true;
                db.Entry(modelo).Property("placa").IsModified = true;
                db.Entry(modelo).Property("valorDesconto").IsModified = true;
                db.Entry(modelo).Property("valorDescontoPercentual").IsModified = true;
                db.Entry(modelo).Property("tipoDesconto").IsModified = true;
                db.Entry(modelo).Property("condicaoPagamento").IsModified = true;
                db.Entry(modelo).Property("tipoPagamento").IsModified = true;
                db.Entry(modelo).Property("statusOrdemServico").IsModified = true;
                db.Entry(modelo).Property("valorTotal").IsModified = true;
                db.Entry(modelo).Property("dataFechamento").IsModified = true;


                db.Entry(modelo).Property("modificadoPor").IsModified            = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified        = true;
                db.Entry(modelo).Property("modificadoEm").IsModified             = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void UpdateStatus(OrdemServico modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("dataFechamento").IsModified = true;
                db.Entry(modelo).Property("statusOrdemServico").IsModified = true;
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
        public OrdemServico Get(Guid id)
        {
            List<OrdemServico> retorno = new List<OrdemServico>();
            try
            {
                retorno = db.OrdensServico.FromSql("SELECT * FROM OrdemServico WHERE  id = '" + id.ToString() + "'" ).ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }
        public List<OrdemServico> GetAll(Guid idOrg)
        {
            List<OrdemServico> retorno = new List<OrdemServico>();
            retorno = db.OrdensServico.FromSql("SELECT * FROM OrdemServico WHERE  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            return retorno;

        }
        public List<OrdemServicoGridViewModel> GetAllGridViewModel(Guid idOrg, int view)
        {
            List<OrdemServico > itens = new List<OrdemServico >();

            //Em Andamento
            if (view == 0)
            {
                itens = db.OrdensServico.FromSql("SELECT * FROM OrdemServico  where statusOrdemServico = 1  and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Aguardando produto
            if (view == 1)
            {
                itens = db.OrdensServico.FromSql("SELECT * FROM OrdemServico  where statusOrdemServico in (3)  and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Para Entrega
            if (view == 2)
            {
                itens = db.OrdensServico.FromSql("SELECT * FROM OrdemServico  where statusOrdemServico in (4) and idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos  Fechado
            if (view == 3)
            {
                itens = db.OrdensServico.FromSql("SELECT * FROM OrdemServico  where statusOrdemServico in (2,5) and  idOrganizacao = '" + idOrg.ToString() + "'").ToList();
            }

            //Todos   
            if (view == 4)
            {
                itens = db.OrdensServico.FromSql("SELECT * FROM OrdemServico  where  idOrganizacao = '" + idOrg.ToString() + "'").ToList();

            }
 
            return HelperAssociate.ConvertToGridOrdemServico(itens);
        }
        public List<OrdemServicoGridViewModel> GetAllGridViewModelByCliente(Guid idCliente)
        {
            List<OrdemServico> retorno = new List<OrdemServico>();
            retorno = db.OrdensServico.FromSql("SELECT * FROM OrdemServico WHERE  idCliente = '" + idCliente.ToString() + "'").ToList();
            return HelperAssociate.ConvertToGridOrdemServico(retorno);
        }

        public List<OrdemServico> GetAllByIdCliente(Guid idCliente)
        {
            List<OrdemServico> retorno = new List<OrdemServico>();
            retorno = db.OrdensServico.FromSql("SELECT * FROM OrdemServico where  idCliente = '" + idCliente.ToString() + "'").ToList();
            return retorno;

        }
    }
}
