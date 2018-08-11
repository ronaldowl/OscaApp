using Microsoft.AspNetCore.Mvc;
using OscaApp.Data;
using OscaApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using OscaFramework.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using OscaFramework.MicroServices;
using System.Collections.Generic;
using OscaApp.ViewModels.GridViewModels;

namespace OscaApp.Controllers
{
    public class ReportController : Controller
    {
        public IConfiguration Configuration { get; }
        public IConfigurationSection sessao { get; }
        public string urlAmbiente { get; set; }
        private ContextPage contexto;

        public ReportController(IHttpContextAccessor httpContext)
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

        public ViewResult GridReports()
        {
            ReportGridViewModel reports = new ReportGridViewModel();
            SqlGenericData sqlData = new SqlGenericData();

            List<Report> reportsNativo = new List<Report>();
            List<Report> reportsCustom = new List<Report>();


            reportsNativo = sqlData.RetornaReportsNativo();
            reportsCustom = sqlData.RetornaReportsCustom(this.contexto.idOrganizacao);          
            reports.reportsNativo = reportsNativo;

            if(reportsCustom.Count > 0)
            {
                reports.reportsCustomizado = reportsCustom;
            }


            return View(reports);
        }

        public ViewResult OpenReport(string nome)
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
    }
}
