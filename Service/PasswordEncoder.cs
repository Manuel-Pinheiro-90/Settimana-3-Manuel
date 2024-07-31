using System.Text;
using System.Security.Cryptography;
namespace Settimana_3_Manuel.Service
{
    public class PasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {
            using (var sha = SHA256.Create())
            {
                var asByteArray = Encoding.UTF8.GetBytes(password);
                var hashedPassword = sha.ComputeHash(asByteArray);
                return Convert.ToBase64String(hashedPassword);
            }
        }

        public bool IsSame(string plainText, string codedText)
        {
            return Encode(plainText) == codedText;
        }




    }
}
