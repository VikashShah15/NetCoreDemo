using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.Model
{
    [Table("tblSignInDetails")]
    public class SignInDetails
    {       
        public SignInDetails()
        {
           
        }

        public long id { get; set; }
        public long UserId { get; set; }
        public string GuidId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Attempt { get; set; }
        public bool isDeleted { get; set; }
    }
}
