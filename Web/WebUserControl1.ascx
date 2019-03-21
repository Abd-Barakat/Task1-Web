<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="Web.WebUserControl1" %>
<style type="text/css">
    .auto-style1 {
        position: absolute;
        top: 14px;
        left: 10px;
    }

    .auto-style2 {
        position: static;
        top: 25px;
        left: 139px;
        z-index: 1;
        width: 25px;
        height: 11px;
    }

    .auto-style3 {
        position: static;
        top: 0px;
        left: 139px;
        z-index: 1;
        height: 11px;
        width: 25px;
    }
    .auto-style4 {
        width: 693px;
        height: 22px;
        position: absolute;
        top: 15px;
        left: 10px;
        z-index: 1;
    }
</style>

    <asp:Panel ID="Panel1" runat="server" CssClass="auto-style4">
    <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style1" Style="z-index: 1; position: static;"></asp:TextBox>
    <asp:ImageButton ID="DownButton" runat="server" CssClass="auto-style2" ImageUrl="~/ArrowDown.png" />
    <asp:ImageButton ID="UpButton" runat="server" CssClass="auto-style3" ImageUrl="~/ArrowUp.png" />
        <br />
        <br />

</asp:Panel>



