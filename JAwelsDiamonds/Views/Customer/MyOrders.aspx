<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="JAwelsDiamonds.Views.Customer.MyOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Orders</title>
    <style>
        .order-table {
            width: 100%;
            border-collapse: collapse;
        }

            .order-table th, .order-table td {
                border: 1px solid #ddd;
                padding: 8px;
                text-align: left;
            }

            .order-table th {
                background-color: #f2f2f2;
            }

        .action-buttons {
            display: flex;
            gap: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>My Orders</h1>

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass="order-table"
            OnRowCommand="gvOrders_RowCommand" OnRowDataBound="gvOrders_RowDataBound"
            DataKeyNames="TransactionID">
            <Columns>
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
                <asp:BoundField DataField="TransactionStatus" HeaderText="Status" />

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <div class="action-buttons">
                            <asp:Button ID="btnDetails" runat="server" Text="View Details"
                                CommandName="ViewDetails" CommandArgument='<%# Eval("TransactionID") %>' />

                            <asp:Panel ID="pnlArrivedActions" runat="server" Visible='<%# Eval("TransactionStatus").ToString() == "Arrived" %>'>
                                <asp:Button ID="btnConfirm" runat="server" Text="Confirm"
                                    CommandName="Confirm" CommandArgument='<%# Eval("TransactionID") %>'
                                    OnClientClick="return confirm('Confirm receipt of this package?');"
                                    CssClass="btn-confirm" />

                                <asp:Button ID="btnReject" runat="server" Text="Reject"
                                    CommandName="Reject" CommandArgument='<%# Eval("TransactionID") %>'
                                    OnClientClick="return confirm('Reject this package?');"
                                    CssClass="btn-reject" />
                            </asp:Panel>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    </form>
</body>
</html>
