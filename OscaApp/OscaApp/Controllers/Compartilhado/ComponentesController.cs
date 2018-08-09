using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using OscaFramework.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using OscaFramework.MicroServices;

namespace OscaApp.Controllers
{
    public class ComponentesController : Controller
    {
        public IConfiguration Configuration { get; }
        public IConfigurationSection sessao { get; }
        public string urlAmbiente { get; set; }
        private ContextPage contexto;

        public ComponentesController(IHttpContextAccessor httpContext)
        {

            var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            this.Configuration = Configuration;
            this.sessao = Configuration.GetSection("Ambiente");
            this.urlAmbiente = sessao.GetValue<string>("valor");

            this.contexto = new ContextPage().ExtractContext(httpContext);
        }

        public ViewResult ReportNativo()
        {
            return View();
        }
        public ViewResult ReportPrint(int tipo, string id)
        {
            Relatorio model = new Relatorio();
            model.idRegistro = id;

            if (tipo == 1) model.nomeReport = "FICHAATENDIMENTO";

            if (tipo == 2) model.nomeReport = "IMPRESSAOPEDIDO";

            if (tipo == 3) model.nomeReport = "IMPRESSAOOS";

            if (urlAmbiente == "desenv")
            {
                model.url = "http://www.report.desenv.oscas.com.br/ReportRenderPrint.aspx?tipo=" + tipo + "&id=" + model.idRegistro;
            }

            if (urlAmbiente == "prod")
            {
                model.url = "http://www.report.oscas.com.br/ReportRenderPrint.aspx?tipo=" + tipo + "&id=" + model.idRegistro;
            }
            return View(model);
        }

        public ViewResult ExportGrid(string nome)
        {
            Relatorio model = new Relatorio();
            model.nomeReport = nome;

            if (urlAmbiente == "desenv")
            {
                model.url = "http://www.report.desenv.oscas.com.br/ReportRenderNativo.aspx?nome=" + nome + "&org=" + this.contexto.idOrganizacao;
            }

            if (urlAmbiente == "prod")
            {
                model.url = "http://www.report.oscas.com.br/ReportRenderNativo.aspx?nome=" + nome + "&org=" + this.contexto.idOrganizacao;
            }
            return View(model);

        }

        [HttpGet]
        public ViewResult VisualizaURLCaptura()
        {
            PaginaClienteViewModel modelo = new PaginaClienteViewModel();

            SqlGenericManager sqlService = new SqlGenericManager();

            Organizacao org = sqlService.RetornaOrganizacao(this.contexto.idOrganizacao);


            if (org.servicoPaginaCapturaLead > 0)
            {

                if (urlAmbiente == "desenv")
                {
                    modelo.urlPaginaCaptura = "http://www.app.desenv.oscas.com.br/paginaCliente/FormularioEntrada?id=" + this.contexto.idOrganizacao.ToString();
                }

                if (urlAmbiente == "prod")
                {
                    modelo.urlPaginaCaptura = "http://www.app.oscas.com.br/paginaCliente/FormularioEntrada?id=" + this.contexto.idOrganizacao.ToString();
                }
            }
            else
            {
                modelo.urlPaginaCaptura = "Adquira o nosso Produto em www.loja.oscas.com.br";

            }

            return View(modelo);
        }

        [HttpGet]
        public ViewResult ResumoFinanceiro()
        {
          

            return View();
        }
    }
}
