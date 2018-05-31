using OscaApp.Data;
using OscaApp.Models;
using OscaFramework.MicroServices;
using OscaFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OscaApp.framework
{
    public class CargaInicial
    {

        public void CreateProfissional(ApplicationUser usuario, ContextPage contexto, ContexDataService dbContext)
        {
            Profissional profissional = new Profissional();
            SqlGeneric sqlservice = new SqlGeneric();

            profissional.codigo = sqlservice.RetornaNovaPosicao(17, contexto.idOrganizacao);
            profissional.nomeProfissional = usuario.nomeAmigavel;
            profissional.idUsuario = new Guid(usuario.Id);

            //************ Objetos de controle de acesso ******************
            profissional.criadoEm = DateTime.Now;
            profissional.criadoPor = contexto.idUsuario;
            profissional.criadoPorName = contexto.nomeUsuario;
            profissional.modificadoEm = DateTime.Now;
            profissional.modificadoPor = contexto.idUsuario;
            profissional.modificadoPorName = contexto.nomeUsuario;
            profissional.idOrganizacao = contexto.idOrganizacao;
            //************ FIM Objetos de controle de acesso ***************

            ProfissionalData profissionalData = new ProfissionalData(dbContext);

            profissionalData.Add(profissional);

        }

    }
}
