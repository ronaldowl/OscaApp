using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class OrgConfigData : IOrgConfigData
    {
        private ContexDataService db;

          public OrgConfigData(ContexDataService dbContext)
        {
            this.db = dbContext;
        }
        public void Add(OrgConfig modelo)
        {
            try
            {
                db.Add(modelo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void Update(OrgConfig modelo)
        {
            try
            {
                db.Attach(modelo);
                db.Entry(modelo).Property("mensagemPedido").IsModified            = true;
                db.Entry(modelo).Property("margemBaseProduto").IsModified         = true;
                db.Entry(modelo).Property("qtdDiasCartaoCredito").IsModified      = true;
                db.Entry(modelo).Property("qtdDiasCartaoDebito").IsModified       = true;
                db.Entry(modelo).Property("mensagemCupom").IsModified             = true;
                db.Entry(modelo).Property("tituloImpressao").IsModified           = true;        
                db.Entry(modelo).Property("quantidadeMinimaProduto").IsModified  = true;
                db.Entry(modelo).Property("cupom_altura").IsModified = true;
                db.Entry(modelo).Property("cupom_largura").IsModified = true;
                db.Entry(modelo).Property("cupom_fontesize").IsModified = true;

                db.Entry(modelo).Property("creditoGeraContasReceber").IsModified = true;
                db.Entry(modelo).Property("debitoGeraContasReceber").IsModified = true;
                
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
        public OrgConfig Get(Guid id)
        {
            List<OrgConfig> retorno = new List<OrgConfig>();
            try
            {
                retorno = db.OrgsConfig.FromSql("SELECT * FROM OrgConfig WHERE  idOrganizacao = '" + id.ToString() + "'").ToList();
            }
            catch (SqlException ex)
            {
            }
            return retorno[0];
        }      
    }
}