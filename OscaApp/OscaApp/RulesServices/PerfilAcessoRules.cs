using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaApp.ViewModels;
using OscaFramework.Models;
using OscaApp.Data;
using OscaFramework.MicroServices;

namespace OscaApp.RulesServices
{
    public class PerfilAcessoRules
    {
        public static bool PerfilAcessoCreate(PerfilAcessoViewModel entrada, out PerfilAcesso modelo, ContextPage contexto)
        {
            modelo = new PerfilAcesso();
            modelo = entrada.perfilAcesso;

            SqlGeneric sqlService = new SqlGeneric();
           
            if (modelo.nome != null)
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
            } // end of if 

            return false;

        }  

        public static bool PerfilAcessoUpdate(PerfilAcessoViewModel entrada, out PerfilAcesso modelo)
        {
            modelo = new PerfilAcesso();
            modelo = entrada.perfilAcesso;
            //************ Objetos de controle de acesso *******************
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.Contexto.idUsuario;
            modelo.modificadoPorName = entrada.Contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        } 

    }  
} // end of namespace OscaApp.RulesServices
