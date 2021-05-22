using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestsBackend.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "TestsBsuApp";
        public const string AUDIENCE = "TestsBsuClient";
        const string KEY = "GIVE_US_TEN_POINTS_PLEASE";
        public const int LIFETIME = 999999999;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
