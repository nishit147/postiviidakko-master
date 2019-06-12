using System;
using System.Text;
using System.Security.Cryptography;

namespace Postiviidakko
{
    public static class SignatureCreator
    {
        public static string GenerateSignature(string secret, string message)
        {
            HMACSHA256 hmc = new HMACSHA256(Encoding.UTF8.GetBytes(secret));

            byte[] hmres = hmc.ComputeHash(Encoding.UTF8.GetBytes(message));
            string signature = BitConverter.ToString(hmres).Replace("-", "").ToLower();

            return signature;
        }
    }
}
