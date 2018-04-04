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
    public partial class ReportRenderPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ReportParameter p = new          ReportParameter("id", Request.QueryString["id"]);
                int tipo =  Convert.ToInt32(Request.QueryString["tipo"]);


                ReportViewer4.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("ronaldowl-001", "Rvnc123...", "ifc"); 
                ReportViewer4.ServerReport.ReportServerCredentials = irsc;               
                ReportViewer4.ServerReport.ReportServerUrl = new Uri("http://sql5030.site4now.net/ReportServer");

                if(tipo == 1)ReportViewer4.ServerReport.ReportPath = "/ronaldowl-001"+ RetornaAmbiente() + "/NATIVO/FICHAATENDIMENTO"; 

                if (tipo == 2) ReportViewer4.ServerReport.ReportPath = "/ronaldowl-001" + RetornaAmbiente() + "/NATIVO/IMPRESSAOPEDIDO"; 

                if (tipo == 3) ReportViewer4.ServerReport.ReportPath = "/ronaldowl-001" + RetornaAmbiente() + "/NATIVO/IMPRESSAOOS"; 

                ReportViewer4.SizeToReportContent = true;
                ReportViewer4.ServerReport.SetParameters(new ReportParameter[] { p });
                ReportViewer4.ServerReport.Refresh();
            }
        }
        public string RetornaAmbiente()
        {
            string ambiente = "";

            System.Configuration.Configuration rootWebConfig1 =   System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/dev");
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