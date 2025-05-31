<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditJewel.aspx.cs" Inherits="JAwelsDiamonds.Views.Admin.EditJewel" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <title>Update Jewel</title>
    <style type="text/css">
        .form-container {
            max-width: 600px;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .form-group {
            margin-bottom: 15px;
        }

            .form-group label {
                display: inline-block;
                width: 150px;
                font-weight: bold;
            }

        .error-message {
            color: red;
            font-size: 0.9em;
            margin-left: 150px;
        }

        .button-group {
            margin-top: 20px;
            text-align: center;
        }

        .text-box {
            padding: 6px 12px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            width: 250px;
        }

        .success-message {
            color: green;
            font-weight: bold;
            text-align: center;
            margin-bottom: 15px;
        }
    </style>
    <div class="form-container">
        <h2>Update Jewel Information</h2>

        <asp:Label ID="SuccessMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>

        <div class="form-group">
            <asp:Label ID="NameLbl" runat="server" Text="Jewel Name" AssociatedControlID="NameTb"></asp:Label>
            <asp:TextBox ID="NameTb" runat="server" CssClass="text-box"></asp:TextBox>
            <asp:Label ID="NameMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="CategoryLbl" runat="server" Text="Category" AssociatedControlID="CatDdl"></asp:Label>
            <asp:DropDownList ID="CatDdl" runat="server" CssClass="text-box">
                <asp:ListItem Value="">-- Select Category --</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="CategoryMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="BrandLbl" runat="server" Text="Brand" AssociatedControlID="BrandDdl"></asp:Label>
            <asp:DropDownList ID="BrandDdl" runat="server" CssClass="text-box">
                <asp:ListItem Value="">-- Select Brand --</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="BrandMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="PriceLbl" runat="server" Text="Price ($)" AssociatedControlID="PriceTb"></asp:Label>
            <asp:TextBox ID="PriceTb" runat="server" CssClass="text-box" TextMode="Number" step="0.01"></asp:TextBox>
            <asp:Label ID="PriceMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label ID="ReleaseYearLbl" runat="server" Text="Release Year" AssociatedControlID="ReleaseTb"></asp:Label>
            <asp:TextBox ID="ReleaseTb" runat="server" CssClass="text-box" TextMode="Number"></asp:TextBox>
            <asp:Label ID="ReleaseYearMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>


        <div class="button-group">
            <asp:Button ID="UpdateBtn" runat="server" Text="Update Jewel" CssClass="btn btn-primary" OnClick="UpdateBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="CancelBtn_Click" CausesValidation="false" />
        </div>
    </div>
</asp:Content>
