using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaApp.framework.Models;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

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
                db.ServicosOrdem.Remove(modelo);
                db.SaveChanges();       

        }
        public void Add(ServicoOrdem modelo)
        {          
                db.ServicosOrdem.Add(modelo);
                db.SaveChanges();      

        }
        public void Update(ServicoOrdem modelo)
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
                db.Entry(modelo).Property("idServico").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
            
                db.SaveChanges();


        }
        public ServicoOrdem Get(Guid id)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
          
            retorno = (from A in db.ServicosOrdem where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<ServicoOrdem> GetAll(Guid idOrg)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            retorno = (from A in db.ServicosOrdem where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;

        }
        public List<ServicoOrdem> GetByServicoOrdemId(Guid idOrdemServico)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            retorno = (from A in db.ServicosOrdem where A.idOrdemServico.Equals(idOrdemServico) select A).ToList();

            return retorno;

        }
        public List<Relacao> GetAllRelacao(Guid idOrg)
        {
            List<ServicoOrdem> retorno = new List<ServicoOrdem>();
            List<Relacao> lista = new List<Relacao>();

            retorno = (from A in db.ServicosOrdem where A.idOrganizacao.Equals(idOrg) select A).ToList();

            return Relacao.ConvertToRelacao(retorno);

        }
        public List<ServicoOrdemGridViewModel> GetAllGridViewModel(Guid idOrdem)
        {
            List<ServicoOrdem> itens = new List<ServicoOrdem>();

            itens = (from A in db.ServicosOrdem where A.idOrdemServico.Equals(idOrdem) select A).ToList();

            return HelperAssociate.ConvertToGridServicoOrdem(itens);
        }

    }
}
