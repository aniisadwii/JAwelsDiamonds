<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HandleOrders.aspx.cs" Inherits="JAwelsDiamonds.Views.Admin.HandleOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .orders-container {
            max-width: 1000px;
            margin: 20px auto;
            padding: 20px;
        }

        .orders-header {
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 1px solid #ddd;
        }

        .orders-grid {
            width: 100%;
            border-collapse: collapse;
        }

            .orders-grid th {
                background-color: #f8f9fa;
                padding: 12px;
                text-align: left;
                border-bottom: 2px solid #ddd;
            }

            .orders-grid td {
                padding: 10px;
                border-bottom: 1px solid #ddd;
            }

            .orders-grid tr:nth-child(even) {
                background-color: #f9f9f9;
            }

        .action-button {
            padding: 5px 10px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }

        .confirm-button {
            background-color: #2c3e50;
            color: white;
        }

        .ship-button {
            background-color: #2c3e50;
            color: white;
        }

        .waiting-text {
            color: #7f8c8d;
            font-style: italic;
        }

        .message {
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
            text-align: center;
        }

        .success-message {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .error-message {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
    </style>

    <div class="orders-container">
        <div class="orders-header">
            <h1>Handle Unfinished Orders</h1>
            <asp:Label ID="MessageLabel" runat="server" CssClass="message" Visible="false"></asp:Label>
        </div>

        <asp:GridView ID="OrdersGridView" runat="server" AutoGenerateColumns="False"
            CssClass="orders-grid" EmptyDataText="No unfinished orders found" OnRowDataBound="OrdersGridView_RowDataBound">
            <Columns>
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" />
                <asp:BoundField DataField="UserID" HeaderText="User ID" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="StatusLabel" runat="server"
                            CssClass='<%# ((JAwelsDiamonds.Views.Admin.HandleOrders)Page).GetStatusCssClass(Eval("TransactionStatus").ToString()) %>'
                            Text='<%# Eval("TransactionStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:PlaceHolder ID="ActionPlaceholder" runat="server"></asp:PlaceHolder>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
