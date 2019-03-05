<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.secure.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 338px;
            height: 224px;
            position: absolute;
            top: 15px;
            left: 10px;
            z-index: 1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Login ID="Login1" runat="server" CssClass="auto-style1" OnAuthenticate="Login1_Authenticate">
            </asp:Login>
        </div>
    </form>
</body>
</html>
