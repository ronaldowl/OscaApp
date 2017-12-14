using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OscaApp.RulesServices
{
    public static class OrganizacaoRules 
    {

        public static bool CriaNovaOrganizacao(string org, Guid idUsuario )
        {

            SqlGenericManager conexao = new SqlGenericManager();
                

            conexao.CriaOrganizacao(org);


            return false;
        }
    }
}
