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
    public partial class ReportRenderAdm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ReportParameter p = new

                ReportParameter("org", Request.QueryString["org"]);


                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("ronaldowl-001", "Rvnc123...", "ifc"); // e.g.: ("demo-001", "123456789", "ifc")
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;               
                ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://sql5030.site4now.net/ReportServer");
                ReportViewer1.ServerReport.ReportPath = "/ronaldowl-001/Desenv/Administrativo/Usuarios_Organizacao"; //e.g.: /demo-001/test
                ReportViewer1.SizeToReportContent = true;
                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { p });
                ReportViewer1.ServerReport.Refresh();
            }
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