using Microsoft.AspNetCore.Http;
using OscaApp.framework.Models;
using System;
using System.Net.Http;
using OscaFramework.Models;
using OscaFramework.MicroServices;

namespace OscaApp.Data
{
    public class ContextPage
    {
        public String organizacao { get; set; }
        public Guid idOrganizacao { get; set; }
        public Guid idUsuario { get; set; }
        public String nomeUsuario { get; set; }
        public string idRelacionado { get; set; }
        public PerfilAcesso perfilAcesso { get; set; }


        //Construtor padrão
        public ContextPage() { }

        //Carrega todas as informações da sessão da pagina
        public ContextPage(string Email, string Org)
        {
            //Prenche informações do Contexto
            SqlGeneric sqlmanager = new SqlGeneric();

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
            SqlGeneric sqlservice = new SqlGeneric();

            Relacao RL = sqlservice.RetornaContextPage(Email, Org);
            RL.tipoObjeto = CustomEntityEnum.Entidade.Usuario;

            this.idOrganizacao = RL.idOrganizacao;
            this.idUsuario = RL.id;
            this.organizacao = RL.organizacao;
            this.nomeUsuario = RL.idName;
         
        }


        /// <summary>
        /// Método para ser usado em todos Controllers como padrão de instanciar o contexto
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public ContextPage ExtractContext(IHttpContextAccessor httpContext)
        {
            ContextPage retorno = new ContextPage();
            try
            {          

            retorno.idOrganizacao =  new Guid(httpContext.HttpContext.Session.GetString("idOrganizacao"));
            retorno.idUsuario = new Guid(httpContext.HttpContext.Session.GetString("idUsuario"));
            retorno.nomeUsuario =  httpContext.HttpContext.Session.GetString("nomeUsuario");
            retorno.organizacao =  httpContext.HttpContext.Session.GetString("organizacao");

            }
            catch (Exception) {}

            return retorno;
        }
    }
}
