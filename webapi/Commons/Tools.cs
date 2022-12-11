using System.Globalization;
using System.Security.Cryptography;

namespace webapi.Commons
{
    public class Tools
    {
        public static string ToSHA256(string inputStr)
        {
            using (SHA256 SHA256Crypto = SHA256.Create())
            {
                byte[] input = System.Text.Encoding.UTF8.GetBytes(inputStr);
                input = SHA256Crypto.ComputeHash(input);
                System.Text.StringBuilder output = new System.Text.StringBuilder();
                foreach (byte b in input)
                {
                    output.Append(b.ToString("x2", CultureInfo.InvariantCulture).ToUpperInvariant());
                }

                return output.ToString();
            }
        }
    }
}
