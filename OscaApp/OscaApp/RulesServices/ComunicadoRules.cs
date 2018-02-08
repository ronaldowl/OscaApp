using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ComunicadoRules
    {
        public static bool ComunicadoCreate(ComunicadoViewModel entrada,out Comunicado comunicado, ContextPage contexto )
        {
            comunicado = new Comunicado();
            comunicado = entrada.comunicado;
         
            if (comunicado.titulo != null)
            {
                //************ Objetos de controle de acesso ******************
                comunicado.criadoEm = DateTime.Now;
                comunicado.criadoPor = contexto.idUsuario;
                comunicado.criadoPorName = contexto.nomeUsuario;
                comunicado.modificadoEm = DateTime.Now;
                comunicado.modificadoPor = contexto.idUsuario;
                comunicado.modificadoPorName = contexto.nomeUsuario;
                comunicado.idOrganizacao = contexto.idOrganizacao;
                //************ FIM Objetos de controle de acesso ***************

                return true;
            }                
            return false;
        }
        public static bool ComunicadoUpdate(ComunicadoViewModel entrada,out Comunicado comunicado)
        {
            comunicado = new Comunicado();

            //************ Objetos de controle de acesso *******************
            comunicado = entrada.comunicado;
            comunicado.modificadoEm = DateTime.Now;
            comunicado.modificadoPor = entrada.Contexto.idUsuario;
            comunicado.modificadoPorName = entrada.Contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }       
    }
}
