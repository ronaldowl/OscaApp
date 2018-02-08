using Microsoft.AspNetCore.Http;
using OscaApp.framework.Models;
using System;
using System.Net.Http;
using OscaFramework.Models;

namespace OscaApp.Data
{
    public class ContextPage
    {
        public String organizacao { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idUsuario { get; set; }
        public String nomeUsuario { get; set; }
     

        //Carrega todas as informações da sessão da pagina
        public ContextPage(string Email, string Org)
        {
            //Prenche informações do Contexto
            SqlGenericManager sqlmanager = new SqlGenericManager();

            Relacao RL = sqlmanager.RetornaContextPage(Email, Org);
            RL.tipoObjeto = CustomEntityEnum.Entidade.Usuario;

            this.idOrganizacao = RL.idOrganizacao;
            this.idUsuario = RL.id;
            this.organizacao = RL.organizacao;
            this.nomeUsuario = RL.idName;
        }

        public ContextPage(string Email, string Org, Guid id, CustomEntityEnum.Entidade Entidade)
        {
            //Prenche informações do Contexto
            SqlGenericManager sqlmanager = new SqlGenericManager();

            Relacao RL = sqlmanager.RetornaContextPage(Email, Org);
            RL.tipoObjeto = CustomEntityEnum.Entidade.Usuario;

            this.idOrganizacao = RL.idOrganizacao;
            this.idUsuario = RL.id;
            this.organizacao = RL.organizacao;
            this.nomeUsuario = RL.idName;
         
        }
    }
}
