<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_Page.Master" CodeBehind="Add_dialog.aspx.cs" Inherits="Web.Add_dialog" %>
<%@ MasterType VirtualPath="~/Master_Page.Master" %>
<asp:Content ID ="RadioButtonContent"  ContentPlaceHolderID="RadioButtonsPlaceHolder" runat="server" >
     
    <asp:Panel ID="QuestionTypePanel" runat="server" style="z-index: 1; width: 166px; height: 135px; position: absolute; top: 45px; left: 73px; margin-left: 0px;" BorderStyle="None" Direction="LeftToRight" GroupingText="Question type">
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
        <br />
    </asp:Panel>
     
</asp:Content>


      
     
<asp:Content ID="Content1" runat="server" contentplaceholderid="SaveButtonPlaceHolder">
    <asp:Button ID="SaveButton" runat="server" CssClass="auto-style38" OnClick="Save_Click" style="z-index: 1; position: relative; top: -0; left: 586; width: 83px; height: 25px;" Text="Save" />
<asp:Button ID="CancelButton" runat="server" height="25px" OnClick="CancelButton_Click" style="z-index: 1; position: absolute; top: -0; left: 693px" Text="Cancel" width="83px" />
<br />
<br />
<br />
</asp:Content>



      
     
