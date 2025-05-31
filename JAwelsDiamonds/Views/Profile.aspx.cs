using JAwels.Repositories;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace JAwelsDiamonds.Views
{
    public partial class Profile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Views/Guest/Login.aspx");
                    return;
                }

                LoadUserProfile();
            }
        }

        private void LoadUserProfile()
        {
            try
            {
                int userId = (int)Session["UserID"];
                MsUser user = UserRepository.GetUserById(userId);

                if (user != null)
                {
                    NameValue.Text = user.UserName;
                    EmailValue.Text = user.UserEmail;
                    DOBValue.Text = user.UserDOB?.ToShortDateString() ?? "Not specified";
                    GenderValue.Text = user.UserGender ?? "Not specified";
                }
                else
                {
                    ShowError("User data not found.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Failed to load profile data. Please try again later.");
            }
        }

        protected void ChangePwBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Views/Guest/Login.aspx");
                return;
            }

            string oldPw = OldPwTb.Text.Trim();
            string newPw = NewPwTb.Text.Trim();
            string confirmPw = ConfirmPwTb.Text.Trim();

            ErrorLbl.Visible = false;

            if (string.IsNullOrEmpty(oldPw) || string.IsNullOrEmpty(newPw) || string.IsNullOrEmpty(confirmPw))
            {
                ShowError("All password fields are required.");
                return;
            }

            int userId = (int)Session["UserID"];
            MsUser user = UserRepository.GetUserById(userId);

            if (user == null)
            {
                ShowError("User not found.");
                return;
            }

            if (user.UserPassword != oldPw)
            {
                ShowError("Old password is incorrect.");
                return;
            }

            if (!IsPasswordValid(newPw))
            {
                ShowError("New password must be 8-25 characters long and contain both letters and numbers.");
                return;
            }

            if (newPw != confirmPw)
            {
                ShowError("New password and confirmation password do not match.");
                return;
            }

            try
            {
                UserRepository.UpdatePassword(userId, newPw);
                ShowSuccess("Password changed successfully!");

                OldPwTb.Text = "";
                NewPwTb.Text = "";
                ConfirmPwTb.Text = "";
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while changing password. Please try again.");
            }
        }

        private bool IsPasswordValid(string password)
        {
            return password.Length >= 8 &&
                   password.Length <= 25 &&
                   Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,25}$");
        }

        private void ShowError(string message)
        {
            ErrorLbl.Text = message;
            ErrorLbl.CssClass = "alert alert-error";
            ErrorLbl.Visible = true;
        }

        private void ShowSuccess(string message)
        {
            ErrorLbl.Text = message;
            ErrorLbl.CssClass = "alert alert-success";
            ErrorLbl.Visible = true;
        }
    }
}