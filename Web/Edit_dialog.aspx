<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_dialog.aspx.cs" Inherits="Web.Edit_dialog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style5 {
            width: 480px;
            height: 60px;
            position: absolute;
            top: 15px;
            left: 10px;
            z-index: 1;
            font-size: larger;
        }
        .auto-style24 {
            position: absolute;
            top: 268px;
            left: 13px;
            z-index: 1;
            width: 759px;
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
            width: 769px;
            height: 28px;
            position: absolute;
            top: 376px;
            left: 10px;
            z-index: 1;
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
        .auto-style38 {
            position: absolute;
            top: 457px;
            left: 706px;
            z-index: 1;
            width: 74px;
            height: 26px;
        }
        .auto-style39 {
            text-align: center;
            position: absolute;
            top: 457px;
            left: 706px;
            z-index: 1;
            width: 74px;
            right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       
        <div class="auto-style36">

            <asp:GridView ID="GridView1" runat="server" CssClass="auto-style5"  ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None"   HeaderStyle-Width ="160px"  HeaderStyle-Height="16px" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Question text">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("question_text") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Question order">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("question_order") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Question type">
                    <EditItemTemplate>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("question_type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
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
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <asp:TextBox ID="questionTextbox" runat="server"  CssClass="auto-style24"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <table class="auto-style34" align="left">
                <tr>
                    <td class="auto-style35">
                        <asp:TextBox ID="StartTextbox" runat="server" CssClass="we" style="z-index: 1; color: #000000;"></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:TextBox ID="EndTextbox" runat="server" CssClass="we"  Visible ="false"  ></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:TextBox ID="Start_captionTextbox" runat="server" CssClass="we" style="z-index: 1; color: #000000;"  Visible ="false"  ></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:TextBox ID="End_captionTextbox" runat="server" CssClass="we" style="z-index: 1; color: #000000;"  Visible ="false" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <p>
                &nbsp;

            </p>
             <script  type="text/javascript">
            function RefreshParent() {
                if (window.opener != null && !window.opener.closed)
                    window.opener.location.replace("Form1.aspx");
                window.close();
                 }
                 var visablilty =<%=Visablilty %>
        </script>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
                    <asp:Button ID="SaveButton" runat="server" CssClass="auto-style39" OnClick="Save_Click" Text="Save" TabIndex="6" />

                        <input id="CloseButton"    type="button"  onclick="RefreshParent()" value="Close" style="display:none" class="auto-style38"   />
          


        </div>
       
    </form>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
