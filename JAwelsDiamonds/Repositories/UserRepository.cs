using JAwelsDiamonds.Models;
using System.Linq;

namespace JAwels.Repositories
{
    public class UserRepository
    {
        private static DatabaseEntities db = new DatabaseEntities(); 

        public static MsUser GetUserByEmail(string email)
        {
            return db.MsUsers.FirstOrDefault(u => u.UserEmail == email);
        }

        public static void AddUser(MsUser user)
        {
            db.MsUsers.Add(user);
            db.SaveChanges();
        }
    }
}