using OscaApp.Data;
using OscaApp.ViewModels;
using OscaFramework.MicroServices;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.RulesServices
{
    public class OrgConfigRules
    {
        public static bool OrgConfigCreate(OrgConfigViewModel entrada, out OrgConfig modelo, ContextPage contexto)
        {
            modelo = new OrgConfig();
            modelo = entrada.orgConfig;

            SqlGeneric sqlServic = new SqlGeneric();


            if (modelo.idOrganizacao != null)
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

        public static bool OrgConfigUpdate(OrgConfigViewModel entrada, out OrgConfig modelo)
        {
            modelo = new OrgConfig();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.orgConfig;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        }
    }
}
