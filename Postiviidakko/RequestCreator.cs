using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Postiviidakko
{
    public static class RequestCreator
    {
        public static string CreateRequest(string endpoint, string content, string secretkey, string now, string realm, string userid)
        {
            Stream stream;

            byte[] data = Encoding.UTF8.GetBytes(content);

            string contentMd5 = MessageCreator.CalculateMD5Hash(content);
            string message = MessageCreator.GenerateMessage(contentMd5, now, content, endpoint);
            string signature = SignatureCreator.GenerateSignature(secretkey, message);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://rest.lianamailer.com" + endpoint);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Headers.Set("X-Date", now);
            request.Headers.Set("Content-MD5", contentMd5);
            request.Headers.Set("Authorization", realm + " " + userid + ":" + signature);

            stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            WebResponse response = request.GetResponse();

            stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            stream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
