using JAwels.Repositories;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAwelsDiamonds.Factory
{
    public class UserFactory
    {
        public static MsUser GetUser(int userId)
        {
            return UserRepository.GetUserById(userId);
        }

        public static void ChangePassword(int userId, string newPassword)
        {
            UserRepository.UpdatePassword(userId, newPassword);
        }
    }
}