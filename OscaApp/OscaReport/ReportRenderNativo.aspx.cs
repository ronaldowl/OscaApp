using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OscaReport
{
    public partial class ReportRenderNativo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                ReportParameter p = new ReportParameter("org", Request.QueryString["org"]);
                string nome = Request.QueryString["nome"];


                ReportViewer2.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("ronaldowl-001", "Rvnc123...", "ifc");
                ReportViewer2.ServerReport.ReportServerCredentials = irsc;
                ReportViewer2.ServerReport.ReportServerUrl = new Uri("http://sql5030.site4now.net/ReportServer");

                ReportViewer2.ServerReport.ReportPath = "/ronaldowl-001" + RetornaAmbiente() + "/NATIVO/" + nome;


                ReportViewer2.SizeToReportContent = true;
                ReportViewer2.ServerReport.SetParameters(new ReportParameter[] { p });
                ReportViewer2.ServerReport.Refresh();
            }
            
        }
        public string RetornaAmbiente()
        {
            string ambiente = "";

            System.Configuration.Configuration rootWebConfig1 = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/dev");
            if (0 < rootWebConfig1.AppSettings.Settings.Count)
            {
                System.Configuration.KeyValueConfigurationElement customSetting =
                    rootWebConfig1.AppSettings.Settings["ambiente"];
                ambiente = customSetting.Value;
            }

            return ambiente;
        }
        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }
    }
}