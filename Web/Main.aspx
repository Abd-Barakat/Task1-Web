<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Web.Form1" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Survey Configurator
        
    </title>
    <link rel="icon" href="resources/question-mark.png" />
    <style type="text/css">
        .auto-style12 {
            position: static;
            top: 30px;
            z-index: 1;
            width: 90px;
            left: 10px;
        }

        .auto-style14 {
            position: static;
            top: 55px;
            z-index: 1;
            width: 90px;
            left: 10px;
        }

        .auto-style15 {
            width: 100%;
            height: 100%;
            vertical-align: top;
        }

        .auto-style16 {
            font: normal normal 900 large Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
            height: 100%;
            width: 100%;
        }

        .auto-style17 {
            width: 95%;
            vertical-align: top;
            height: 100%;
        }

        .auto-style18 {
            position: static;
            z-index: 1;
            width: 100%;
            height: 100%;
            left: 13px;
        }

        .auto-style19 {
            position: static;
            top: 5px;
            z-index: 1;
            width: 90px;
            left: 10px;
        }
        .modelpopup{
            width:350px;
            height:300px;
            background-color:white;
            border-width:1px;
            padding-right:5px;
            padding-bottom:5px;
            border-radius:4px;
        }
        .modelbackground{
            opacity:0.5;
            background-color:black;
            z-index:20;

        }
        .auto-style21 {
            width: 100%;
            height: 100%;
            position: static;
            left: 0px;
            top: 22px;
        }
        .CloseButton {
            position: fixed;
            width: 18px;
            height: 18px;
            border-width: 0px;
        }
        .auto-style22 {
            position: fixed;
            width: 18px;
            height: 18px;
            border-width: 0px;
            right: 402px;
            top: 203px;
        }
    </style>
</head>
<body style="height: 95vh; width: 98vw">
    <script>
        function Edit_dialog() {

            window.open("QuestionAttributes.aspx", "_blank", false);
            window.location.replace("Main.aspx");

        }
        function Check_Selection() {
            if (Row_Count == 0) {
                alert("Database is empty !!");
                return false;

            }
            else if (Row_Index == -1) {
                alert("No question selected !!");
                return false;

            }
            else
                return true;
        }
        function Add_dialog() {
            window.showModalDialog ("QuestionAttributes.aspx", "_blank", "width=410,height=370,left=400,top=200 ", false);
            window.location.replace("Main.aspx");
        }

    
        }
        function Delete_confir() {
            if (Row_Index != -1 && Row_Count != 0) {
                var result = confirm("Are you sure you want to delete question ?");
                if (result == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (Row_Count == 0) {
                alert("Database is empty !!");
                return false;

            }
            else {
                alert("No question selected !!");
                return false;

            }
        }
    var Row_Count = <%=Row_count%>;
        var Row_Index =<%=Row_index%>;
    </script>

    <form id="Main_form" runat="server" style="height: 100%">
        <div class="auto-style16">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            Question list :<br />
            <table class="auto-style15">
                <tr>
                    <td class="auto-style17">
                        <asp:ListBox ID="DatabaseListBox" runat="server" CssClass="auto-style18" OnLoad="DatabaseListBox_Load" Font-Size="Medium" Font-Strikeout="False" ForeColor="Black" SelectionMode="Multiple" TabIndex="3" ToolTip="Select a question" OnSelectedIndexChanged="DatabaseListBox_SelectedIndexChanged"></asp:ListBox>
                    </td>
                    <td style="vertical-align: top; text-align: left; height: 100%; padding-left: 5px; width: 5%;">
                        <asp:Button ID="AddButton" runat="server" CssClass="auto-style19" Height="28px" Text="Add" ToolTip="Add a question" OnClick="AddButton_Click"  />
                        <br />
                        <br />
                        <asp:Button ID="EditButton" runat="server" CssClass="auto-style12" Height="28px" OnClick="Edit_Button_Click" Text="Edit" ToolTip="Edit the selected question" />
                        <br />
                        <br />
                        <asp:Button ID="DeleteButton" runat="server" CssClass="auto-style14" Height="28px" OnClick="DeleteButton_Click" OnClientClick=" return Delete_confir()" Text="Delete" ToolTip="Delete the selected question" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" Enabled="true"  CssClass="modelpopup" >
<%--                <asp:Button ID="Button1" runat="server" Text="x" BackColor="White" CssClass="auto-style22" ForeColor="#999999" />--%>
                <iframe id="QuestionAttributesFrame"   src="QuestionAttributes.aspx" runat="server" class="auto-style21" > </iframe> 
            </asp:Panel> 
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1"  Enabled="false"  TargetControlID="AddButton" BackgroundCssClass="modelbackground" runat="server" PopupControlID="Panel1" ></ajaxToolkit:ModalPopupExtender>
        </div>

    </form>


</body>
</html>
