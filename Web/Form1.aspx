<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form1.aspx.cs" Inherits="Web.Form1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 126px;
            height: 491px;
            position: absolute;
            top: 3px;
            left: 686px;
            z-index: 1;
        }
        .auto-style2 {
            position: absolute;
            top: 143px;
            left: 36px;
            z-index: 1;
        }
        .auto-style3 {
            position: absolute;
            top: 272px;
            left: 36px;
            z-index: 1;
            width: 57px;
            height: 28px;
        }
        .auto-style4 {
            position: absolute;
            top: 206px;
            left: 36px;
            z-index: 1;
            height: 25px;
        }
        .auto-style5 {
            width: 661px;
            height: 380px;
            position: absolute;
            top: 52px;
            left: 18px;
            z-index: 1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="auto-style1" EnableTheming="True">
            <asp:Button ID="AddButton" runat="server" CssClass="auto-style2" Text="Add" height="28px" width="57px" OnClick="AddButton_Click" />
            <asp:Button ID="DeleteButton" runat="server" CssClass="auto-style3" Text="Delete" />
            <asp:Button ID="EditButton" runat="server" CssClass="auto-style4" height="28px" Text="Edit" width="57px" />
        </asp:Panel>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="auto-style5" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </form>
</body>
</html>
