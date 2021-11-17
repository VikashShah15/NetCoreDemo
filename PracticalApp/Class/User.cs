using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.Class
{
    public class User
    {
        public User(string UserName, string TokenKey)
        {
            userName = UserName;            
            tokenKey = TokenKey;
        }
        public string userName { get; set; }      
        public string tokenKey { get; set; }
    }
}
