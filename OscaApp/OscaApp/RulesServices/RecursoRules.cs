using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class RecursoRules
    {
        public static bool RecursoCreate(RecursoViewModel entrada,out Recurso modelo, ContextPage contexto )
        {
            modelo = new Recurso();
            modelo = entrada.recurso;

            SqlGeneric sqlServic = new SqlGeneric();
            modelo.codigo = sqlServic.RetornaNovaPosicao(22,contexto.idOrganizacao);
            
 
            if (modelo.codigo != null)
            {
                //************ Objetos de controle de acesso ******************
                modelo.criadoEm = DateTime.Now;
                modelo.criadoPor = contexto.idUsuario;
                modelo.criadoPorName = contexto.nomeUsuario;
                modelo.modificadoEm = DateTime.Now;
                modelo.modificadoPor = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                modelo.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }

        public static bool RecursoUpdate(RecursoViewModel entrada, out Recurso modelo)
        {
            modelo = new Recurso();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.recurso;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
