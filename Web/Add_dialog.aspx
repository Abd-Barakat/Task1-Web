<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Page.Master" CodeBehind="Add_dialog.aspx.cs" Inherits="Web.Add_dialog" %>
<%@ MasterType VirtualPath="~/Master_Page.Master" %>
<asp:Content ID ="RadioButtonContent"  ContentPlaceHolderID="RadioButtons" runat="server" >
     
    <asp:Panel ID="QuestionTypePanel" runat="server" style="z-index: 1; width: 166px; height: 106px; position: absolute; top: 18px; left: 23px; margin-left: 0px;" BorderStyle="None" Direction="LeftToRight" GroupingText="Question type">
        <asp:RadioButtonList ID="QuestionType" runat="server" style="z-index: 1; width: 97px; height: 2px; position: absolute; top: 23px; right: 37px; bottom: 14px;" OnSelectedIndexChanged="QuestionType_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>Slider</asp:ListItem>
            <asp:ListItem>Smiley</asp:ListItem>
            <asp:ListItem>Stars</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
     
</asp:Content>


      
     
<asp:Content ID="Content1" runat="server" contentplaceholderid="SaveButtonPlaceHolder">
    <asp:Button ID="SaveButton" runat="server" CssClass="auto-style38" OnClick="Save_Click" style="z-index: 1; position: absolute; top: 0px; left: 0px" Text="Save" />
</asp:Content>



      
     
