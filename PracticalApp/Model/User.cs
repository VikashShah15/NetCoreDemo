using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.Model
{
    [Table("tblUser")]
    public class User
    {
        public User()
        {

        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }    
        public bool isDeleted { get; set; }
        public bool IsLocked { get; set; }
    }
}
