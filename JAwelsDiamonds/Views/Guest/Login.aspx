<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JAwels.Views.Guest.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .login-container {
            --primary-color: #2c3e50;
            --primary-dark: #1abc9c;
            --text-color: #ffffff;
            --light-gray: #e67e22;
            --border-color: #bdc3c7;
            --error-color: #e74c3c;
        }

        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 80vh;
            padding: 20px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .login-card {
            background: white;
            padding: 2.5rem;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
        }

        .login-title {
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
            width: 93%;
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

        .remember-me {
            display: flex;
            align-items: center;
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

    <div class="login-container">
        <div class="login-card">
            <h2 class="login-title">Login</h2>
            
            <asp:Label ID="lblError" runat="server" CssClass="error-message" Text=""></asp:Label>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtEmail" runat="server" CssClass="form-label">Email</asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-input" placeholder="Enter your email"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Label AssociatedControlID="txtPassword" runat="server" CssClass="form-label">Password</asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-input" placeholder="Enter your password"></asp:TextBox>
            </div>
            
            <div class="form-group remember-me">
                <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-checkbox" Text="Remember me" />
            </div>
            
            <div class="form-actions">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                <a href="Register.aspx" class="btn btn-link">Don't have an account? Register</a>
            </div>
        </div>
    </div>
</asp:Content>