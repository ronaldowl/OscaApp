﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportRenderNativo.aspx.cs" Inherits="OscaReport.ReportRenderNativo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <rsweb:ReportViewer ID="ReportViewer2" runat="server">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
