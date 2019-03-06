<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Page.Master" CodeBehind="Edit_dialog.aspx.cs" Inherits="Web.Edit_dialog" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="SaveButtonContent" ContentPlaceHolderID="SaveButtonPlaceHolder" runat="server">

    <asp:Button ID="SaveButton" runat="server" CssClass="auto-style38" OnClick="SaveButton_Click" style="z-index: 1; position: absolute; top: 0px; left: 0px" Text="Save" Visible="False" />

</asp:Content>

<asp:Content  ID="Try" ContentPlaceHolderID="RadioButtons" runat="server" >

    </asp:Content>