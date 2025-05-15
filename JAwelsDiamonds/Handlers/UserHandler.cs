using JAwels.Repositories;
using JAwelsDiamonds.Models;
using System;
using System.Text.RegularExpressions;

namespace JAwels.Handlers
{
    public class UserHandler
    {
        public static string CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return "Email must be filled.";
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Invalid email format.";
            if (UserRepository.GetUserByEmail(email) != null)
                return "Email already exists.";
            return "";
        }

        public static string CheckUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return "Username must be filled.";
            if (username.Length < 3 || username.Length > 25)
                return "Username must be between 3 and 25 characters.";
            return "";
        }

        public static string CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Password must be filled.";
            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]{8,20}$"))
                return "Password must be alphanumeric and 8 to 20 characters.";
            return "";
        }

        public static string CheckConfirmPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return "Confirm password must match password.";
            return "";
        }

        public static string CheckGender(string gender)
        {
            if (gender != "Male" && gender != "Female")
                return "Gender must be Male or Female.";
            return "";
        }

        public static string CheckDOB(DateTime dob)
        {
            if (dob >= new DateTime(2010, 1, 1))
                return "Date of birth must be earlier than 01/01/2010.";
            return "";
        }

        public static MsUser DoLogin(string email, string password)
        {
            MsUser user = UserRepository.GetUserByEmail(email);
            if (user == null || user.UserPassword != password) // Ganti dengan hash password di produksi
                return null;
            return user;
        }

        public static string DoRegister(string email, string username, string password, string confirmPassword, string gender, DateTime dob)
        {
            string response = CheckEmail(email);
            if (response == "") response = CheckUsername(username);
            if (response == "") response = CheckPassword(password);
            if (response == "") response = CheckConfirmPassword(password, confirmPassword);
            if (response == "") response = CheckGender(gender);
            if (response == "") response = CheckDOB(dob);

            if (response == "")
            {
                MsUser user = new MsUser
                {
                    UserEmail = email,
                    UserName = username,
                    UserPassword = password, // Ganti dengan hash password di produksi
                    UserGender = gender,
                    UserDOB = dob,
                    UserRole = "Customer"
                };
                UserRepository.AddUser(user);
            }
            return response;
        }
    }
}