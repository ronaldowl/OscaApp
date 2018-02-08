using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.Models;
using OscaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OscaFramework.Models;

namespace OscaApp.RulesServices
{
    public static class OrganizacaoRules 
    {
        public static bool CriaNovaOrganizacao(string org, string idUser )
        {
            SqlGenericManager conexao = new SqlGenericManager();
            conexao.CriaOrganizacao(org, idUser);
            return false;
        }

        public static bool MontaOrganizacaoUpdate(OrganizacaoViewModel entrada, out Organizacao modelo)
        {
            modelo = new Organizacao ();

            ////************ Objetos de controle de acesso ***************
            modelo = entrada.organizacao;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.contexto.idUsuario;
            modelo.modificadoPorName = entrada.contexto.nomeUsuario;
            ////************ FIM Objetos de controle de acesso ***************
            return true;
        }
    }
}
