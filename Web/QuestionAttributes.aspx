<%@ Page Language="C#" EnableSessionState="True" AutoEventWireup="true" CodeBehind="QuestionAttributes.aspx.cs" Inherits="Web.QuestionAttributes" %>

<%@ Register Assembly="CustomNumericUpDown" Namespace="CustomNumericUpDown" TagPrefix="cc1" %>





<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style41 {
            width: 99%;
            height: 22px;
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
            width: 100%;
            position: static;
            overflow: visible;
        }

        .auto-style45 {
            position: static;
            width: 100%;
            border-right-width: thin;
        }

        .auto-style47 {
            border-color: #000000;
            border-width: thin;
            position: static;
            height: 18px;
            width: 99%;
        }

        .auto-style48 {
            position: static;
            width: 100%;
        }

        .auto-style49 {
            position: fixed;
            width: 60px;
            right: 8px;
            bottom: 10px;
            overflow: hidden;
        }

        .auto-style51 {
            position: fixed;
            width: 60px;
            bottom: 10px;
            right: 80px;
        }

        .auto-style53 {
            position: static;
            width: 100%;
        }

        .auto-style61 {
            position: absolute;
            top: 100px;
            left: 570px;
        }
        .auto-style62 {
            height: 24px;
            width: 100%;
        }
        .QuestionStyle {
            padding: 1px 0 1px 0;
            border-color: #000000;
            border-width: thin;
            position: static;
            min-width: 250px;
            width: 99%;
            height: 58px;
        }
    </style>
    <script src="//code.jquery.com/jquery-2.1.3.min.js" type="text/javascript"></script>
</head>
<body>
    <script>
        function RedFunction(TextBox) {
            var $Textbox = $(TextBox);
            if ($Textbox.val() == '') {
                $Textbox.css('border-color', 'red');
            }
            else {
                $Textbox.css('border-color','black');
            }
        }
        function RedColor(TextBox) {
            $Textbox = $(TextBox);
             $Textbox.css('border-color', 'red');
        }
    </script>
    <form id="form1" runat="server" class="auto-style41" style="height: 280px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>

        <div dir="ltr" style="height: 80%; width: 100%;">
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <table class="auto-style43">
                        <tr>
                            <td class="auto-style45">
                                <asp:TextBox ID="question_box" runat="server" TextMode="MultiLine" ToolTip="Write question here " placeholder="Write a question here" TabIndex="1" Style="border-radius:4px; box-sizing:border-box;resize:none;" onblur="RedFunction(this)" CssClass="QuestionStyle"></asp:TextBox>
                                <asp:CustomValidator ID="Question_Validator" runat="server" ControlToValidate="question_box" CssClass="auto-style61" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red" Style="z-index: 1; margin-left: 11px; position: static" ValidationGroup="Required" OnServerValidate="ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style53">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                <asp:TextBox ID="QuestionOrder_Textbox" runat="server" CssClass="auto-style47" TextMode="Number"  ToolTip="Select question order"  style="border-radius:4px; min-width: 250px;" TabIndex="2" AutoPostBack="True" OnTextChanged="QuestionOrder_Textbox_TextChanged" placeholder="Select question order" onblur="RedFunction(this)"></asp:TextBox>
                                <asp:CustomValidator ID="Order_Validator" runat="server" ControlToValidate="QuestionOrder_Textbox" CssClass="auto-style61" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red" Style="z-index: 1; margin-left: 10px; position: static" ValidationGroup="Required" OnServerValidate="ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                           
                                </ContentTemplate>
                                    <Triggers >
                                        <asp:AsyncPostBackTrigger ControlID="QuestionOrder_Textbox" EventName="TextChanged" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                    </td>
                        </tr>
                        <tr>
                            <td class="auto-style62" >
                                <asp:DropDownList ID="QuestionType_DropDownList" runat="server" CssClass="auto-style41" Style="border-color: #000000; border-radius:4px; box-sizing:content-box; border-width: thin; z-index: 1; height: 20px; min-width: 250px;" Width="99%"  AutoPostBack="True" TabIndex="3"  OnSelectedIndexChanged="QuestionType_DropDownList_SelectedIndexChanged" ToolTip="Select question type">
                                    <asp:ListItem>Slider</asp:ListItem>
                                    <asp:ListItem>Smiley</asp:ListItem>
                                    <asp:ListItem>Stars</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; position: static;">
                                <asp:TextBox ID="Shared_textbox" runat="server" CssClass="auto-style47" onblur="RedFunction(this)" TextMode="Number" TabIndex="4" placeholder="Start Value" ToolTip="Select Start Value"  style="border-radius:4px; min-width: 250px;" ></asp:TextBox>
                                <asp:CustomValidator ID="Shared_Validator" runat="server" ControlToValidate="Shared_textbox" CssClass="auto-style61" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red" Style="z-index: 1; margin-left: 10px; position: static" ValidationGroup="Required" ValidateEmptyText="True" OnServerValidate="ServerValidate"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style48">
                                <asp:TextBox ID="End_textBox" runat="server"  style="border-radius:4px; min-width: 250px;"  CssClass="auto-style47" TabIndex="5" onblur="RedFunction(this)" ToolTip="Select End Value" TextMode="Number" placeholder="End Value"></asp:TextBox>
                                <asp:CustomValidator ID="End_Validator" runat="server" ControlToValidate="End_textBox" CssClass="auto-style61" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red" Style="z-index: 1; margin-left: 10px; position: static" ValidationGroup="Required" ValidateEmptyText="True" OnServerValidate="ServerValidate"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style53">
                                <asp:TextBox ID="Start_caption_textBox" runat="server" CssClass="auto-style47"  onblur="RedFunction(this)" TabIndex="6"  style="border-radius:4px; min-width: 250px;"  ToolTip="Write Start Caption" placeholder="Start Caption"></asp:TextBox>
                                <asp:CustomValidator ID="Start_Caption_Validator" runat="server" ControlToValidate="Start_caption_textBox" CssClass="auto-style61" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red" Style="z-index: 1; margin-left: 10px; position: static" ValidationGroup="Required" ValidateEmptyText="True" OnServerValidate="ServerValidate"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="position: static; width: 100%;">
                                <asp:TextBox ID="End_caption_textBox" runat="server"  style="border-radius:4px; min-width: 250px;"  CssClass="auto-style47"  onblur="RedFunction(this)" TabIndex="7" ToolTip="Write End Caption" placeholder="End Caption"></asp:TextBox>
                                <asp:CustomValidator ID="End_Caption_Validator" runat="server" ControlToValidate="End_caption_textBox" CssClass="auto-style61" Display="Dynamic" ErrorMessage="CustomValidator" ForeColor="Red" Style="z-index: 1; margin-left: 10px; position: static" ValidationGroup="Required" ValidateEmptyText="True" OnServerValidate="ServerValidate"></asp:CustomValidator>
                            </td>
                        </tr>
                    </table>
            <%--    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="question_box" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>--%>
            <asp:Button ID="SaveButton" runat="server" TabIndex="8" onblur="RedFunction(this)" Text="Save" ToolTip="Click to save question" Width="60px" Height="27px" CssClass="auto-style51" ValidationGroup="Required" />
            <asp:Button ID="CancelButton" runat="server" Height="27px" onblur="RedFunction(this)" Text="Cancel" Width="60px" CssClass="auto-style49" TabIndex="9" />
        </div>
    </form>
</body>
</html>
