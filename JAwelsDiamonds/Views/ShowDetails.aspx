<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShowDetails.aspx.cs" Inherits="JAwelsDiamonds.Views.ShowDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }

        .jewel-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .jewel-info {
            margin-bottom: 20px;
        }

            .jewel-info h2 {
                color: #333;
                margin-top: 0;
            }

        .price {
            font-size: 24px;
            font-weight: bold;
            color: #c00;
        }

        .btn-add-to-cart {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

            .btn-add-to-cart:hover {
                background-color: #45a049;
            }

        .message {
            padding: 10px;
            margin: 10px 0;
            border-radius: 4px;
        }

        .success {
            background-color: #dff0d8;
            color: #3c763d;
            border: 1px solid #d6e9c6;
        }

        .error {
            background-color: #f2dede;
            color: #a94442;
            border: 1px solid #ebccd1;
        }

        .btn-edit {
            background-color: #2196F3;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

            .btn-edit:hover {
                background-color: #1976D2;
            }


        .btn-delete {
            background-color: #f44336;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }

            .btn-delete:hover {
                background-color: #d32f2f;
            }
    </style>
    <div class="jewel-container">

        <!-- Jewel Information -->
        <div class="jewel-info">
            <h2>
                <asp:Label ID="lblJewelName" runat="server" Text="Jewel Name"></asp:Label></h2>
            <p>
                <strong>Category:</strong>
                <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <strong>Brand:</strong>
                <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <strong>Origin:</strong>
                <asp:Label ID="lblOrigin" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <strong>Class:</strong>
                <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <strong>Release Year:</strong>
                <asp:Label ID="lblReleaseYear" runat="server" Text=""></asp:Label>
            </p>
            <p class="price">$<asp:Label ID="lblPrice" runat="server" Text=""></asp:Label></p>
        </div>

        <!-- Success Message -->
        <asp:Panel ID="pnlSuccess" runat="server" CssClass="message success" Visible="false">
            <asp:Label ID="lblSuccess" runat="server" Text="Item successfully added to cart!"></asp:Label>
        </asp:Panel>

        <!-- Error Message -->
        <asp:Panel ID="pnlError" runat="server" CssClass="message error" Visible="false">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- Add to Cart Button -->
        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn-add-to-cart" OnClick="btnAddToCart_Click" />

        <!-- Admin Buttons -->
        <asp:Panel ID="pnlAdminActions" runat="server" Visible="false">
            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-edit" OnClick="btnEdit_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn-delete" OnClick="btnDelete_Click" />

        </asp:Panel>


        <!-- View Cart Link -->
        <div style="margin-top: 20px;">
            <asp:HyperLink ID="lnkViewCart" runat="server" NavigateUrl="~/Views/Customer/Cart.aspx" Visible="false">
                    View Cart & Checkout
            </asp:HyperLink>
        </div>
    </div>
</asp:Content>
