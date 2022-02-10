using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toyota.Auth
{
    public class AuthOptions
    {
        public const String Issuer = "ToyotaAuthServer"; // издатель токена
        public const String Audince = "AuthClient"; // потребитель токена
        public const int Lifetime = 60; // время жизни токена - 1 минута

        private const String key = "1mVKANeW4P6QdyAwNknGOGwie94vffEGJFXhLiqQfJibwSjkutS8tuWE9IYpcgN";   // ключ для шифрации

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}
