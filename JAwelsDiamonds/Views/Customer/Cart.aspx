<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="JAwelsDiamonds.Views.Customer.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
        }

        .cart-container {
            max-width: 800px;
            margin: 0 auto;
        }

        .cart-item {
            display: grid;
            grid-template-columns: 2fr 1fr 1fr 1fr 1fr;
            padding: 10px;
            border-bottom: 1px solid #ddd;
            align-items: center;
        }

        .cart-header {
            font-weight: bold;
            background-color: #f5f5f5;
        }

        .quantity-input {
            width: 50px;
            text-align: center;
        }

        .btn-update {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            margin-left: 5px;
        }

        .btn-remove {
            background-color: #f44336;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
        }

        .error-message {
            color: red;
            font-size: 12px;
        }
    </style>

    <div class="cart-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h1>Your Shopping Cart</h1>

                <asp:Panel ID="pnlEmptyCart" runat="server" Visible="false">
                    <p>Your cart is empty.</p>
                </asp:Panel>

                <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
                    <HeaderTemplate>
                        <div class="cart-item cart-header">
                            <span>Item</span>
                            <span>Price</span>
                            <span>Quantity</span>
                            <span>Total</span>
                            <span>Action</span>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="cart-item">
                            <span><%# Eval("JewelName") %></span>
                            <span><%# Eval("Price", "{0:C}") %></span>
                            <span>
                                <asp:TextBox ID="txtQuantity" runat="server"
                                    Text='<%# Eval("Quantity") %>'
                                    CssClass="quantity-input"
                                    TextMode="Number" min="1" />
                                <asp:RegularExpressionValidator runat="server"
                                    ControlToValidate="txtQuantity"
                                    ValidationExpression="^[1-9]\d*$"
                                    ErrorMessage="Must be ≥1"
                                    CssClass="error-message"
                                    Display="Dynamic" />
                            </span>
                            <span><%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("C") %></span>
                            <span>
                                <asp:Button ID="btnUpdate" runat="server"
                                    CommandName="Update"
                                    CommandArgument='<%# Eval("JewelID") %>'
                                    Text="Update"
                                    CssClass="btn-update" />
                                <asp:Button ID="btnRemove" runat="server"
                                    CommandName="Remove"
                                    CommandArgument='<%# Eval("JewelID") %>'
                                    Text="Remove"
                                    CssClass="btn-remove" />
                            </span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <div style="margin-top: 20px; text-align: right;">
                    <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="true"></asp:Label>
                </div>

                <asp:Panel ID="pnlCheckout" runat="server" Visible="false">
                    <div style="margin-top: 20px;">
                        <h3>Checkout</h3>
                        <asp:Label runat="server" Text="Payment Method:" />
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server" Required="true">
                            <asp:ListItem Text="-- Select --" Value="" Selected="True" />
                            <asp:ListItem Text="Credit Card" Value="Credit Card" />
                            <asp:ListItem Text="Bank Transfer" Value="Bank Transfer" />
                            <asp:ListItem Text="PayPal" Value="PayPal" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPaymentMethod"
                            ErrorMessage="Please select payment method" ForeColor="Red" />

                        <asp:Button ID="btnCheckout" runat="server" Text="Complete Checkout"
                            OnClick="btnCheckout_Click" CssClass="btn-checkout" />
                    </div>
                </asp:Panel>

                <asp:Label ID="lblMessage" runat="server" Visible="false" Style="margin-top: 10px; display: block;"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
