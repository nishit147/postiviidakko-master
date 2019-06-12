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
    public static class ImportCSVToMailingList
    {
        [FunctionName("ImportCSVToMailingList")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "importcsvtomailinglist")] HttpRequest req,
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
                string searchendpoint = data.searchendpoint;
                string createendpoint = data.createendpoint;
                string joinendpoint = data.joinendpoint;

                string email = data.email;
                string listid = data.listid;

                string searchcontent = data.searchcontent;
                string createcontent = data.createcontent;

                string secretkey = data.secretkey;
                string realm = data.realm;
                string userid = data.userid;

                string now = DateTime.Now.ToString("O");

                bool recipientId = false;

                string searchResponseFromServer = Postiviidakko.RequestCreator.CreateRequest(searchendpoint, searchcontent, secretkey, now, realm, userid);
                dynamic searchResponseObj = JsonConvert.DeserializeObject(searchResponseFromServer);

                if (searchResponseObj.result != null)
                {
                    recipientId = searchResponseObj.result.Value;
                }
                //else
                //{
                //    string createResponseFromServer = Postiviidakko.RequestCreator.CreateRequest(createendpoint, createcontent, secretkey, now, realm, userid);
                //    dynamic createResponseObj = JsonConvert.DeserializeObject(createResponseFromServer);
                //    recipientId = ((long)createResponseObj.result.Value).ToString();
                //}

                if (!recipientId)
                {
                    //string joincontent = "{\"list_id\": " + listid + ",\"recipient_list\": " + recipientId + ",\"autoconfirm\": true}";
                    //string joinResponseFromServer = Postiviidakko.RequestCreator.CreateRequest(joinendpoint, joincontent, secretkey, now, realm, userid);                    
                    //return (ActionResult)new OkObjectResult(joinResponseFromServer);
                    return (ActionResult)new OkObjectResult("Server Response: "+ searchResponseFromServer);
                }
                else
                {
                    return new BadRequestObjectResult("Error in ImportCSVToMailingList Azure Function: RecipientId is null.");
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult("Error in ImportCSVToMailingList Azure Function: " + e.ToString());
            }
        }
    }
}
