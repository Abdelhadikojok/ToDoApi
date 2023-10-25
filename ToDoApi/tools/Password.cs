using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text; // Add this using directive to access Encoding

namespace ToDoApi.tools
{
    public class Password
    {
        public static string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hash = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hash);
        }
    }
}
 