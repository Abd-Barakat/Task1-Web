<%@  page Language="C#" AutoEventWireup="true" CodeBehind="Form1.aspx.cs" Inherits="Web.Form1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"   onchange="Page_Load" onfocus="Page_Load"  onblur="Page_Load" >
<head runat="server" >
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 126px;
            height: 491px;
            position: absolute;
            top: 3px;
            left: 686px;
            z-index: 1;
            text-align: center;
        }
        .auto-style5 {
            width: 661px;
            height: 380px;
            position: absolute;
            top: 52px;
            left: 18px;
            z-index: 1;
        }
        .auto-style7 {
            position: absolute;
            top: 158px;
            left: 36px;
            z-index: 1;
            height: 28px;
            width: 57px;
        }
        .auto-style8 {
            position: absolute;
            top: 217px;
            left: 36px;
            z-index: 1;
            width: 57px;
            height: 28px;
        }
        .auto-style9 {
            position: absolute;
            top: 281px;
            left: 36px;
            z-index: 1;
            width: 57px;
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="Main_form" runat="server" >
        <div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:HiddenField ID="OrderMain" runat="server"  />
            <br />
        </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="auto-style1" EnableTheming="True">
            <input id="AddButton" type="button" value="Add" class="auto-style7 " onclick="Add_dialog()" />
            <input type="hidden" id="Order" runat="server" />
            <script type="text/javascript">
                function Add_dialog() {
                    window.open("Add_dialog.aspx", "_blank", false);
                }
                function Edit_dialog() {
                    if (index != -1) {
                        var edit = window.open("Edit_dialog.aspx", "_blank", false);
                        edit.document.getElementById("OrderEdit").value = document.getElementById("OrderMain").value;
                    }
                    else {
                        alert("No question selected !!");
                    }
                }
                var index =<%=Row_index%>;
                </script>
            &nbsp;<input id="EditButton" class="auto-style8" type="button" value="Edit" onclick="Edit_dialog()" />
            <input id="DeleteButton" type="button" value="Delete" class="auto-style9" />
        </asp:Panel>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="auto-style5" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnLoad="GridView1_Load" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Question text">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("question_text") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="auto-style6" style="z-index: 1" Text='<%# Eval("question_text") %>'></asp:Label>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <PagerTemplate>
                <asp:Label ID="Label2" runat="server" CssClass="auto-style6" style="z-index: 1"></asp:Label>
            </PagerTemplate>
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
