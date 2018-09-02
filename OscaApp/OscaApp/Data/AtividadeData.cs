using System;
using System.Collections.Generic;
using System.Linq;
using OscaApp.Models;
using OscaApp.Data;
using Microsoft.EntityFrameworkCore;
using OscaApp.framework.Models;
using OscaFramework.Models;
using OscaApp.ViewModels.GridViewModels;
using OscaApp.framework;

namespace OscaApp.Data
{
    public class AtividadeData : IAtividadeData
    {
        private ContexDataService db;  

        public AtividadeData(ContexDataService dbContext)
        {
            this.db = dbContext;        
        }
        public void Add(Atividade atividade)
        {           
                db.Atividades.Add(atividade);                
                db.SaveChanges();             
        }
        public void Delete(Atividade atividade)
        {
                db.Atividades.Remove(atividade);
                db.SaveChanges();
        }
        public void Update(Atividade modelo)
        {
       
                db.Attach(modelo);
                db.Entry(modelo).Property("descricao").IsModified           = true;
                db.Entry(modelo).Property("tipo").IsModified                = true;
                db.Entry(modelo).Property("idProfissional").IsModified      = true;
                db.Entry(modelo).Property("dataAlvo").IsModified            = true;
                db.Entry(modelo).Property("assunto").IsModified             = true;
                db.Entry(modelo).Property("statusAtividade").IsModified = true;                
                db.Entry(modelo).Property("modificadoPor").IsModified       = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified   = true;
                db.Entry(modelo).Property("modificadoEm").IsModified        = true;
                db.SaveChanges(); 
       
        }
        public void UpdateStatus(Atividade modelo)
        {         
                db.Attach(modelo);
                db.Entry(modelo).Property("dataFechamento").IsModified = true;              
                db.Entry(modelo).Property("statusAtividade").IsModified = true;
                db.Entry(modelo).Property("modificadoPor").IsModified = true;
                db.Entry(modelo).Property("modificadoPorName").IsModified = true;
                db.Entry(modelo).Property("modificadoEm").IsModified = true;
                db.SaveChanges();
      
        }
        public Atividade Get(Guid id)
        {
            List<Atividade> retorno = new List<Atividade>();

            retorno = (from A in db.Atividades where A.id.Equals(id) select A).ToList();

            return retorno[0];
        }
        public List<Atividade> GetAll(Guid idOrg)
        {
            List<Atividade> retorno = new List<Atividade>();
 
            retorno = (from A in db.Atividades where A.idOrganizacao.Equals(idOrg) select A).ToList();
            
            return retorno;
        }

        public List<AtividadeGridViewModel> GetAllGridViewModel(Guid idOrg, int view, string idProfissional)
        {
            List<Atividade> itens = new List<Atividade>();

            
            //Minhas Atividades Abertas
            if (view == 0)
            { 
                itens = (from A in db.Atividades where A.idProfissional.Equals(idProfissional) & A.statusAtividade == CustomEnumStatus.StatusAtividade.Aberta select A).ToList();
            }

            //Minhas Atividades Fechadas
            if (view == 1)
            {              
                itens = (from A in db.Atividades where A.idProfissional.Equals(idProfissional) & (A.statusAtividade == CustomEnumStatus.StatusAtividade.Cancelada || A.statusAtividade == CustomEnumStatus.StatusAtividade.Concluida) select A).ToList();
            }

            //Atividades Fechadas
            if (view == 2)
            {               
                itens = (from A in db.Atividades where A.idOrganizacao.Equals(idOrg) & (A.statusAtividade == CustomEnumStatus.StatusAtividade.Cancelada || A.statusAtividade == CustomEnumStatus.StatusAtividade.Concluida) select A).ToList();
            }

            //Todas Atividades
            if (view == 3)
            {
                itens = (from A in db.Atividades where A.idOrganizacao.Equals(idOrg) select A).ToList();
            }

            return HelperAssociate.ConvertToGridAtividade(itens);
        }
        public List<AtividadeGridViewModel> GetAllGridDia( string idProfissional)
        {
            List<Atividade> itens = new List<Atividade>();
   
            itens = (from A in db.Atividades where A.idProfissional.Equals(idProfissional) & A.dataAlvo == DateTime.Now.Date select A).ToList();

            return HelperAssociate.ConvertToGridAtividade(itens);
        }
    }
}
