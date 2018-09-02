using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OscaApp.Data
{
    public class PerfilAcessoData : IPerfilAcesso
    {
        // FIELDS
        private ContexDataService db;

        // CTOR
        public PerfilAcessoData(ContexDataService dbContext)
        {
            this.db = dbContext;
        } // end of ctor

        public void Add(PerfilAcesso modelo)
        {            
                db.PerfilAcessos.Add(modelo);
                db.SaveChanges();
       
        } // end of method Add

        public PerfilAcesso Get(Guid id )
        {
            List<PerfilAcesso> retorno = new List<PerfilAcesso>();

            retorno = (from A in db.PerfilAcessos where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }

        public List<PerfilAcesso> GetAll(Guid idOrg)
        {
            List<PerfilAcesso> retorno = new List<PerfilAcesso>();
         
            retorno = (from A in db.PerfilAcessos where A.idOrganizacao.Equals(idOrg) select A).ToList();
            return retorno;
        }

        public void Update(PerfilAcesso modelo)
        {
           
                db.Attach(modelo);
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                db.Entry(modelo).Property("nome").IsModified = true;

                db.Entry(modelo).Property("permitePainelVendas").IsModified = true;
                db.Entry(modelo).Property("permitePainelServico").IsModified = true;
                db.Entry(modelo).Property("permitePainelFinanceiro").IsModified = true;
                db.Entry(modelo).Property("permitePainelCadastro").IsModified = true;
                db.Entry(modelo).Property("permitePainelConfiguracoes").IsModified = true;         
                db.Entry(modelo).Property("permitePainelGlobal").IsModified = true;
                db.Entry(modelo).Property("permitePainelSuporte").IsModified = true;
                db.Entry(modelo).Property("permitePainelGerenciamento").IsModified = true;       
       
                db.SaveChanges();   

        } // end of method Update

    } // end of 
}
