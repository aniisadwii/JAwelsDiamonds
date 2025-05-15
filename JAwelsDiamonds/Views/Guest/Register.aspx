<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="JAwels.Views.Guest.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .register-container {
            --primary-color: #2c3e50;
            --primary-dark: #1abc9c;
            --text-color: #ffffff;
            --light-gray: #e67e22;
            --border-color: #bdc3c7;
            --error-color: #e74c3c;
        }

        .register-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 80vh;
            padding: 20px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .register-card {
            background: white;
            padding: 2.5rem;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 450px;
        }

        .register-title {
            color: var(--primary-color);
            text-align: center;
            margin-bottom: 1.5rem;
            font-size: 1.8rem;
            font-weight: 600;
        }

        .form-group {
            margin-bottom: 1.2rem;
        }

        .form-label {
            display: block;
            margin-bottom: 0.5rem;
            color: var(--text-color);
            font-weight: 500;
        }

        .form-input {
            width: 100%;
            padding: 0.75rem;
            border: 1px solid var(--border-color);
            border-radius: 4px;
            font-size: 1rem;
            transition: border-color 0.3s;
        }

        .form-input:focus {
            outline: none;
            border-color: var(--primary-color);
            box-shadow: 0 0 0 2px rgba(52, 152, 219, 0.2);
        }

        .radio-group {
            display: flex;
            gap: 1rem;
            margin-top: 0.5rem;
        }

        .radio-option {
            display: flex;
            align-items: center;
        }

        .radio-option input {
            margin-right: 0.5rem;
        }

        .date-input {
            width: 100%;
        }

        .btn {
            display: inline-block;
            padding: 0.75rem 1.5rem;
            border-radius: 4px;
            font-size: 1rem;
            font-weight: 500;
            text-align: center;
            cursor: pointer;
            transition: all 0.3s;
        }

        .btn-primary {
            background-color: var(--primary-color);
            color: white;
            border: none;
        }

        .btn-primary:hover {
            background-color: var(--primary-dark);
        }

        .btn-link {
            color: var(--primary-color);
            text-decoration: none;
            margin-left: 1rem;
        }

        .btn-link:hover {
            text-decoration: underline;
        }

        .error-message {
            display: block;
            color: var(--error-color);
            margin-bottom: 1rem;
            text-align: center;
        }

        .form-actions {
            margin-top: 1.5rem;
            text-align: center;
        }
    </style>

    <div class="register-container">
        <div class="register-card">
            <h2 class="register-title">Register</h2>
            
            <asp:Label ID="lblError" runat="server" CssClass="error-message" Text=""></asp:Label>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtEmail" runat="server" CssClass="form-label">Email</asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-input" placeholder="Enter your email"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtUsername" runat="server" CssClass="form-label">Username</asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-input" placeholder="Choose a username"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtPassword" runat="server" CssClass="form-label">Password</asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-input" placeholder="Create a password"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtConfirmPassword" runat="server" CssClass="form-label">Confirm Password</asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-input" placeholder="Confirm your password"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Label runat="server" CssClass="form-label">Gender</asp:Label>
                <div class="radio-group">
                    <div class="radio-option">
                        <asp:RadioButton ID="rbMale" runat="server" GroupName="Gender" />
                        <asp:Label AssociatedControlID="rbMale" runat="server">Male</asp:Label>
                    </div>
                    <div class="radio-option">
                        <asp:RadioButton ID="rbFemale" runat="server" GroupName="Gender" />
                        <asp:Label AssociatedControlID="rbFemale" runat="server">Female</asp:Label>
                    </div>
                </div>
            </div>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtDOB" runat="server" CssClass="form-label">Date of Birth</asp:Label>
                <asp:TextBox ID="txtDOB" runat="server" TextMode="Date" CssClass="form-input date-input"></asp:TextBox>
            </div>
            
            <div class="form-actions">
                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn btn-primary" />
                <a href="Login.aspx" class="btn-link">Already have an account? Login</a>
            </div>
        </div>
    </div>
</asp:Content>