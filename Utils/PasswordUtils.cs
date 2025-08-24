using System.Text;
using System.Security.Cryptography;

namespace WebApplication_SRPFIQ.Utils
{
    public class PasswordUtils
    {
        public string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
