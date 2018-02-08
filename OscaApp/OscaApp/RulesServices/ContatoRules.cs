using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class ContatoRules
    {
        public static bool MontaContatoCreate(ContatoViewModel entrada,out Contato modelo, ContextPage contexto )
        {
            modelo = new Contato (); 
            
           
            if (entrada.contexto.idOrganizacao != null)
            {
                modelo = entrada.contato;
              
                ////************ Objetos de controle de acesso ***************
                modelo.criadoEm         = DateTime.Now;
                modelo.criadoPor        = contexto.idUsuario;
                modelo.criadoPorName    = contexto.nomeUsuario;
                modelo.modificadoEm     = DateTime.Now;
                modelo.modificadoPor    = contexto.idUsuario;
                modelo.modificadoPorName = contexto.nomeUsuario;
                ////************ FIM Objetos de controle de acesso ***************

                modelo.idOrganizacao    = contexto.idOrganizacao;              

                return true;
            }           
            
            return false;
        }
        public static bool MontaContatoUpdate(ContatoViewModel entrada,out Contato modelo)
        {
            modelo = new Contato();

            ////************ Objetos de controle de acesso ***************
            modelo = entrada.contato;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************       

            return true;           
        }
        
    }
}
