<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="JAwelsDiamonds.Views.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <title>Profile Page</title>
    <style type="text/css">
        .profile-container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }

        .profile-info {
            margin-bottom: 30px;
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

        .password-section {
            margin-top: 30px;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .alert {
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
        }

        .alert-error {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }

        .alert-success {
            background-color: #d4edda;
            color: #155724;
            border: 1px solid #c3e6cb;
        }

        .btn {
            padding: 8px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .text-box {
            padding: 6px 12px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            width: 250px;
        }
    </style>
    <div class="profile-container">
        <h1>Profile Information</h1>

        <div class="profile-info">
            <div class="form-group">
                <asp:Label ID="NameLbl" runat="server" Text="Name: "></asp:Label>
                <asp:Label ID="NameValue" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="EmailLbl" runat="server" Text="Email: "></asp:Label>
                <asp:Label ID="EmailValue" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="DOBLbl" runat="server" Text="Date of Birth: "></asp:Label>
                <asp:Label ID="DOBValue" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="GenderLbl" runat="server" Text="Gender: "></asp:Label>
                <asp:Label ID="GenderValue" runat="server"></asp:Label>
            </div>
        </div>

        <div class="password-section">
            <h2>Change Password</h2>

            <asp:Label ID="ErrorLbl" runat="server" CssClass="alert" Visible="false"></asp:Label>

            <div class="form-group">
                <asp:Label ID="OldPwLbl" runat="server" Text="Old Password:" AssociatedControlID="OldPwTb"></asp:Label>
                <asp:TextBox ID="OldPwTb" runat="server" TextMode="Password" CssClass="text-box"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="NewPwLbl" runat="server" Text="New Password:" AssociatedControlID="NewPwTb"></asp:Label>
                <asp:TextBox ID="NewPwTb" runat="server" TextMode="Password" CssClass="text-box"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="ConfirmPwLbl" runat="server" Text="Confirm Password:" AssociatedControlID="ConfirmPwTb"></asp:Label>
                <asp:TextBox ID="ConfirmPwTb" runat="server" TextMode="Password" CssClass="text-box"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="ChangePwBtn" runat="server" Text="Change Password"
                    CssClass="btn btn-primary" OnClick="ChangePwBtn_Click" />
            </div>
        </div>
    </div>
</asp:Content>
