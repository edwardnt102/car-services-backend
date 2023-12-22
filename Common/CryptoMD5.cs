using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class CryptoMd5
    {
        public static string Encode(string original)
        {
            var md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(original);
            var encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }
    }
}
