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
    public class IncidenteRules
    {
        public static bool IncidenteCreate(IncidenteViewModel entrada, out Incidente modelo, ContextPage contexto)
        {
            modelo = new Incidente();
            modelo = entrada.Incidente;

            SqlGeneric sqlService = new SqlGeneric();
            modelo.Codigo = sqlService.RetornaNovaPosicao(11, contexto.idOrganizacao);

            if (modelo.Codigo != null)
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

        } // end of method IncidenteCreate

        public static bool IncidenteUpdate(IncidenteViewModel entrada, out Incidente modelo)
        {
            modelo = new Incidente();

            //************ Objetos de controle de acesso *******************
            modelo = entrada.Incidente;
            modelo.modificadoEm = DateTime.Now;
            modelo.modificadoPor = entrada.Contexto.idUsuario;
            modelo.modificadoPorName = entrada.Contexto.nomeUsuario;
            //************ FIM Objetos de controle de acesso ***************
            return true;
        } // end of method IncidenteUpdate

    } // end of class IncidenteRules
} // end of namespace OscaApp.RulesServices
