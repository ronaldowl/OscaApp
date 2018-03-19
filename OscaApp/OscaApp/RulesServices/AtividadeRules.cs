using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class AtividadeRules
    {
        public static bool AtividadeCreate(AtividadeViewModel entrada,out Atividade modelo, ContextPage contexto )
        {
            modelo = new Atividade();
            modelo = entrada.atividade;
            modelo.idProfissional = entrada.profissional.id;

            if (modelo.assunto != null)
            {
                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                ////************ FIM Objetos de controle de acesso ***************

                return true;
            }           
            
            return false;
        }
        public static bool AtividadeUpdate(AtividadeViewModel entrada,out Atividade modelo)
        {

            modelo = new Atividade();
            modelo = entrada.atividade;
            modelo.idProfissional = entrada.profissional.id;

            if (entrada.atividade.statusAtividade == CustomEnumStatus.StatusAtividade.Cancelada || entrada.atividade.statusAtividade == CustomEnumStatus.StatusAtividade.Concluida)
            {
                modelo.dataFechamento = DateTime.Now;
            }    

            ////************ Objetos de controle de acesso ***************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************

            return true;
        }
        public static bool AtividadeUpdateStatus(AtividadeViewModel entrada, out Atividade modelo)
        {

            modelo = new Atividade();
            modelo = entrada.atividade;
            
            if(entrada.atividade.statusAtividade == CustomEnumStatus.StatusAtividade.Cancelada || entrada.atividade.statusAtividade == CustomEnumStatus.StatusAtividade.Concluida)
            {
                modelo.dataFechamento = DateTime.Now;
            }

            if(entrada.atividade.statusAtividade == CustomEnumStatus.StatusAtividade.Aberta)
            {
                modelo.dataFechamento = new DateTime();
            }

            ////************ Objetos de controle de acesso ***************

            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************

            return true;
        }
    }
}
