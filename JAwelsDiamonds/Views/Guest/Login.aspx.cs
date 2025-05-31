using JAwels.Controllers;
using JAwelsDiamonds.Models;
using System;
using System.Web;

namespace JAwels.Views.Guest
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["user_cookie"];
                if (cookie != null && Session["UserID"] == null)
                {
                    string email = cookie["Email"];
                    string password = cookie["Password"];
                    MsUser user = UserController.UserSession(email, password);
                    if (user != null)
                    {
                        Session["UserID"] = user.UserID;
                        Session["UserName"] = user.UserName;
                        Session["UserRole"] = user.UserRole;
                        Response.Redirect("~/Views/Home.aspx");
                    }
                }

                if (Session["UserID"] != null)
                    Response.Redirect("~/Views/Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string response = UserController.DoLogin(email, password);

            if (response == "")
            {
                MsUser user = UserController.UserSession(email, password);
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserRole"] = user.UserRole;

                HttpCookie cookie = UserController.UserCookies(user, chkRememberMe.Checked);
                if (cookie != null)
                    Response.Cookies.Add(cookie);

                    Response.Redirect("~/Views/Home.aspx");
            }
            else
            {
                lblError.Text = response;
            }
        }
    }
}