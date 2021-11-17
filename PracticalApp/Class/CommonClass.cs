using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PracticalApp.Class
{
    public class CommonClass
    {
        public string GenerateJSONWebToken(PracticalApp.Model.User userInfo, IConfiguration _config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
             //   new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
            //    new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId",userInfo.UserId.ToString() )
            };
            //if (userInfo.expires <= 0)
            //    userInfo.expires = 60;
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
              //  expires: DateTime.Now.AddSeconds(userInfo.expires),
              expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public PracticalApp.Model.User decodeToken(string token)
        {
            var jwt = token;
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(jwt);
            var claims = response.Claims;
            var objClaim = claims.ToArray();
            var user = new PracticalApp.Model.User();
            if (objClaim[0] != null)
            {
                user.UserName = objClaim[0].Value;
            }
            //if (objClaim[1] != null)
            //{
            //    user.EmailAddress = objClaim[1].Value;
            //}
            //if (objClaim[2] != null)
            //{
            //    user.DateOfJoing = Convert.ToDateTime(objClaim[2].Value);
            //}
            if (objClaim[2] != null)
            {
                user.UserId = Convert.ToInt64(objClaim[2].Value);
            }

            return user;        



        }
    }
}
