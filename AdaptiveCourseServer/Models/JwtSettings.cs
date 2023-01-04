using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdaptiveCourseServer.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public static string Issuer { get; set; }

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public static string Audience { get; set; }

        /// <summary>
        /// Ключ для шифрации
        /// </summary>
        public static string Key { get; set; }

        /// <summary>
        /// Время жизни токена
        /// </summary>
        public static int LifeTime { get; set; }

        /// <summary>
        /// Ключ для шифрации
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
