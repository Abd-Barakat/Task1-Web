<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form1.aspx.cs" Inherits="Web.Form1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

        <script>
           function Delete_confir() {
                if (index != -1 && Row_Count != 0) {
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
            </script>
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
            height: 138px;
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

        .auto-style10 {
            position: absolute;
            top: 282px;
            left: 36px;
            z-index: 1;
        }
    </style>
</head>
<body>

        
    <form id="Main_form" runat="server">
       

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
            <br />
            <br />
            <br />
        </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="auto-style1" EnableTheming="True">
            <input id="AddButton" type="button" value="Add" class="auto-style7 " onclick="Add_dialog()" />
            <script type="text/javascript">
                function Add_dialog() {
                    window.open("Add_dialog.aspx", "_blank", false);
                    window.location.replace("Form1.aspx");
                }
                function Edit_dialog() {
                    if (index != -1 &&  Row_Count !=0) {
                         window.open("Edit_dialog.aspx", "_blank", false);
                    }
                    else if (Row_Count == 0)
                    {
                          alert("Database is empty !!");
                    }
                    else {
                        alert("No question selected !!");
                    }
                }
                          var Row_Count = <%=Row_count%>;

                var index =<%=Row_index%>;
            </script>
          

            &nbsp;<input id="EditButton" class="auto-style8" type="button" value="Edit" onclick="Edit_dialog()" aria-disabled="False"  />
            <asp:Button ID="DeleteButton" runat="server" CssClass="auto-style10" Height="28px" OnClick="DeleteButton_Click" OnClientClick=" return Delete_confir()" Text="Delete" Width="57px" />
        
         
            &nbsp;
        </asp:Panel >
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" CssClass="auto-style5" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnLoad="GridView1_Load" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Question text">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("question_text") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Height="50px" />
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="auto-style6" Style="z-index: 1" Text='<%# Eval("question_text") %>'></asp:Label>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <PagerTemplate>
                <asp:Label ID="Label2" runat="server" CssClass="auto-style6" Style="z-index: 1"></asp:Label>
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
