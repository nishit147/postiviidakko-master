using System;
using System.Text;
using System.Security.Cryptography;

namespace Postiviidakko
{
    public static class MessageCreator
    {        
        public static string GenerateMessage(string contentMd5, string now, string content, string endpoint) {
            string message = string.Format("POST\n{0}\napplication/json\n{1}\n{2}\n{3}", contentMd5, now, content, endpoint);
            return message;
        }

        public static string CalculateMD5Hash(string input) {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) {
                sb.Append(hash[i].ToString("X2").ToLower());
            }

            return sb.ToString();
        }
    }
}
