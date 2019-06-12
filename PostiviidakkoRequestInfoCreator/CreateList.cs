using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace PostiviidakkoRequestInfoCreator
{
    public static class CreateList
    {
        [FunctionName("CreateList")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createlist")] HttpRequest req,
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
                string endpoint = data.endpoint;
                string content = data.content;
                string secretkey = data.secretkey;
                string realm = data.realm;
                string userid = data.userid;

                string now = DateTime.Now.ToString("O");

                string responseFromServer = Postiviidakko.RequestCreator.CreateRequest(endpoint, content, secretkey, now, realm, userid);

                dynamic responseObj = JsonConvert.DeserializeObject(responseFromServer);
                dynamic listId = responseObj.result.Value;

                return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(listId));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error in CreateList Azure Function: " + e.ToString());
            }
        }
    }
}
