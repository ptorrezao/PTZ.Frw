using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PTZ.Frw.Utils
{
    public class Crypto
    {
        public static string PreparePassword(string passwordSalt, string password)
        {
            return HashPassword(password, passwordSalt);
        }

        private static string HashPassword(string password, string passwordSalt)
        {
            byte[] salt = Encoding.ASCII.GetBytes(passwordSalt);
            Array.Resize<byte>(ref salt, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password ?? string.Empty, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static string PreparePassword(object passwordSalt, string password)
        {
            throw new NotImplementedException();
        }
    }
}
