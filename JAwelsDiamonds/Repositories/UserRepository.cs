using JAwelsDiamonds.Models;
using System.Linq;

namespace JAwels.Repositories
{
    public class UserRepository
    {
        private static DatabaseEntities1 db = new DatabaseEntities1(); 

        public static MsUser GetUserByEmail(string email)
        {
            return db.MsUsers.FirstOrDefault(u => u.UserEmail == email);
        }

        public static void AddUser(MsUser user)
        {
            db.MsUsers.Add(user);
            db.SaveChanges();
        }

        public static MsUser GetUserById(int userId)
        {
            return db.MsUsers.FirstOrDefault(u => u.UserID == userId);
        }

        public static void UpdatePassword(int userId, string newPassword)
        {
            var user = db.MsUsers.FirstOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                user.UserPassword = newPassword;
                db.SaveChanges();
            }
        }


    }
}