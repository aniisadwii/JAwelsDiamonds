<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionDetail.aspx.cs" 
    Inherits="JAwelsDiamonds.Views.Customer.TransactionDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Order Details #<asp:Label ID="lblTransactionID" runat="server"></asp:Label></h1>
        
        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="JewelName" HeaderText="Item" />
                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        
        <asp:HyperLink ID="lnkBack" runat="server" Text="Back to My Orders" 
            NavigateUrl="~/Views/Customer/MyOrders.aspx"></asp:HyperLink>
    </form>
</body>
</html>