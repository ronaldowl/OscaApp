using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public static class ProfissionalRules
    {
        public static bool ProfissionalCreate(ProfissionalViewModel entrada,out Profissional profissional, ContextPage contexto )
        {
            profissional = new Profissional();
            profissional = entrada.profissional;

            SqlGeneric sqlservice = new SqlGeneric();

            profissional.codigo = sqlservice.RetornaNovaPosicao(17, contexto.idOrganizacao);
            profissional.idBanco = entrada.banco.id;
            profissional.idUsuario = entrada.usuario.id;
            profissional.nomeProfissional = entrada.usuario.idName;


            if (profissional.idUsuario != null)
            {
                //************ Objetos de controle de acesso ******************
                profissional.criadoEm = DateTime.Now;
                profissional.criadoPor = contexto.idUsuario;
                profissional.criadoPorName = contexto.nomeUsuario;
                profissional.modificadoEm = DateTime.Now;
                profissional.modificadoPor = contexto.idUsuario;
                profissional.modificadoPorName = contexto.nomeUsuario;
                profissional.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool ProfissionalUpdate(ProfissionalViewModel entrada,out Profissional profissional)
        {
            profissional = new Profissional();
            profissional = entrada.profissional;
            profissional.idBanco = entrada.banco.id;
            profissional.idUsuario = entrada.usuario.id;
            profissional.nomeProfissional = entrada.usuario.idName;

            //************ Objetos de controle de acesso *******************

            profissional.modificadoEm = DateTime.Now;
            profissional.modificadoPor = entrada.contexto.idUsuario;
            profissional.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
