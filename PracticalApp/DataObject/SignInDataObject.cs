using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalApp.Model;

namespace PracticalApp.DataObject
{
    public class SignInDataObject
    {
        public long Insert(SignInDetails signInDetails)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                context.SignInDetails.Add(signInDetails);
                Id = context.SaveChanges();
            }

            return Id;
        }

        public SignInDetails GetByGuid(string guid)
        {
            var signInDetails = new SignInDetails();
            using (var context = new DemoAppContext())
            {
                signInDetails = context.SignInDetails.Where(x => x.GuidId == guid && x.isDeleted == false && x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).FirstOrDefault();               
            }

            return signInDetails;
        }

        public long UpdateAttempt(SignInDetails signInDetails)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                var sID = context.SignInDetails.Where(x => x.id == signInDetails.id).FirstOrDefault();
                sID.Attempt = signInDetails.Attempt;
                Id = context.SaveChanges();
            }

            return Id;
        }

        public long Delete(SignInDetails signInDetails)
        {
            int Id = 0;
            using (var context = new DemoAppContext())
            {
                var sID = context.SignInDetails.Where(x => x.id == signInDetails.id).FirstOrDefault();
                sID.isDeleted = true;
                Id = context.SaveChanges();
            }

            return Id;
        }
    }
}
