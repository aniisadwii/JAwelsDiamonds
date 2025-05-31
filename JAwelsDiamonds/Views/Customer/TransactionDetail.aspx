<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionDetail.aspx.cs"
    Inherits="JAwelsDiamonds.Views.Customer.TransactionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <title>Transaction Details</title>
    <style type="text/css">
        .transaction-container {
            max-width: 800px;
            margin: 20px auto;
            padding: 20px;
        }

        .transaction-header {
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 1px solid #ddd;
        }

        .transaction-grid {
            width: 100%;
            border-collapse: collapse;
        }

            .transaction-grid th {
                background-color: #f8f9fa;
                padding: 10px;
                text-align: left;
                border-bottom: 2px solid #ddd;
            }

            .transaction-grid td {
                padding: 10px;
                border-bottom: 1px solid #ddd;
            }

            .transaction-grid tr:nth-child(even) {
                background-color: #f9f9f9;
            }

        .back-button {
            margin-top: 20px;
        }
    </style>

    <div class="transaction-container">
        <div class="transaction-header">
            <h1>Transaction Details</h1>
            <asp:Label ID="TransactionIdLabel" runat="server" Font-Bold="true"></asp:Label>
        </div>

        <asp:GridView ID="TransactionDetailGV" runat="server" AutoGenerateColumns="False"
            CssClass="transaction-grid" EmptyDataText="No transaction details found">
            <Columns>
                <asp:BoundField DataField="JewelName" HeaderText="Jewel Name" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <div class="transaction-footer">
            <asp:Label ID="TotalLabel" runat="server" Font-Bold="true" Font-Size="Larger"></asp:Label>
            <br />
            <asp:Button ID="BackButton" runat="server" Text="Back to My Orders"
                CssClass="back-button" OnClick="BackButton_Click" />
        </div>
    </div>
</asp:Content>
