<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="MessageReceiverWebService.Message" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 361px; margin-bottom: 15px; width: 1002px;">
    <form id="form1" runat="server">
        <div style="height: 359px">
            <div>
                <asp:Label ID="lbl_Error" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
            </div>
            <div style="margin-bottom: 20px">
                <asp:Label ID="lbl_ReceivedMessage" runat="server" Text="Received Messages" Font-Size="Large" Style="margin-left: 0px"></asp:Label>
                <asp:Button ID="button_Refresh" runat="server" Text="Refresh" Style="margin-left: 300px; margin-top: 50px" OnClick="button_Refresh_Click" Width="118px" />
            </div>
            <asp:GridView ID="grid_Messages" runat="server" DataKeyNames="RowID" BackColor="Black" BorderColor="#CC9966" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Width="952px" Height="100px" AutoGenerateColumns="False" OnRowDeleting="grid_Messages_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="DateTime" HeaderText="DateTime" DataFormatString="{0: yyyy-MM-dd HH:mm:ss}" />
                    <asp:BoundField DataField="Message" HeaderText="Message" />
                    <asp:CommandField AccessibleHeaderText="Delete" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
