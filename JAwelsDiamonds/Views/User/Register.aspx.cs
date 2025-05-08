using JAwels.Controllers;
using System;

namespace JAwels.Views.User
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                Response.Redirect("~/Views/Home.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string gender = rbMale.Checked ? "Male" : rbFemale.Checked ? "Female" : "";
            DateTime dob;
            if (!DateTime.TryParse(txtDOB.Text, out dob))
            {
                lblError.Text = "Invalid date format.";
                return;
            }

            string response = UserController.DoRegister(email, username, password, confirmPassword, gender, dob);
            if (response == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                lblError.Text = response;
            }
        }
    }
}