using Lingkail.VMS.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lingkail.VMS
{
    public class TokenProvider
    {
        public string LoginUser(string UserID, string Password)
        {
            //Get user details for the user who is trying to login - JRozario
            var user = UserList.SingleOrDefault(x => x.USERID == UserID);

            //Authenticate User, Check if its a registered user in DB  - JRozario
            if (user == null)
                return null;

            //If its registered user, check user password stored in DB - JRozario
            //For demo, password is not hashed. Its just a string comparison - JRozario
            //In reality, password would be hashed and stored in DB. Before comparing, hash the password - JRozario
            if (Password == user.PASSWORD)
            {
                //Authentication successful, Issue Token with user credentials - JRozario
                //Provide the security key which was given in the JWToken configuration in Startup.cs - JRozario
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                //Generate Token for user - JRozario
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:45092/",
                    audience: "http://localhost:45092/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token - JRozario
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        //Using hard coded collection list as Data Store for demo purpose. In reality, User data comes from Database or some other Data Source - JRozario
        private List<user> UserList = new List<user>
        {
            new user { USERID = "weiwei2", PASSWORD = "test", EMAILID = "cheahwei.leow@gmail.com", FIRST_NAME = "Cheah Wei", LAST_NAME = "Leow", PHONE = "0127239729", ACCESS_LEVEL = "Director", READ_ONLY = "false" },
            new user { USERID = "claudia", PASSWORD = "test", FIRST_NAME = "Claudia", LAST_NAME = "Huang", EMAILID = "claudia.huang@lingkail.com", PHONE = "0146890738", ACCESS_LEVEL = "Supervisor", READ_ONLY = "false" },
            new user { USERID = "asmidar", PASSWORD = "test", FIRST_NAME = "Nurul", LAST_NAME = "Asmidar", EMAILID = "nurul.asmidar@lingkail.com", PHONE = "0174622584", ACCESS_LEVEL = "Analyst", READ_ONLY = "false" },
            new user { USERID = "yap", PASSWORD = "test", FIRST_NAME = "Chun Teen", LAST_NAME = "Yap", EMAILID = "yap@senatraffic.com.my", PHONE = "0166814131", ACCESS_LEVEL = "Analyst", READ_ONLY = "true" }
        };

        //Using hard coded collection list as Data Store for demo. In reality, User data comes from Database or some other Data Source - JRozario
        private IEnumerable<Claim> GetUserClaims(user user)
        {
            IEnumerable<Claim> claims = new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.FIRST_NAME + " " + user.LAST_NAME),
                    new Claim("USERID", user.USERID),
                    new Claim("EMAILID", user.EMAILID),
                    new Claim("PHONE", user.PHONE),
                    new Claim("ACCESS_LEVEL", user.ACCESS_LEVEL.ToUpper()),
                    new Claim("READ_ONLY", user.READ_ONLY.ToUpper())
                    };
            return claims;
        }
    }
}
