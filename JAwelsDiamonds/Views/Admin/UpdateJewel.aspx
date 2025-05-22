<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateJewel.aspx.cs" Inherits="JAwelsDiamonds.Views.UpdateJewel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Update Jewel</h2>
            
            <div class="form-group">
                <asp:Label ID="NameLbl" runat="server" Text="Jewel Name" AssociatedControlID="NameTb"></asp:Label>
                <asp:TextBox ID="NameTb" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="CategoryLbl" runat="server" Text="Category" AssociatedControlID="CatDdl"></asp:Label>
                <asp:DropDownList ID="CatDdl" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Select Category --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="BrandLbl" runat="server" Text="Brand" AssociatedControlID="BrandDdl"></asp:Label>
                <asp:DropDownList ID="BrandDdl" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Select Brand --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="PriceLbl" runat="server" Text="Price ($)" AssociatedControlID="PriceTb"></asp:Label>
                <asp:TextBox ID="PriceTb" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="ReleaseYearLbl" runat="server" Text="Release Year" AssociatedControlID="ReleaseTb"></asp:Label>
                <asp:TextBox ID="ReleaseTb" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
            </div>

            <div class="button-group">
                <asp:Button ID="UpdateBtn" runat="server" Text="Add Jewel" CssClass="btn btn-primary" OnClick="UpdateBtn_Click"/>
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="CancelBtn_Click" CausesValidation="false"/>
            </div>
            <asp:Label ID="SuccessMessage" runat="server" CssClass="text-success"></asp:Label>
        </div>
    </form>
</body>
</html>
