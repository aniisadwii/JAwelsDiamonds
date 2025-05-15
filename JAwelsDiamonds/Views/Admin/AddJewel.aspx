<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddJewel.aspx.cs" Inherits="JAwelsDiamonds.Views.Admin.AddJewel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Jewel</title>
    <style type="text/css">
        .form-container {
            max-width: 500px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .error-message {
            color: red;
            font-size: 0.9em;
        }
        .button-group {
            margin-top: 20px;
            text-align: center;
        }
        .button-group .btn {
            margin: 0 10px;
            padding: 8px 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Add New Jewel</h2>
            
            <div class="form-group">
                <asp:Label ID="NameLbl" runat="server" Text="Jewel Name" AssociatedControlID="NameTb"></asp:Label>
                <asp:TextBox ID="NameTb" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="NameMessage" runat="server" CssClass="error-message"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="CategoryLbl" runat="server" Text="Category" AssociatedControlID="CatDdl"></asp:Label>
                <asp:DropDownList ID="CatDdl" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Select Category --</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="CategoryMessage" runat="server" CssClass="error-message"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="BrandLbl" runat="server" Text="Brand" AssociatedControlID="BrandDdl"></asp:Label>
                <asp:DropDownList ID="BrandDdl" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Select Brand --</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="BrandMessage" runat="server" CssClass="error-message"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="PriceLbl" runat="server" Text="Price ($)" AssociatedControlID="PriceTb"></asp:Label>
                <asp:TextBox ID="PriceTb" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                <asp:Label ID="PriceMessage" runat="server" CssClass="error-message"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Label ID="ReleaseYearLbl" runat="server" Text="Release Year" AssociatedControlID="ReleaseTb"></asp:Label>
                <asp:TextBox ID="ReleaseTb" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                <asp:Label ID="ReleaseYearMessage" runat="server" CssClass="error-message"></asp:Label>
            </div>

            <div class="button-group">
                <asp:Button ID="AddBtn" runat="server" Text="Add Jewel" CssClass="btn btn-primary" OnClick="AddBtn_Click"/>
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="CancelBtn_Click" CausesValidation="false"/>
            </div>
            <asp:Label ID="GeneralMessage" runat="server" ForeColor="Red" Visible="false" />

            <asp:Label ID="SuccessMessage" runat="server" CssClass="text-success" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>