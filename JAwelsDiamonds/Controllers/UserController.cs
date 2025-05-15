using JAwels.Handlers;
using JAwelsDiamonds.Models;
using System;
using System.Web;

namespace JAwels.Controllers
{
    public class UserController
    {
        public static string CheckEmail(string email)
        {
            return UserHandler.CheckEmail(email);
        }

        public static string CheckPassword(string password)
        {
            return UserHandler.CheckPassword(password);
        }

        public static string CheckUser(string email, string password)
        {
            return UserHandler.DoLogin(email, password) == null ? "Invalid email or password." : "";
        }

        public static string DoLogin(string email, string password)
        {
            return CheckUser(email, password);
        }


        public static MsUser UserSession(string email, string password)
        {
            return UserHandler.DoLogin(email, password);
        }

        public static HttpCookie UserCookies(MsUser user, bool rememberMe)
        {
            if (!rememberMe) return null;
            HttpCookie cookie = new HttpCookie("user_cookie");
            cookie.Values["Email"] = user.UserEmail;
            cookie.Values["Token"] = GenerateAuthToken(user);
            //cookie.Expires = DateTime.Now.AddDays(30);
            cookie.Expires = DateTime.Now.AddMinutes(1);
            cookie.HttpOnly = true; 
            cookie.Secure = true; 
            return cookie;
        }

        private static string GenerateAuthToken(MsUser user)
        {
            // Implementasi sederhana, ganti dengan JWT atau token lain di produksi
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user.UserEmail + DateTime.Now.Ticks));
        }



        public static string DoRegister(string email, string username, string password, string confirmPassword, string gender, DateTime dob)
        {
            return UserHandler.DoRegister(email, username, password, confirmPassword, gender, dob);
        }
    }
}