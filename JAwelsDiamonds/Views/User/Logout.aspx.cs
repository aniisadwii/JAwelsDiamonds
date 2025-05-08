using System;
using System.Web;

namespace JAwels.Views.User
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            HttpCookie cookie = Request.Cookies["user_cookie"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
                System.Diagnostics.Debug.WriteLine("Cookie deleted");
            }
            else
            {
                cookie = new HttpCookie("user_cookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
                System.Diagnostics.Debug.WriteLine("Empty cookie created and deleted");
            }
            Response.Redirect("~/Views/Home.aspx");
        }
    }
}