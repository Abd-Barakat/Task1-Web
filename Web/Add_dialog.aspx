<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_dialog.aspx.cs" Inherits="Web.Add_dialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 144px;
            height: 125px;
            position: absolute;
            top: 62px;
            left: 628px;
            z-index: 1;
        }
        .auto-style4 {
            width: 107px;
            height: 95px;
            position: absolute;
            top: 22px;
            left: 19px;
            z-index: 1;
        }
        .auto-style5 {
            width: 480px;
            height: 60px;
            position: absolute;
            top: 65px;
            left: 13px;
            z-index: 1;
            font-size: larger;
        }
        .auto-style24 {
            position: absolute;
            top: 268px;
            left: 13px;
            z-index: 1;
            width: 759px;
            color: #000000;
        }
        Table_size {
            width: 766px;
            height: 31px;
        }
        .textBox {
            width: 50px;
            height: 30px;
        }
        .textboooox {
            width: 125px;
            height: 25px;
        }
        style {
            width: 125px;
            height: 20px;
        }
        style {
            width: 120px;
            height: 22px;
        }
        we {
            width: 120px;
            height: 22px;
        }
        .auto-style34 {
            width: 783px;
            height: 28px;
            position: absolute;
            top: 376px;
            left: 10px;
        }
        .auto-style35 {
            width: 195px;
            height: 23px;
            text-align: left;
        }
        .auto-style19 {
            width: 195px;
            height: 23px;
            color: #000000;
        }
        .auto-style36 {
            text-align: left;
        }
        .auto-style37 {
            position: absolute;
            top: 466px;
            left: 711px;
            z-index: 1;
            width: 74px;
        }
        .auto-style38 {
            position: absolute;
            top: 467px;
            left: 711px;
            z-index: 1;
            width: 88px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       
        <div class="auto-style36">
       
        <asp:Panel ID="Question_typePanel" runat="server" CssClass="auto-style3" GroupingText="Question type">
            <asp:RadioButtonList ID="Question_types" runat="server" CssClass="auto-style4" OnSelectedIndexChanged="Question_types_SelectedIndexChanged" AutoPostBack="True" TabIndex="2" >
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
        <asp:GridView ID="GridView1" runat="server" CssClass="auto-style5"  ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None"   HeaderStyle-Width ="160px"  HeaderStyle-Height="16px" OnRowCreated="GridView1_RowCreated"  >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField></asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="true" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:TextBox ID="questionTextbox" runat="server" CssClass="auto-style24" TabIndex="1"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <table class="auto-style34" align="left" style="z-index: 1">
                <tr>
                    <td class="auto-style35">
                        <asp:TextBox ID="StartTextbox" runat="server" CssClass="we" style="z-index: 1; color: #000000;" Visible ="false"  placeholder="Start =0"   TabIndex="3"></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:TextBox ID="EndTextbox" runat="server" CssClass="we"  Visible ="false"  placeholder ="End =100" TabIndex="4" ></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:TextBox ID="Start_captionTextbox" runat="server" CssClass="we" style="z-index: 1; color: #000000;"  Visible ="false"  placeholder ="Start caption =20" TabIndex="5"></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:TextBox ID="End_captionTextbox" runat="server" CssClass="we" style="z-index: 1; color: #000000;"  Visible ="false" placeholder="End caption =80"  TabIndex="6"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <p>
                &nbsp;</p>
        </div>
       
    <p>
        <asp:Button ID="CloseButton" runat="server" CssClass="auto-style38" height="26px" OnClick="CloseButton_Click" Text="Close" Visible="False" width="74px" TabIndex="7" />
        </p>
    <p>
        <asp:Button ID="SaveButton" runat="server" CssClass="auto-style37" OnClick="Save_Click" Text="Save" TabIndex="6" Visible="False" />
        </p>
       
    </form>
    </body>
</html>
