<%@ Page Language="C#" EnableSessionState="True" AutoEventWireup="true" CodeBehind="QuestionAttributes.aspx.cs" Inherits="Web.QuestionAttributes" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style41 {
            padding: 0 2px 0 2px;
            width: 99%;
            height: 24px;
            position: static;
        }

        .SaveButton {
            position: relative;
            width: 60px;
            left: 545px;
        }

        .Button_Style {
            position: relative;
            top: 95%;
            left: 75%;
        }

        .auto-style43 {
            width: 97%;
            position: fixed;
            overflow: visible;
        }

        .auto-style45 {
            position: static;
            width: 99%;
        }

        .auto-style47 {
            padding: 1px;
            position: static;
            height: 18px;
            width: 99%;
        }

        .auto-style48 {
            position: static;
            width: 99%;
        }

        .auto-style49 {
            position: fixed;
            width: 60px;
            right: 8px;
            bottom: 10px;
            overflow: hidden;
        }

        .auto-style51 {
            position: absolute;
            width: 60px;
            bottom: 10px;
            right: 80px;
        }

        .auto-style52 {
            position: absolute;
            top: 99px;
            left: 3px;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style41" style="height: 350px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div dir="ltr"  style="height:80%;width:100%;">
            <table class="auto-style43">
                <tr>
                    <td class="auto-style45">
                        <asp:TextBox ID="question_box" runat="server" TextMode="MultiLine" ToolTip="Write question here " Width="99%" Height="58px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="QuestionOrder_Textbox" runat="server" CssClass="auto-style47" Width="99%" TextMode="Number" ToolTip="Select question order"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="QuestionType_DropDownList" runat="server" AutoPostBack="True" CssClass="auto-style41" style="z-index: 1;box-sizing:content-box" Width="99%">
                            <asp:ListItem>Slider</asp:ListItem>
                            <asp:ListItem>Smiley</asp:ListItem>
                            <asp:ListItem>Stars</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="Shared_textbox" runat="server" CssClass="auto-style47" TextMode="Number" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style48">
                        <asp:TextBox ID="End_textBox" runat="server" CssClass="auto-style47" TabIndex="4" ToolTip="Enter end value" Width="99%" TextMode="Number"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="Start_caption_textBox" runat="server" CssClass="auto-style47" TabIndex="5" ToolTip="Enter start caption" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="End_caption_textBox" runat="server" CssClass="auto-style47" TabIndex="6" ToolTip="Enter end caption" Width="99%"></asp:TextBox>

                    </td>
                </tr>
            </table>
            <asp:Button ID="SaveButton" runat="server" TabIndex="7" Text="Save" ToolTip="Click to save question" Width="60px" Height="27px" CssClass="auto-style51"  />
            <asp:Button ID="CancelButton" runat="server" Height="27px" Text="Cancel" Width="60px" CssClass="auto-style49" />
        </div>
    </form>
</body>
</html>
