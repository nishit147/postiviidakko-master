using System;
using System.Runtime;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Postiviidakko
{
    class Program {
        static void Main(string[] args) {
            Random random = new Random();
            string rnd = random.Next(0, 1000000).ToString();
            const string SECRETKEY = "4cd28a896516f054372ad3c17531293fee09a378df27db8728747a816d5e57cd";
            


            // List
            string endpoint = "/api/v1/createMailingList";
            string content = "[\"Ääkköset 100\",\"desc\",true]";
            string now = DateTime.Now.ToString("O");

            string responseFromServer = CreateRequest(endpoint, content, SECRETKEY, now);

            dynamic responseObj = JsonConvert.DeserializeObject(responseFromServer);
            dynamic listId = responseObj.result.Value;
            Console.WriteLine("List" + responseFromServer);
            //csv
             endpoint = "/api/v1/importCSVToMailingList";
            //int listid = 705011;
            // string csv = "email \n 10015045231.3556468209@supervise.fi \n 10023159054.520780531@supervise.fi";
            //content = "[\"" + listid + "\",null,"+csv+",null,false,false]";
            //content = "[\"" + listId + "\",null,\"10015045231.3556468209@supervise.fi\"]";
            content = "[\"" + listId + "\",null,\"10015045231.3556468209@supervise.fi\r\n10023159054.520780531@supervise.fi\r\n10049623121.3800529272@supervise.fi\r\n1023787312.3683123194@supervise.fi\r\n102744596.6485323126@supervise.fi\r\n1310470460.1490023139@supervise.fi\r\n1682317870.1712316292@supervise.fi\r\n1830166693.788231693@supervise.fi\r\n184919114.3620523147@supervise.fi\r\n2157207730.9231499003@supervise.fi\r\n2312238076.876843376@supervise.fi\r\n2317628783.6015761567@supervise.fi\r\n2380791991.5323161707@supervise.fi\r\n3108323116.5514619354@supervise.fi\r\n3141473585.5231214025@supervise.fi\r\n3165436768.2318667458@supervise.fi\r\n3467712311.6919971186@supervise.fi\r\n3821272119.2311420201@supervise.fi\r\n421358948.1825192318@supervise.fi\r\n4323165094.8817252323@supervise.fi\r\n4542230973.1962319190@supervise.fi\r\n4623182050.7908561563@supervise.fi\r\n4895009909.3231387060@supervise.fi\r\n4956946072.5832315750@supervise.fi\r\n5033023100.4772149847@supervise.fi\r\n5235328936.823137461@supervise.fi\r\n5696015710.727062315@supervise.fi\r\n6071231346.4604019122@supervise.fi\r\n6242023231.8011501638@supervise.fi\r\n635419257.4622314162@supervise.fi\r\n6794635900.7152312669@supervise.fi\r\n683132318.6088947443@supervise.fi\r\n6838107567.10072312109@supervise.fi\r\n7231009960.3073020393@supervise.fi\r\n7231016211.4111530130@supervise.fi\r\n753976513.4195231068@supervise.fi\r\n7651492876.3615231143@supervise.fi\r\n7723164237.7774301887@supervise.fi\r\n8342616331.2314349630@supervise.fi\r\n8485992311.3482610025@supervise.fi\r\n8538878775.9382313840@supervise.fi\r\n935584604.2140231600@supervise.fi\"]";
            now = DateTime.Now.ToString("O");
            //responseFromServer = CreateRequest(endpoint, content, SECRETKEY, now);
           // content = "[\"searchcontent\": \"{\"705050\",null,\"email\r\n10015045231.3556468209@supervise.fi\r\n10023159054.520780531@supervise.fi\r\n10049623121.3800529272@supervise.fi\r\n1023787312.3683123194@supervise.fi\r\n102744596.6485323126@supervise.fi\r\n1310470460.1490023139@supervise.fi\r\n1682317870.1712316292@supervise.fi\r\n1830166693.788231693@supervise.fi\r\n184919114.3620523147@supervise.fi\r\n2157207730.9231499003@supervise.fi\r\n2312238076.876843376@supervise.fi\r\n2317628783.6015761567@supervise.fi\r\n2380791991.5323161707@supervise.fi\r\n3108323116.5514619354@supervise.fi\r\n3141473585.5231214025@supervise.fi\r\n3165436768.2318667458@supervise.fi\r\n3467712311.6919971186@supervise.fi\r\n3821272119.2311420201@supervise.fi\r\n421358948.1825192318@supervise.fi\r\n4323165094.8817252323@supervise.fi\r\n4542230973.1962319190@supervise.fi\r\n4623182050.7908561563@supervise.fi\r\n4895009909.3231387060@supervise.fi\r\n4956946072.5832315750@supervise.fi\r\n5033023100.4772149847@supervise.fi\r\n5235328936.823137461@supervise.fi\r\n5696015710.727062315@supervise.fi\r\n6071231346.4604019122@supervise.fi\r\n6242023231.8011501638@supervise.fi\r\n635419257.4622314162@supervise.fi\r\n6794635900.7152312669@supervise.fi\r\n683132318.6088947443@supervise.fi\r\n6838107567.10072312109@supervise.fi\r\n7231009960.3073020393@supervise.fi\r\n7231016211.4111530130@supervise.fi\r\n753976513.4195231068@supervise.fi\r\n7651492876.3615231143@supervise.fi\r\n7723164237.7774301887@supervise.fi\r\n8342616331.2314349630@supervise.fi\r\n8485992311.3482610025@supervise.fi\r\n8538878775.9382313840@supervise.fi\r\n935584604.2140231600@supervise.fi\r\n\"}\"]";
            responseFromServer= Postiviidakko.RequestCreator.CreateRequest(endpoint, content, SECRETKEY, now, "EUR", "16561");
            responseObj = JsonConvert.DeserializeObject(responseFromServer);
            listId = responseObj.result.Value;
            Console.WriteLine("List" + responseFromServer);
            // Recipient
            rnd = random.Next(0, 1000000).ToString();

            endpoint = "/api/v1/createRecipient";
            // content = "{\"email\": \"" + rnd + "@gmail.com\"}";

            string rndemail = "aarne.kaakinen@amk-engiingasd.fi";

            content = "[\"" + rndemail + "\",null,[],true,\"API Created\",\"api\"]";
            // content = "[\"maansiirto@is-ma.fi\",null,[],true,\"API Created\",\"api\"]";
            // content = "{\"email\": \"" + rnd + "@gmail.com\",\"props\": {\"forename\": \"Forename " + rnd + "\",\"surname\": \"Surname " + rnd + "\",\"gender\": \"male\",\"region\": \"example_region\"}}";
            // content = "{\"email\": \"" + rnd + "@gmail.com\",\"sms\": \"" + rnd + "\",\"props\": {\"forename\": \"Forename " + rnd + "\",\"surname\": \"Surname " + rnd + "\",\"gender\": \"male\",\"region\": \"example_region\"}}";
            now = DateTime.Now.ToString("O");

            responseFromServer = CreateRequest(endpoint, content, SECRETKEY, now);

            responseObj = JsonConvert.DeserializeObject(responseFromServer);
            dynamic contactId = responseObj.result.Value;
            Console.WriteLine("Recipient" + responseFromServer);

            // Get recipient by email
            rndemail = "maansiirto@is-ma.fi";
            endpoint = "/api/v1/getRecipientByEmail";
            content = "{\"email\":\"" + rndemail + "\"}";
            now = DateTime.Now.ToString("O");

            responseFromServer = CreateRequest(endpoint, content, SECRETKEY, now);
            responseObj = JsonConvert.DeserializeObject(responseFromServer);
            dynamic result = responseObj.result.recipient;

            // Join
            rnd = random.Next(0, 1000000).ToString();

            endpoint = "/api/v1/joinMailingList";
            content = "{\"list_id\": " + listId + ",\"recipient_list\": " + contactId + ",\"autoconfirm\": true}";
            now = DateTime.Now.ToString("O");

            responseFromServer = CreateRequest(endpoint, content, SECRETKEY, now);

            responseObj = JsonConvert.DeserializeObject(responseFromServer);
            Console.WriteLine("Join" + responseFromServer);

            Console.ReadLine();
        }

        public static string CreateRequest(string endpoint, string content, string secretkey, string now)
        {
            Stream stream;

            byte[] data = Encoding.UTF8.GetBytes(content);

            string contentMd5 = MessageCreator.CalculateMD5Hash(content);
            string message = MessageCreator.GenerateMessage(contentMd5, now, content, endpoint);
            string signature = SignatureCreator.GenerateSignature(secretkey, message);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://rest.lianamailer.com" + endpoint);

            request.Method = "POST";
            request.Accept = "application/json; charset=UTF-8";
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = data.Length;
            request.Headers.Set("X-Date", now);
            request.Headers.Set("Content-MD5", contentMd5);
            request.Headers.Set("Authorization", "EUR 16561:" + signature);

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
