using PracticalApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.DataObject
{
    public class UserDataObject
    {
        public User Login(string username, string password)
        {
            User user = new User();
            using (var context = new DemoAppContext())
            {
                user = context.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();                
            }

            return user;
        }

        public User GetUserByUserId(long userId)
        {
            User user = new User();
            using (var context = new DemoAppContext())
            {
                user = context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            }

            return user;
        }

        public long LockAccount(long userId)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                var uID = context.Users.Where(x => x.UserId == userId).FirstOrDefault();
                uID.IsLocked = true;
                Id = context.SaveChanges();
            }

            return Id;
        }
    }
}
