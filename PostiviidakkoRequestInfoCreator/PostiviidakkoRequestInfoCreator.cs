using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Postiviidakko;

namespace PostiviidakkoRequestInfoCreator
{
    public static class PostiviidakkoRequestInfoCreator
    {
        [FunctionName("PostiviidakkoRequestInfoCreator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "requestinfocreator")] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            try
            {
                string content = data.content;
                string secretkey = data.secretkey;
                string endpoint = data.endpoint;

                byte[] contentData = Encoding.UTF8.GetBytes(content);

                string now = DateTime.Now.ToString("O");

                string contentMd5 = MessageCreator.CalculateMD5Hash(content);
                string message = MessageCreator.GenerateMessage(contentMd5, now, content, endpoint);
                string signature = SignatureCreator.GenerateSignature(secretkey, message);
                int contentLength = contentData.Length;

                var returnVal = new
                {
                    contentMd5,
                    signature,
                    now,
                    contentLength
                };

                return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnVal));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error in PostiviidakkoRequestInfoCreator: " + e.ToString());
            }
        }
    }
}
