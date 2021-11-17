using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PracticalApp.Class;
using PracticalApp.DataObject;
using PracticalApp.Model;

namespace PracticalApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;          
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                //  Class.User user = new Class.User(username, password);

                #region ValidateUser
                UserDataObject cls = new UserDataObject();
                var user = cls.Login(username, password);
                #endregion

                if (user == null)
                {
                    return Ok(new { IsError = 1, Message = "UserName or Password is wrong." });
                }

                if (user.isDeleted)
                {
                    return Ok(new { IsError = 1, Message = "User is Deleted." });
                }

                if (user.IsLocked)
                {
                    return Ok(new { IsError = 1, Message = "User is Locked." });
                }

                #region Add SignIn Details
                Guid guid = Guid.NewGuid();

                var signInDetails = new SignInDetails();                
                signInDetails.StartDate = DateTime.Now;
                signInDetails.EndDate = DateTime.Now.AddMinutes(15);
                signInDetails.GuidId = guid.ToString();
                signInDetails.UserId = user.UserId;
                signInDetails.Attempt = 0;

                SignInDataObject clsSignIn = new SignInDataObject();
                clsSignIn.Insert(signInDetails);
                #endregion

                TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
                var setupInfo =
                    twoFactor.GenerateSetupCode("demoapp", user.UserName, TwoFactorKey(user), false, 3);

                return Ok(new { IsError = 0, SetupCode = setupInfo.ManualEntryKey, BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl, guid = guid });
            }
            catch(Exception ex)
            {
                return Ok(new { IsError = 1, Message = ex.Message });
            }
        }

        [HttpPost("Authorize")]
        public IActionResult Authorize(string inputCode, string guid)
        {
            // User user = new User(); // TODO: fetch signed in user from a database

            #region Get UserId
            SignInDataObject clsSignIn = new SignInDataObject();
            var signInDetails = clsSignIn.GetByGuid(guid);
            #endregion

            if (signInDetails == null)
            {
                return Ok(new { IsError = 1, Message = "Invalid User" });
            }

            #region Get User Details
            UserDataObject cls = new UserDataObject();
            var user = cls.GetUserByUserId(signInDetails.UserId);
            #endregion

            if (user.isDeleted)
            {
                return Ok(new { IsError = 1, Message = "User is Deleted." });
            }

            if (user.IsLocked)
            {
                return Ok(new { IsError = 1, Message = "User is Locked." });
            }

            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            bool isValid = twoFactor.ValidateTwoFactorPIN(TwoFactorKey(user), inputCode);
            if (!isValid)
            {
                #region Update Attempt
                signInDetails.Attempt = signInDetails.Attempt + 1;
                clsSignIn.UpdateAttempt(signInDetails);
                #endregion

                #region Lock the account
                if (signInDetails.Attempt >= 10)
                {
                    cls.LockAccount(signInDetails.UserId);

                    return Ok(new { IsError = 1, Message = "Your Account is locked." });
                }
                #endregion

                return Ok(new { IsError = 1, Message = "Entered Code is wrong." });
            }

            #region Delete the SignInDetails
            clsSignIn.Delete(signInDetails);
            #endregion

            #region Generate Token                           
            Class.CommonClass objCommon = new Class.CommonClass();
            var tokenString = objCommon.GenerateJSONWebToken(user, _config);
            Class.User model = new Class.User(user.UserName, tokenString);                                
            #endregion

            return Ok(new { IsError = 0, Message = "Success", data = model });            
        }

        private static string TwoFactorKey(Model.User user)
        {
            return $"myverysecretkey+{user.UserName}";
        }
    }
}
